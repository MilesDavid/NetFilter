#include "stdafx.h"
#include "AuxiliaryFuncs.h"


AuxiliaryFuncs::AuxiliaryFuncs() {}

AuxiliaryFuncs::~AuxiliaryFuncs() {}

DWORD AuxiliaryFuncs::getProcessName(DWORD pid, std::wstring& processName, bool fullName) {

	HANDLE hProcess = OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION | PROCESS_VM_READ, FALSE, pid);
	if (hProcess != NULL) {
		TCHAR processFullName[MAX_PATH];
		if (GetModuleFileNameEx(hProcess, NULL, processFullName, MAX_PATH)) {
			if (!fullName) {
				TCHAR name[MAX_PATH];
				TCHAR ext[MAX_PATH];
				_wsplitpath_s(processFullName, nullptr, 0, nullptr, 0, name, MAX_PATH, ext, MAX_PATH);
				processName += name;
				processName += ext;
			}
			else {
				processName = processFullName;
			}
		}
		else {
			CloseHandle(hProcess);
			return GetLastError();
		}

		CloseHandle(hProcess);
	}
	else {
		return GetLastError();
	}

	return ERROR_SUCCESS;
}

std::wstring AuxiliaryFuncs::getTimeStamp() {

	SYSTEMTIME time;
	GetLocalTime(&time);

	wchar_t timeString[64];
	swprintf_s(timeString, 64, L"%04d-%02d-%02d-%02d-%02d-%02d-%03d", time.wYear, time.wMonth, time.wDay,
		time.wHour, time.wMinute, time.wSecond, time.wMilliseconds);

	return timeString;
}

bool AuxiliaryFuncs::forceDirectories(const std::wstring& aPath) {
	TCHAR* temp = _tcsdup(aPath.c_str());
	bool done = false;

	for (TCHAR* p = temp; *p != 0; ++p) {
		if (*p != '\\')
			continue;

		*p = 0;
		DWORD attrs = GetFileAttributes(temp);
		if (attrs == INVALID_FILE_ATTRIBUTES) {
			if (::CreateDirectory(temp, NULL) == 0 && GetLastError() != ERROR_ALREADY_EXISTS) {
				goto out;
			}
		}
		else if ((attrs & FILE_ATTRIBUTE_DIRECTORY) == 0) {
			goto out;
		}
		*p = '\\';
	}

	done = ::CreateDirectory(temp, NULL) != 0 || GetLastError() == ERROR_ALREADY_EXISTS;

out:
	free(temp);
	return done;
}

void AuxiliaryFuncs::toLower(std::string & str) {
	for (auto& sym : str) {
		sym = tolower(sym);
	}
}

