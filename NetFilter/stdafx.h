// stdafx.h: включаемый файл дл€ стандартных системных включаемых файлов
// или включаемых файлов дл€ конкретного проекта, которые часто используютс€, но
// не часто измен€ютс€
//

#pragma once

#include "targetver.h"

#include <stdio.h>
#include <tchar.h>

#define NOMINMAX

#pragma region Windows sockets
	#define _WINSOCKAPI_
	#define _WINSOCK_DEPRECATED_NO_WARNINGS
	#include <WinSock2.h>
	#include <ws2tcpip.h>
#pragma endregion

#include <Windows.h>
#include <Psapi.h>
#include <Shlwapi.h>

#pragma region std c++ library
	#include <string>
	#include <iostream>
	#include <vector>
	#include <map>
	#include <set>
	#include <fstream>
#pragma endregion

#pragma region NetFilter SDK 2 + ProtocolFilter
	#include "nfapi.h"
	#include "ProtocolFilters.h"
#pragma endregion

#pragma region JSON
	#include "json.hpp"
	using nlohmann::json;
#pragma endregion

#pragma region Misc
	#include "Noncopyable.h"
#pragma endregion