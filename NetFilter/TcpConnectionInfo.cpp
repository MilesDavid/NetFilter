#include "StdAfx.h"
#include "TcpConnectionInfo.h"

TcpConnectionInfo::TcpConnectionInfo(const PNF_TCP_CONN_INFO info) : m_info(*info) {}

TcpConnectionInfo::~TcpConnectionInfo() {}
