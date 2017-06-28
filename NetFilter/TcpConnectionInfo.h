#pragma once

using namespace nfapi;
using namespace ProtocolFilters;

class TcpConnectionInfo {
private:
	const PNF_TCP_CONN_INFO m_info;
	std::string m_processName;
public:
	TcpConnectionInfo(const PNF_TCP_CONN_INFO info, const std::string& procName = "");
	~TcpConnectionInfo();

	const PNF_TCP_CONN_INFO info() const { return m_info; };
	const std::string& processName() const { return m_processName; };
	const std::string toString() const {
		std::string res = "[connection_id]: " + std::to_string(m_info->processId) + "\r\n";
		res += "[process_name]: " + m_processName + "\r\n\r\n";
		return res;
	};
};

