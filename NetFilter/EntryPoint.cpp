// NetFilter.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include "NetMon.h"

#ifndef ENABLE_CONSOLE

#define NETMON_LIB
#ifdef NETMON_LIB
#define NETMON_EXPORT extern __declspec(dllexport)
#else
#define NETMON_EXPORT __declspec(dllimport)
#endif

#ifdef __cplusplus
extern "C" {
#endif // __cplusplus
	NETMON_EXPORT NetMon* __cdecl NetMonCreate() { return new(std::nothrow) NetMon(); }
	NETMON_EXPORT void __cdecl NetMonFree(NetMon* netMon) { if (netMon) delete netMon; }
	NETMON_EXPORT bool __cdecl NetMonStart(NetMon* netMon) { return (netMon) ? netMon->Start() : false; }
	NETMON_EXPORT bool __cdecl NetMonIsStarted(NetMon* netMon) { return (netMon) ? netMon->NetfilterStarted() : false; }
	NETMON_EXPORT void __cdecl NetMonStop(NetMon* netMon) { if (netMon) netMon->Stop(); }
	NETMON_EXPORT void __cdecl NetMonRefreshSettings(NetMon* netMon) { if (netMon) netMon->RefreshSettings(); }
	NETMON_EXPORT void __cdecl NetMonLogPath(NetMon* netMon, char* buf, size_t size) {
		if (netMon) { strcpy_s(buf, size, netMon->LogPath().c_str()); }
	}

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

