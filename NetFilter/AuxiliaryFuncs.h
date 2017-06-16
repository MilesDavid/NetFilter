#pragma once

class AuxiliaryFuncs {
private:
	AuxiliaryFuncs();
	~AuxiliaryFuncs();
public:
	static DWORD getProcessName(DWORD pid, std::string& processName, bool fullName = false);
	static std::string getTimeStamp(const std::string& format);
	static bool forceDirectories(const std::string & aPath);
	static void toLower(std::string& str);
	static BOOL AuxiliaryFuncs::IsWow64();
	static bool AuxiliaryFuncs::getCurrentFile(std::string& disk, std::string& folder, std::string& file, std::string& ext);
};

