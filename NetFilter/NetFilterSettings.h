#pragma once
#include "stdafx.h"
#include "Logger.h"

class NetFilterSettings : public Noncopyable {
private:
	std::set<std::string> m_trackingProcesses;
	bool m_selfSigned;
	std::string m_dumpPath;
	std::string m_certPath;
	std::string m_configPath;

	Logger* m_logger;
	bool m_Init;

	bool Init();
public:
	NetFilterSettings(Logger* logger);
	~NetFilterSettings();

	const bool findProcess(const std::string& processName) const {
		return m_trackingProcesses.find(processName) != m_trackingProcesses.end();
	}
	const bool selfSigned() const {
		return m_selfSigned;
	}
	const std::string dumpPath() const {
		return m_dumpPath;
	}
	const std::string certPath() const {
		return m_certPath;
	}
	const bool isInit() const {
		return m_Init;
	}

	bool readSettings();
	std::string toString();
};

