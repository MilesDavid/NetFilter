// NetFilter.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include "NetMon.h"

#define ENABLE_CONSOLE

#ifdef ENABLE_CONSOLE

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

	return 0;
}

#else
bool __cdecl createNetFilter() {

}
#endif

