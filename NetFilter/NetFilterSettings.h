#pragma once
#include "stdafx.h"

class NetFilterSettings : public Noncopyable {
private:
	std::set<std::string> m_trackingProcesses;
	std::wstring m_dumpPath;
	std::wstring m_certPath;
	std::wstring m_configPath;
public:
	NetFilterSettings();
	~NetFilterSettings();

	bool findProcess(std::string processName) const {
		return m_trackingProcesses.find(processName) != m_trackingProcesses.end();
	}
	const std::wstring dumpPath() const {
		return m_dumpPath;
	}
	const std::wstring certPath() const {
		return m_certPath;
	}

	bool readSettings();
};

