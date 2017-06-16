#pragma once
class Logger : public Noncopyable {
private:
	CRITICAL_SECTION m_guard;
	bool m_Init;
	const std::string m_logName;
	std::fstream m_logFile;
	bool m_rewrite;

	bool Init();
	void Release();

	void lock();
	void unlock();
public:
	Logger(const std::string& fileName, bool rewrite=false);
	~Logger();

	void write(const std::string& buf, const char* caller);
	const bool isInit() const {
		return m_Init;
	}
};

