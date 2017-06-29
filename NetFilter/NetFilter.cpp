#include "stdafx.h"
#include "NetFilter.h"

inline void NetFilter::addProtocolFilterRules(ENDPOINT_ID id, PNF_TCP_CONN_INFO pConnInfo) {
	tPF_FilterFlags sslFlags = (m_settings->selfSigned()) ?
		FF_SSL_TLS | FF_SSL_TLS_AUTO | FF_SSL_INDICATE_HANDSHAKE_REQUESTS | FF_SSL_SELF_SIGNED_CERTIFICATE :
		FF_SSL_TLS | FF_SSL_TLS_AUTO | FF_SSL_INDICATE_HANDSHAKE_REQUESTS;

	if (pConnInfo->direction == NF_D_OUT) {
		pf_addFilter(id, FT_PROXY, FF_READ_ONLY_OUT);
		pf_addFilter(id, FT_SSL, sslFlags);
		pf_addFilter(id, FT_HTTP, FF_READ_ONLY_OUT);
		pf_addFilter(id, FT_RAW, FF_READ_ONLY_OUT);
	}
	else if (pConnInfo->direction == NF_D_IN) {
		pf_addFilter(id, FT_PROXY, FF_READ_ONLY_IN);
		pf_addFilter(id, FT_SSL, sslFlags);
		pf_addFilter(id, FT_HTTP, FF_READ_ONLY_IN);
		pf_addFilter(id, FT_RAW, FF_READ_ONLY_IN);
	}
}

inline bool NetFilter::rawInHandler(ENDPOINT_ID id, PFObject * object) {

	PFStream* rawStream = object->getStream();
	std::string buf = "";

	pf_postObject(id, object);

	if (pFStreamToString(rawStream, buf)) {
		// Dump packet
		std::string connInfo = "";
		auto tcpConn = m_connections.find(id);
		if (tcpConn != m_connections.end()) {
			connInfo += tcpConn->second.toString();
		}

		if (writePacket("RAW", "in", connInfo + buf)) {
			return true;
		}
		else {
			// write to log
			m_logger->write("Couldn't dump packet", __FUNCTION__);
			printf_s("[%s] Couldn't dump packet\n", __FUNCTION__);
		}
	}
	else {
		// write to log
		m_logger->write("Couldn't read stream", __FUNCTION__);
		printf_s("[%s] Couldn't read stream\n", __FUNCTION__);
	}

	return false;
}

inline bool NetFilter::rawOutHandler(ENDPOINT_ID id, PFObject * object) {

	PFStream* rawStream = object->getStream();
	std::string buf = "";

	pf_postObject(id, object);

	if (pFStreamToString(rawStream, buf)) {
		// Dump packet
		std::string connInfo = "";
		auto tcpConn = m_connections.find(id);
		if (tcpConn != m_connections.end()) {
			connInfo += tcpConn->second.toString();
		}

		if (writePacket("RAW", "out", connInfo + buf)) {
			return true;
		}
		else {
			// write to log
			m_logger->write("Couldn't dump packet", __FUNCTION__);
			printf_s("[%s] Couldn't dump packet\n", __FUNCTION__);
		}
	}
	else {
		// write to log
		m_logger->write("Couldn't read stream", __FUNCTION__);
		printf_s("[%s] Couldn't read stream\n", __FUNCTION__);
	}

	return false;
}

inline bool NetFilter::httpRequestHandler(ENDPOINT_ID id, PFObject * object) {
	PFStream* httpStatusStream = object->getStream(HS_STATUS);
	PFStream* httpHeaderStream = object->getStream(HS_HEADER);
	PFStream* httpContentStream = object->getStream(HS_CONTENT);
	std::string statusBuf = "", headerBuf = "", contentBuf = "";

	pf_postObject(id, object);

#ifdef UNPACK_GZIP_CONTENT
	PFHeader httpHeader;
	if (pf_readHeader(httpHeaderStream, &httpHeader)
		&& httpHeader.toString().find("Content-Encoding: gzip") != std::string::npos) {
		if (!pf_unzipStream(httpContentStream)) {
			m_logger->write("Couldn't UNZIP content", __FUNCTION__);
		}
	}
#endif

	if (pFStreamToString(httpStatusStream, statusBuf) &&
		pFStreamToString(httpHeaderStream, headerBuf) &&
		pFStreamToString(httpContentStream, contentBuf)) {

		std::string buf = "";
		auto tcpConn = m_connections.find(id);
		if (tcpConn != m_connections.end()) {
			buf += tcpConn->second.toString();
		}

		buf += statusBuf + headerBuf + contentBuf;

		// Dump packet
		if (writePacket("HTTP", "request", buf)) {
			return true;
		}
		else {
			// write to log
			m_logger->write("Couldn't dump packet", __FUNCTION__);
			printf_s("[%s] Couldn't dump packet\n", __FUNCTION__);
		}
	}
	else {
		// write to log
		m_logger->write("Couldn't read stream", __FUNCTION__);
		printf_s("[%s] Couldn't read stream\n", __FUNCTION__);
	}

	return false;
}

inline bool NetFilter::httpResponseHandler(ENDPOINT_ID id, PFObject * object) {
	PFStream* httpStatusStream = object->getStream(HS_STATUS);
	PFStream* httpHeaderStream = object->getStream(HS_HEADER);
	PFStream* httpContentStream = object->getStream(HS_CONTENT);
	std::string statusBuf = "", headerBuf = "", contentBuf = "";

	pf_postObject(id, object);

#ifdef UNPACK_GZIP_CONTENT
	PFHeader httpHeader;
	if (pf_readHeader(httpHeaderStream, &httpHeader)
		&& httpHeader.toString().find("Content-Encoding: gzip") != std::string::npos) {
		if (!pf_unzipStream(httpContentStream)) {
			m_logger->write("Couldn't UNZIP content", __FUNCTION__);
		}
	}
#endif

	if (pFStreamToString(httpStatusStream, statusBuf) &&
		pFStreamToString(httpHeaderStream, headerBuf) &&
		pFStreamToString(httpContentStream, contentBuf)) {

		std::string buf = "";
		auto tcpConn = m_connections.find(id);
		if (tcpConn != m_connections.end()) {
			buf += tcpConn->second.toString();
		}

		buf += statusBuf + headerBuf + contentBuf;

		// Dump packet
		if (writePacket("HTTP", "response", buf)) {
			return true;
		}
		else {
			// write to log
			m_logger->write("Couldn't dump packet", __FUNCTION__);
			printf_s("[%s] Couldn't dump packet\n", __FUNCTION__);
		}
	}
	else {
		// write to log
		m_logger->write("Couldn't read stream", __FUNCTION__);
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
			m_logger->write("Couldn't allocate memory. Errno: " + std::to_string(errno), __FUNCTION__);
			printf_s("[%s] Couldn't allocate memory. Errno: %d\n", __FUNCTION__, errno);
		}
	}

	return false;
}

bool NetFilter::writePacket(std::string packetType, std::string direction, std::string buf) {
	std::fstream dumpFile;
	std::string sPacketType(packetType.begin(), packetType.end());
	std::string dumpDir = m_settings->dumpPath() + "\\" + sPacketType + "\\" + direction + "\\";
	std::string dumpPath = dumpDir + AuxiliaryFuncs::getTimeStamp("%04d-%02d-%02d-%02d-%02d-%02d-%03d") + ".txt";

	dumpFile.open(dumpPath, std::ios::out | std::fstream::binary);
	if (dumpFile.is_open()) {
		dumpFile << buf;
		dumpFile.close();

		return true;
	}
	else {
		// write to log
		m_logger->write("Couldn't open file. Errno: " + std::to_string(errno), __FUNCTION__);
		printf_s("[%s] Couldn't open file. Errno: %d\n", __FUNCTION__, errno);
	}

	return false;
}

NetFilter::NetFilter(const NetFilterSettings * settings, Logger* logger) :
	m_Init(false),
	m_settings(settings),
	m_connections(),
	m_logger(logger)
{
	m_Init = true;
}

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
	m_logger->write("Netfilter thread started", __FUNCTION__);
	printf_s("[%s]\n", __FUNCTION__);
}

void NetFilter::threadEnd() {
	m_logger->write("Netfilter thread ended", __FUNCTION__);
	printf_s("[%s]\n", __FUNCTION__);
}

void NetFilter::tcpConnected(ENDPOINT_ID id, PNF_TCP_CONN_INFO pConnInfo) {
	std::string sProcessName;
	DWORD pid = pConnInfo->processId;
	DWORD error = AuxiliaryFuncs::getProcessName(pid, sProcessName, true);
	if (error == ERROR_SUCCESS) {
		AuxiliaryFuncs::toLower(sProcessName);

		if (
#ifndef FILTER_ANY_PROCESS
			pid != 4 && m_settings->findProcess(sProcessName)
#else
			true
#endif
			) {
			addProtocolFilterRules(id, pConnInfo);
			m_connections.insert(std::make_pair(id, TcpConnectionInfo(pConnInfo, sProcessName)));
		}
	}
	else {
		// write to log
		TCHAR msg[MAX_PATH] = { 0 };
		_snprintf_s(msg, MAX_PATH, "Couldn't get procname by pid: %d. Last error: %d", pid, error);

		m_logger->write(msg, __FUNCTION__);
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
		case OT_HTTP_REQUEST:
			httpRequestHandler(id, object);
			break;
		case OT_HTTP_RESPONSE:
			httpResponseHandler(id, object);
			break;
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
