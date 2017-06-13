#pragma once

class AuxiliaryFuncs {
private:
	AuxiliaryFuncs();
	~AuxiliaryFuncs();
public:
	static DWORD getProcessName(DWORD pid, std::wstring& processName, bool fullName = false);
	static std::wstring getTimeStamp();
	static bool forceDirectories(const std::wstring & aPath);
	static void toLower(std::string& str);
};

