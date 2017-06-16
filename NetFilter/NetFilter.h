#pragma once

#include "PFEvents.h"
#include "NetFilterSettings.h"
#include "TcpConnectionInfo.h"
#include "AuxiliaryFuncs.h"
#include "Logger.h"

using namespace nfapi;
using namespace ProtocolFilters;

class NetFilter :
	public PFEvents, public Noncopyable {
private:
	inline void addProtocolFilterRules(ENDPOINT_ID id, PNF_TCP_CONN_INFO pConnInfo);

#pragma region Packet handlers
	inline bool rawInHandler(ENDPOINT_ID id, PFObject * object);
	inline bool rawOutHandler(ENDPOINT_ID id, PFObject * object);
#pragma endregion

	bool pFStreamToString(PFStream * stream, std::string& str);
	bool writePacket(std::string packetType, std::string direction, std::string buf);

	bool m_Init;
	const NetFilterSettings* m_settings;
	std::map<ENDPOINT_ID, TcpConnectionInfo> m_connections;
	Logger* m_logger;
public:
	NetFilter(const NetFilterSettings* settings, Logger* logger);
	~NetFilter();

	const bool isInit() const {
		return m_Init;
	}
	bool refreshFilters();

	virtual void threadStart();
	virtual void threadEnd();

	virtual void tcpConnected(ENDPOINT_ID id, PNF_TCP_CONN_INFO pConnInfo);
	virtual void tcpConnectRequest(ENDPOINT_ID id, PNF_TCP_CONN_INFO pConnInfo);
	virtual void tcpClosed(ENDPOINT_ID id, PNF_TCP_CONN_INFO pConnInfo);

	virtual NF_STATUS tcpPostSend(ENDPOINT_ID id, const char * buf, int len);
	virtual NF_STATUS tcpPostReceive(ENDPOINT_ID id, const char * buf, int len);
	virtual NF_STATUS tcpSetConnectionState(ENDPOINT_ID id, int suspended);

	virtual void udpCreated(ENDPOINT_ID id, PNF_UDP_CONN_INFO pConnInfo);
	virtual void udpConnectRequest(ENDPOINT_ID id, PNF_UDP_CONN_REQUEST pConnReq);
	virtual void udpClosed(ENDPOINT_ID id, PNF_UDP_CONN_INFO pConnInfo);

	virtual NF_STATUS udpPostSend(ENDPOINT_ID id, const unsigned char * remoteAddress,
		const char * buf, int len,
		PNF_UDP_OPTIONS options);
	virtual NF_STATUS udpPostReceive(ENDPOINT_ID id, const unsigned char * remoteAddress,
		const char * buf, int len, PNF_UDP_OPTIONS options);
	virtual NF_STATUS udpSetConnectionState(ENDPOINT_ID id, int suspended);

	virtual void dataAvailable(ENDPOINT_ID id, PFObject * object);
	virtual PF_DATA_PART_CHECK_RESULT dataPartAvailable(ENDPOINT_ID id, PFObject * object);
};

