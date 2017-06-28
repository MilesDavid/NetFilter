#include "StdAfx.h"
#include "TcpConnectionInfo.h"

TcpConnectionInfo::TcpConnectionInfo(const PNF_TCP_CONN_INFO info, const std::string& procName) :
	m_info(info),
	m_processName(procName)
{}

TcpConnectionInfo::~TcpConnectionInfo() {}
