#include "stdafx.h"
#include "NetFilterSettings.h"
#include "AuxiliaryFuncs.h"

bool NetFilterSettings::Init() {
	HMODULE hModule = GetModuleHandleW(NULL);
	TCHAR currentPath[MAX_PATH];

	if (!GetModuleFileName(hModule, currentPath, MAX_PATH)) {
		// write to log
		TCHAR msg[MAX_PATH] = { 0 };
		_snprintf_s(msg, MAX_PATH, "Error during calling GetModuleFileName, error: %d", GetLastError());

		m_logger->write(msg, __FUNCTION__);

		return false;
	}

	TCHAR currentDisk[MAX_PATH];
	TCHAR currentDir[MAX_PATH];
	_splitpath_s(currentPath, currentDisk, MAX_PATH, currentDir, MAX_PATH, nullptr, 0, nullptr, 0);

	m_dumpPath = std::string(currentDisk) + std::string(currentDir) + "dump";
	m_certPath = std::string(currentDisk) + std::string(currentDir) + "cert";
	m_configPath = std::string(currentDisk) + std::string(currentDir) + "settings.conf";

}

NetFilterSettings::NetFilterSettings(Logger* logger) :
	m_trackingProcesses(),
	m_selfSigned(false),
	m_dumpPath(),
	m_certPath(),
	m_logger(logger),
	m_Init(false)
{
	m_Init = Init();
}

NetFilterSettings::~NetFilterSettings() {}

bool NetFilterSettings::readSettings() {
	if (PathFileExists(m_configPath.c_str()) == FALSE) {
		return false;
	}

	std::fstream configFile;
	configFile.open(m_configPath, std::ios::in);
	if (configFile.is_open()) {
		std::string content;
		char ch;
		while (configFile.get(ch)) {
			content += ch;
		}

		configFile.close();
#ifdef ENABLE_NLOHMANN_JSON
		json jsonObj;
		try {
			jsonObj = json::parse(content.c_str());

			for (const auto elem : jsonObj["TracingProcesses"]) {
				if (elem.is_string()) {
					std::string processPath = elem.get<std::string>();
					AuxiliaryFuncs::toLower(processPath);
					m_trackingProcesses.insert(processPath);
				}
			}

			if (jsonObj["CertSelfSigned"].is_boolean()) {
				m_selfSigned = jsonObj["CertSelfSigned"];
			}
		}
		catch (const std::exception& e) {
			// write to log
			TCHAR msg[MAX_PATH] = { 0 };
			_snprintf_s(msg, MAX_PATH, "Couldn't parse json. Error: %s", e.what());

			m_logger->write(msg, __FUNCTION__);

			printf_s("Couldn't parse json. Error: %s\n", e.what());
			return false;
		}
#else
		// TODO: Add python unicode json parser
#endif // ENABLE_NLOHMANN_JSON

	}

	return m_trackingProcesses.size() != 0;
}
