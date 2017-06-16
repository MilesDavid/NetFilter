#include "stdafx.h"
#include "Logger.h"
#include "AuxiliaryFuncs.h"


bool Logger::Init() {
	InitializeCriticalSection(&m_guard);

	std::ios_base::openmode mode = (m_rewrite) ? std::ios::app | std::ios::out : std::ios::out;
	m_logFile.open(m_logName, mode);
	if (m_logFile.is_open()) {
		write("Logger started succcessfully", __FUNCTION__);
		return true;
	}

	return false;
}

void Logger::Release() {
	DeleteCriticalSection(&m_guard);
	if (m_Init) {
		m_logFile.close();
	}
}

void Logger::lock() {
	EnterCriticalSection(&m_guard);
}

void Logger::unlock() {
	LeaveCriticalSection(&m_guard);
}

Logger::Logger(const std::string& fileName, bool rewrite) :
	m_guard(),
	m_Init(false),
	m_logName(fileName),
	m_logFile(),
	m_rewrite(rewrite)
{
	m_Init = Init();
}

Logger::~Logger() {
	Release();
}
void Logger::write(const std::string& buf, const char* caller) {
	lock();

	TCHAR tmp[1024] = { 0 };
	_snprintf_s(tmp, 1024, "%s [%s] %s\n", AuxiliaryFuncs::getTimeStamp("%04d.%02d.%02d %02d:%02d:%02d.%03d").c_str(), caller, buf.c_str());
	std::string line = tmp;

	m_logFile.write(line.c_str(), line.size());
	m_logFile.flush();

	unlock();
}
