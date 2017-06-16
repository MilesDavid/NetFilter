#include "stdafx.h"
#include "NetMon.h"

using namespace nfapi;
using namespace ProtocolFilters;

const std::string driverName = "netfilter2";

bool NetMon::Init() {
	if (!m_Init) {
		try {
			if (m_logger == nullptr) {
				std::string disk, dir, file, ext;
				if (!AuxiliaryFuncs::getCurrentFile(disk, dir, file, ext)) {
					return false;
				}

				std::string logFileName = disk + dir + "NetFilter.log";
				m_logger = new Logger(logFileName);
			}
			else {
				// write to log
				return false;
			}

			if (m_settings == nullptr) {
				m_settings = new NetFilterSettings(m_logger);
			}
			else {
				// write to log
				m_logger->write("m_settings already initialized", __FUNCTION__);
				return false;
			}


		}
		catch (const std::exception&) {
			// write to log
			TCHAR msg[MAX_PATH] = { 0 };
			_snprintf_s(msg, MAX_PATH, "Couldn't allocate memory. Errno: %d", errno);

			m_logger->write(msg, __FUNCTION__);
			printf_s("[%s] Couldn't allocate memory. Errno: %d\n", __FUNCTION__, errno);

			return false;
		}

		if (!m_settings->readSettings()) {
			// write to log
			m_logger->write("Couldn't read settings", __FUNCTION__);
			return false;
		}
		m_logger->write("Settings readed succcessfully", __FUNCTION__);
#pragma region Creating folders
		//Creating folders
		std::string dumpInFolder = m_settings->dumpPath() + "\\RAW\\in";
		if (AuxiliaryFuncs::forceDirectories(dumpInFolder)) {
			// write to log
			TCHAR msg[MAX_PATH] = { 0 };
			_snprintf_s(msg, MAX_PATH, "Folder %s has been created", dumpInFolder.c_str());

			m_logger->write(msg, __FUNCTION__);
			printf_s("[%s] Folder %s has been created\n", __FUNCTION__, dumpInFolder.c_str());
		}
		std::string dumpOutFolder = m_settings->dumpPath() + "\\RAW\\out";
		if (AuxiliaryFuncs::forceDirectories(dumpOutFolder)) {
			// write to log
			TCHAR msg[MAX_PATH] = { 0 };
			_snprintf_s(msg, MAX_PATH, "Folder %s has been created", dumpOutFolder.c_str());

			m_logger->write(msg, __FUNCTION__);
			printf_s("[%s] Folder %s has been created\n", __FUNCTION__, dumpOutFolder.c_str());
		}
		if (AuxiliaryFuncs::forceDirectories(m_settings->certPath().c_str())) {
			// write to log
			TCHAR msg[MAX_PATH] = { 0 };
			_snprintf_s(msg, MAX_PATH, "Folder %s has been created", m_settings->certPath().c_str());

			m_logger->write(msg, __FUNCTION__);
			printf_s("[%s] Folder %s has been created\n", __FUNCTION__, m_settings->certPath().c_str());
		}
#pragma endregion

		if (!copyDriver()) {
			// write to log
			m_logger->write("Couldn't copy driver " + driverName + ".sys", __FUNCTION__);
			return false;
		}

		m_logger->write("Driver " + driverName + ".sys has been copied", __FUNCTION__);
	}

	return m_settings->isInit() && m_logger->isInit();
}

void NetMon::Release() {
	if (m_Init) {

		Stop();

		if (!removeDriver()) {
			// write to log
			m_logger->write("Couldn't remove driver " + driverName + ".sys", __FUNCTION__);
		}
		else {
			m_logger->write("Driver " + driverName + ".sys has been deleted", __FUNCTION__);
		}

		if (m_settings != nullptr) {
			delete m_settings;
			m_settings = nullptr;
		}

		if (m_logger != nullptr && m_logger->isInit()) {
			delete m_logger;
			m_logger = nullptr;
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
		TCHAR msg[MAX_PATH] = { 0 };
		_snprintf_s(msg, MAX_PATH, "Couldn't add netfilter rule. Status: %d", status);

		m_logger->write(msg, __FUNCTION__);
		printf_s("[%s] Couldn't add netfilter rule. Status: %d\n", __FUNCTION__, status);
		return false;
	}

	return true;
}

void NetMon::AddNetFilterRules() {
	AddRule(IPPROTO_TCP, 0, 0, 0, 0, AF_INET, "", "", "127.0.0.1", "255.0.0.0", NF_ALLOW);
	AddRule(IPPROTO_TCP, 0, 0, 0, 0, AF_INET, "", "", "", "", NF_FILTER);
}

bool NetMon::copyDriver() {
	std::string disk, dir, file, ext;
	if (!AuxiliaryFuncs::getCurrentFile(disk, dir, file, ext)) {
		m_logger->write("Couldn't get current file", __FUNCTION__);
		return false;
	}

	TCHAR dstDriverFolder[MAX_PATH] = { 0 };
	if (!ExpandEnvironmentStrings("%windir%\\system32\\drivers\\", dstDriverFolder, MAX_PATH)) {
		m_logger->write("Couldn't expand string", __FUNCTION__);
		return false;
	}

	std::string osBit = (AuxiliaryFuncs::IsWow64()) ? "amd64" : "i386";
	std::string srcDriverPath = disk + dir + "bin\\drivers\\" + osBit + "\\" + driverName + ".sys";
	std::string dstDriverPath = dstDriverFolder + driverName + ".sys";

	if (!CopyFile(srcDriverPath.c_str(), dstDriverPath.c_str(), FALSE)) {
		TCHAR msg[MAX_PATH] = { 0 };
		_snprintf_s(msg, MAX_PATH, "Couldn't copy driver %s to %s. Error: %d", srcDriverPath.c_str(), dstDriverFolder, GetLastError());

		m_logger->write(msg, __FUNCTION__);

		return false;
	}

	return true;
}

bool NetMon::removeDriver() {
	TCHAR driverFolder[MAX_PATH] = { 0 };
	if (!ExpandEnvironmentStrings("%windir%\\system32\\drivers\\", driverFolder, MAX_PATH)) {
		m_logger->write("Couldn't expand string", __FUNCTION__);
		return false;
	}
	std::string driverPath = driverFolder + driverName + ".sys";

	if (!DeleteFile(driverPath.c_str())) {
		TCHAR msg[MAX_PATH] = { 0 };
		_snprintf_s(msg, MAX_PATH, "Couldn't remove driver %s. Error: %d", driverPath.c_str(), GetLastError());

		m_logger->write(msg, __FUNCTION__);

		return false;
	}

	return false;
}

NetMon::NetMon() :
	m_logger(nullptr),
	m_netfilter(nullptr),
	m_settings(nullptr),
	m_Init(false),
	m_NetFilterStarted(false)
{
	m_Init = Init();
}

NetMon::~NetMon() {
	Release();
}

bool NetMon::Start() {
	if (m_Init) {
		if (m_NetFilterStarted) {
			m_logger->write("Netfilter already started..", __FUNCTION__);
			return false;
		}

		if (m_netfilter == nullptr && m_settings->isInit()) {
			m_netfilter = new NetFilter(m_settings, m_logger);
		}
		else {
			// write to log
			m_logger->write("m_netfilter already initialized", __FUNCTION__);
			return false;
		}

		std::string certPath(m_settings->certPath());
		std::wstring wsCertPath(certPath.begin(), certPath.end());
		if (!pf_init(m_netfilter, wsCertPath.c_str())) {
			// write to log
			m_logger->write("Couldn't init protocol filter", __FUNCTION__);
			printf_s("[%s] Couldn't init protocol filter\n", __FUNCTION__);
			return false;
		}
		m_logger->write("Protocol filter initialized succcessfully", __FUNCTION__);

		pf_setRootSSLCertSubject("NetFilter");

		// Initialize the library and start filtering thread
		if (nf_init(driverName.c_str(), pf_getNFEventHandler()) != NF_STATUS_SUCCESS) {
			// write to log
			m_logger->write("Couldn't init netfilter", __FUNCTION__);
			printf_s("[%s] Couldn't init netfilter\n", __FUNCTION__);
			return false;
		}
		m_logger->write("Netfilter initialized succcessfully", __FUNCTION__);

		AddNetFilterRules();
	}
	else {
		return false;
	}

	m_NetFilterStarted = true;

	m_logger->write("Netfilter started..", __FUNCTION__);

	return true;
}

void NetMon::Stop() {
	if (m_NetFilterStarted) {
		pf_free();
		nf_free();

		if (m_netfilter != nullptr) {
			delete m_netfilter;
			m_netfilter = nullptr;
		}

		m_logger->write("Netfilter stopped..", __FUNCTION__);

		m_NetFilterStarted = false;
	}
	else {
		m_logger->write("Netfilter already stopped..", __FUNCTION__);
	}
}

void NetMon::RefreshSettings() {
	if (!m_settings->readSettings()) {
		m_logger->write("Couldn't read settings", __FUNCTION__);
		return;
	}

	//Refresing filter rules
	m_netfilter->refreshFilters();
	m_logger->write("Couldn't refresh filters", __FUNCTION__);
}
