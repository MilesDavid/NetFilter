#include "stdafx.h"
#include "NetFilterSettings.h"
#include "AuxiliaryFuncs.h"


NetFilterSettings::NetFilterSettings() : m_trackingProcesses(), m_dumpPath(), m_certPath() {
	HMODULE hModule = GetModuleHandleW(NULL);
	WCHAR currentPath[MAX_PATH];
	GetModuleFileNameW(hModule, currentPath, MAX_PATH);

	WCHAR currentDisk[MAX_PATH];
	WCHAR currentDir[MAX_PATH];
	_wsplitpath_s(currentPath, currentDisk, MAX_PATH, currentDir, MAX_PATH, nullptr, 0, nullptr, 0);

	m_dumpPath = std::wstring(currentDisk) + std::wstring(currentDir) + L"dump";
	m_certPath = std::wstring(currentDisk) + std::wstring(currentDir) + L"cert";
	m_configPath = std::wstring(currentDisk) + std::wstring(currentDir) + L"settings.conf";
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

		json jsonObj;
		try {
			jsonObj = json::parse(content.c_str());

			for (const auto elem : jsonObj["tracingProcesses"]) {
				if (elem.is_string()) {
					std::string processPath = elem.get<std::string>();
					AuxiliaryFuncs::toLower(processPath);
					m_trackingProcesses.insert(processPath);
				}
			}
		}
		catch (const std::exception& e) {
			// write to log
			printf_s("Couldn't parse json. Error: %s\n", e.what());
			return false;
		}

		configFile.close();
	}

	return m_trackingProcesses.size() != 0;
}
