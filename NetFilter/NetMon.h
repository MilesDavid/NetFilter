#pragma once

#include "NetFilter.h"
#include "NetFilterSettings.h"

class NetMon : public Noncopyable {
private:
	bool Init();
	void Release();
	bool AddRule(int protocol, unsigned long processId, unsigned char direction, unsigned short localPort, unsigned short remotePort, unsigned short ip_family, const std::string & localIpAddress, const std::string & localIpAddressMask, const std::string & remoteIpAddress, const std::string & remoteIpAddressMask, unsigned long filteringFlag, BOOL appendToHead = FALSE);
	void AddNetFilterRules();

	NetFilter* m_netfilter;
	NetFilterSettings* m_settings;

	bool m_Init;
public:
	NetMon();
	~NetMon();

	bool Start();
	void Stop();
	void RefreshSettings();
};

