#include "stdafx.h"
#include "NetFilter.h"

inline void NetFilter::addProtocolFilterRules(ENDPOINT_ID id, PNF_TCP_CONN_INFO pConnInfo) {
	if (pConnInfo->direction == NF_D_OUT) {
		pf_addFilter(id, FT_PROXY, FF_READ_ONLY_OUT);
		pf_addFilter(id, FT_SSL, FF_SSL_TLS | FF_SSL_TLS_AUTO | FF_SSL_INDICATE_HANDSHAKE_REQUESTS);
		pf_addFilter(id, FT_RAW, FF_READ_ONLY_OUT);
	}
	else if (pConnInfo->direction == NF_D_IN) {
		pf_addFilter(id, FT_PROXY, FF_READ_ONLY_IN);
		pf_addFilter(id, FT_SSL, FF_SSL_TLS | FF_SSL_TLS_AUTO | FF_SSL_INDICATE_HANDSHAKE_REQUESTS);
		pf_addFilter(id, FT_RAW, FF_READ_ONLY_IN);
	}
}

inline bool NetFilter::rawInHandler(ENDPOINT_ID id, PFObject * object) {
	PFStream* rawStream = object->getStream();
	std::string buf;

	pf_postObject(id, object);

	if (rawStream && pFStreamToString(rawStream, buf)) {
		// Dump packet
		if (writePacket(L"RAW", L"in", buf)) {
			return true;
		}
		else {
			// write to log
			printf_s("[%s] Couldn't dump packet\n", __FUNCTION__);
		}
	}
	else {
		// write to log
		printf_s("[%s] Couldn't read stream\n", __FUNCTION__);
	}

	return false;
}

inline bool NetFilter::rawOutHandler(ENDPOINT_ID id, PFObject * object) {
	PFStream* rawStream = object->getStream();
	std::string buf;

	pf_postObject(id, object);

	if (rawStream && pFStreamToString(rawStream, buf)) {
		// Dump packet
		if (writePacket(L"RAW", L"out", buf)) {
			return true;
		}
		else {
			// write to log
			printf_s("[%s] Couldn't dump packet\n", __FUNCTION__);
		}
	}
	else {
		// write to log
		printf_s("[%s] Couldn't read stream\n", __FUNCTION__);
	}

	return false;
}

bool NetFilter::pFStreamToString(PFStream * stream, std::string & str) {
	if (stream != nullptr) {
		char * buf = (char*)malloc((tStreamPos)stream->size());

		if (buf) {
			stream->seek(0, FILE_BEGIN);
			tStreamPos bufLen = (tStreamPos)stream->size();
			tStreamPos readed = stream->read(buf, bufLen);

			str.reserve(readed);
			str.assign(buf, readed);

			free(buf);

			return true;
		}
		else {
			// write to log
			printf_s("[%s] Couldn't allocate memory. Errno: %d\n", __FUNCTION__, errno);
		}
	}

	return false;
}

bool NetFilter::writePacket(std::wstring packetType, std::wstring direction, std::string buf) {
	std::fstream dumpFile;
	std::wstring wsPacketType(packetType.begin(), packetType.end());
	std::wstring dumpDir = m_settings->dumpPath() + L"\\" + wsPacketType + L"\\" + direction + L"\\";
	std::wstring dumpPath = dumpDir + AuxiliaryFuncs::getTimeStamp() + L".txt";

	dumpFile.open(dumpPath, std::ios::out);
	if (dumpFile.is_open()) {
		dumpFile << buf;
		dumpFile.close();

		return true;
	}
	else {
		// write to log
		printf_s("[%s] Couldn't open file. Errno: %d\n", __FUNCTION__, errno);
	}

	return false;
}

NetFilter::NetFilter(const NetFilterSettings * settings) : m_settings(settings), m_connections() {}

NetFilter::~NetFilter() {}

bool NetFilter::refreshFilters() {
	for each(auto connection in m_connections) {
		ENDPOINT_ID id = connection.first;
		int filtersCount = pf_getFilterCount(id);

		if (filtersCount > 0 && pf_canDisableFiltering(id) == TRUE) {
			for (int filterType = FT_SSL; filterType <= FT_XMPP; filterType += FT_STEP) {
				if (pf_isFilterActive(id, (PF_FilterType)filterType) == TRUE) {
					pf_deleteFilter(id, (PF_FilterType)filterType);
				}
			}
		}
	}

	return false;
}

void NetFilter::threadStart() {
	printf_s("[%s]\n", __FUNCTION__);
}

void NetFilter::threadEnd() {}

void NetFilter::tcpConnected(ENDPOINT_ID id, PNF_TCP_CONN_INFO pConnInfo) {
	std::wstring wsProcessName;
	DWORD pid = pConnInfo->processId;
	if (AuxiliaryFuncs::getProcessName(pid, wsProcessName, true) == ERROR_SUCCESS) {
		std::string sProcName(wsProcessName.begin(), wsProcessName.end());
		AuxiliaryFuncs::toLower(sProcName);

		if (pid != 4 && m_settings->findProcess(sProcName)) {
			addProtocolFilterRules(id, pConnInfo);
			m_connections.insert(std::make_pair(id, TcpConnectionInfo(pConnInfo)));
		}
	}
	else {
		// write to log
		printf_s("[%s] Couldn't get procname by pid: %d\n", __FUNCTION__, pid);
	}
}

void NetFilter::tcpConnectRequest(ENDPOINT_ID id, PNF_TCP_CONN_INFO pConnInfo) {}

void NetFilter::tcpClosed(ENDPOINT_ID id, PNF_TCP_CONN_INFO pConnInfo) {
	auto elem = m_connections.find(id);
	if (elem != m_connections.end()) {
		m_connections.erase(elem);
	}
}

NF_STATUS NetFilter::tcpPostSend(ENDPOINT_ID id, const char * buf, int len) {
	return nf_tcpPostSend(id, buf, len);
}

NF_STATUS NetFilter::tcpPostReceive(ENDPOINT_ID id, const char * buf, int len) {
	return nf_tcpPostReceive(id, buf, len);
}

NF_STATUS NetFilter::tcpSetConnectionState(ENDPOINT_ID id, int suspended) {
	return nf_tcpSetConnectionState(id, suspended);
}

void NetFilter::udpCreated(ENDPOINT_ID id, PNF_UDP_CONN_INFO pConnInfo) {}

void NetFilter::udpConnectRequest(ENDPOINT_ID id, PNF_UDP_CONN_REQUEST pConnReq) {}

void NetFilter::udpClosed(ENDPOINT_ID id, PNF_UDP_CONN_INFO pConnInfo) {}

NF_STATUS NetFilter::udpPostSend(ENDPOINT_ID id, const unsigned char * remoteAddress, const char * buf, int len, PNF_UDP_OPTIONS options) {
	return nf_udpPostSend(id, remoteAddress, buf, len, options);
}

NF_STATUS NetFilter::udpPostReceive(ENDPOINT_ID id, const unsigned char * remoteAddress, const char * buf, int len, PNF_UDP_OPTIONS options) {
	return nf_udpPostReceive(id, remoteAddress, buf, len, options);
}

NF_STATUS NetFilter::udpSetConnectionState(ENDPOINT_ID id, int suspended) {
	return nf_udpSetConnectionState(id, suspended);
}

void NetFilter::dataAvailable(ENDPOINT_ID id, PFObject * object) {
	switch (object->getType()) {
		case OT_RAW_INCOMING:
			rawInHandler(id, object);
			break;
		case OT_RAW_OUTGOING:
			rawOutHandler(id, object);
			break;
		default:
			pf_postObject(id, object);
			break;
	}
}

PF_DATA_PART_CHECK_RESULT NetFilter::dataPartAvailable(ENDPOINT_ID id, PFObject * object) {
	return DPCR_FILTER;
}
