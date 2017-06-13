#pragma once

using namespace nfapi;
using namespace ProtocolFilters;

class TcpConnectionInfo {
private:
	NF_TCP_CONN_INFO m_info;
public:
	TcpConnectionInfo(const PNF_TCP_CONN_INFO info);
	~TcpConnectionInfo();

	const PNF_TCP_CONN_INFO info() { return &m_info; };
};

