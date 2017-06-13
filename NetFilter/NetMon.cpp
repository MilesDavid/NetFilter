#include "stdafx.h"
#include "NetMon.h"

using namespace nfapi;
using namespace ProtocolFilters;

bool NetMon::Init() {
	if (!m_Init) {
		try {
			if (m_settings == nullptr) {
				m_settings = new NetFilterSettings();
			}
			if (m_netfilter == nullptr && m_settings != nullptr) {
				m_netfilter = new NetFilter(m_settings);
			}
		}
		catch (const std::exception&) {
			// write to log
			printf_s("[%s] Couldn't allocate memory. Errno: %d\n", __FUNCTION__, errno);
			return false;
		}

		if (!m_settings->readSettings()) {
			return false;
		}
	#pragma region Creating folders
		//Creating folders
		std::wstring dumpInFolder = m_settings->dumpPath() + L"\\RAW\\in";
		if (AuxiliaryFuncs::forceDirectories(dumpInFolder)) {
			// write to log
			printf_s("[%s] Folder %s has been created\n", __FUNCTION__,
				std::string(dumpInFolder.begin(), dumpInFolder.end()).c_str());
		}
		std::wstring dumpOutFolder = m_settings->dumpPath() + L"\\RAW\\out";
		if (AuxiliaryFuncs::forceDirectories(dumpOutFolder)) {
			// write to log
			printf_s("[%s] Folder %s has been created\n", __FUNCTION__,
				std::string(dumpOutFolder.begin(), dumpOutFolder.end()).c_str());
		}
		if (AuxiliaryFuncs::forceDirectories(m_settings->certPath().c_str())) {
			// write to log
			std::wstring tmp(m_settings->certPath());
			printf_s("[%s] Folder %s has been created\n", __FUNCTION__,
				std::string(tmp.begin(), tmp.end()).c_str());
		}
	#pragma endregion

		if (!pf_init(m_netfilter, m_settings->certPath().c_str())) {
			// write to log
			printf_s("[%s] Couldn't init protocol filter\n", __FUNCTION__);
			return false;
		}

		pf_setRootSSLCertSubject("NetFilter");

		// Initialize the library and start filtering thread
		if (nf_init("netfilter2", pf_getNFEventHandler()) != NF_STATUS_SUCCESS) {
			// write to log
			printf_s("[%s] Couldn't init netfilter\n", __FUNCTION__);
			return false;
		}
	}

	AddNetFilterRules();

	return m_settings && m_netfilter;
}

void NetMon::Release() {
	if (m_Init) {
		pf_free();
		nf_free();

		if (m_netfilter != nullptr) {
			delete m_netfilter;
		}
		if (m_settings != nullptr) {
			delete m_settings;
		}
	}
}

bool NetMon::AddRule(int protocol, unsigned long processId, unsigned char direction, unsigned short localPort, unsigned short remotePort, unsigned short ip_family, const std::string& localIpAddress, const std::string& localIpAddressMask, const std::string& remoteIpAddress, const std::string& remoteIpAddressMask, unsigned long filteringFlag, BOOL appendToHead) {
	NF_RULE rule;
	memset(&rule, 0, sizeof(rule));

	rule.protocol = protocol;
	rule.processId = processId;
	rule.direction = direction;
	rule.localPort = ntohs(localPort);
	rule.remotePort = ntohs(remotePort);
	rule.ip_family = ip_family;

	if (localIpAddress != "") {
		*((unsigned long*)rule.localIpAddress) = inet_addr(localIpAddress.c_str());
	}
	if (localIpAddressMask != "") {
		*((unsigned long*)rule.localIpAddressMask) = inet_addr(localIpAddressMask.c_str());
	}
	if (remoteIpAddress != "") {
		*((unsigned long*)rule.remoteIpAddress) = inet_addr(remoteIpAddress.c_str());
	}
	if (remoteIpAddressMask != "") {
		*((unsigned long*)rule.remoteIpAddressMask) = inet_addr(remoteIpAddressMask.c_str());
	}
	rule.filteringFlag = filteringFlag;

	NF_STATUS status = nf_addRule(&rule, appendToHead);

	if (status != NF_STATUS_SUCCESS) {
		// write to log
		printf_s("[%s] Couldn't add netfilter rule\n", __FUNCTION__);
		return false;
	}

	return true;
}

void NetMon::AddNetFilterRules() {
	AddRule(IPPROTO_TCP, 0, 0, 0, 0, AF_INET, "", "", "127.0.0.1", "255.0.0.0", NF_ALLOW);
	AddRule(IPPROTO_TCP, 0, 0, 0, 0, AF_INET, "", "", "", "", NF_FILTER);
}

NetMon::NetMon() : m_netfilter(nullptr), m_settings(nullptr), m_Init(false) {}

NetMon::~NetMon() {
	Release();
}

bool NetMon::Start() {
	return Init();
}

void NetMon::Stop() {
	Release();
}

void NetMon::RefreshSettings() {
	m_settings->readSettings();;

	//Refresing filter rules
	m_netfilter->refreshFilters();
}
