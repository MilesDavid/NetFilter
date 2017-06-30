// NetFilter.cpp: ���������� ����� ��//

#include "stdafx.h"
#include "NetMon.h"

#ifndef ENABLE_CONSOLE

#define NETMON_LIB
#ifdef NETMON_LIB
#define NETMON_API extern __declspec(dllexport)
#else
#define NETMON_API __declspec(dllimport)
#endif

#ifdef __cplusplus
#define NETMON_CC __cdecl
extern "C" {
#else
#define NETMON_CC
#endif // __cplusplus
	NETMON_API NetMon* NETMON_CC NetMonCreate() { return new(std::nothrow) NetMon(); }
	NETMON_API void NETMON_CC NetMonFree(NetMon* netMon) {
		if (netMon) {
			delete netMon;
			netMon = nullptr;
		}
	}

	NETMON_API int NETMON_CC NetMonStart(NetMon* netMon) {
		return (netMon && netMon->Start()) ? 1 : 0;
	}
	NETMON_API int NETMON_CC NetMonIsStarted(NetMon* netMon) {
		return (netMon && netMon->NetfilterStarted()) ? 1 : 0;
	}
	NETMON_API void NETMON_CC NetMonStop(NetMon* netMon) { if (netMon) netMon->Stop(); }
	NETMON_API void NETMON_CC NetMonRefreshSettings(NetMon* netMon) { if (netMon) netMon->RefreshSettings(); }
	NETMON_API void NETMON_CC NetMonLogPath(NetMon* netMon, char* buf, size_t size) {
		if (netMon) { strcpy_s(buf, size, netMon->LogPath().c_str()); }
}

#pragma region Dump folders

	NETMON_API void NETMON_CC NetMonDeleteHttpRequestDumpFolder(NetMon* netMon) {
		if (netMon) { netMon->deleteHttpRequestDumpFolder(); }
	}
	NETMON_API void NETMON_CC NetMonDeleteHttpResponseDumpFolder(NetMon* netMon) {
		if (netMon) { netMon->deleteHttpResponseDumpFolder(); }
	}
	NETMON_API void NETMON_CC NetMonDeleteRawInDumpFolder(NetMon* netMon) {
		if (netMon) { netMon->deleteRawInDumpFolder(); }
	}
	NETMON_API void NETMON_CC NetMonDeleteRawOutDumpFolder(NetMon* netMon) {
		if (netMon) { netMon->deleteRawOutDumpFolder(); }
	}

#pragma endregion

#ifdef __cplusplus
}
#endif // __cplusplus

#else

int main() {
	NetMon* netMon = new NetMon();
	if (netMon->Start()) {
		std::cout << "NetFilter started.." << std::endl;
	}
	else {
		std::cout << "Error during starting NetFilter.." << std::endl;
	}

	SleepEx(INFINITE, FALSE);

	delete netMon;
	netMon = nullptr;
}

#endif

