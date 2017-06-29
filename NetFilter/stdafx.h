// stdafx.h: включаемый файл дл€ стандартных системных включаемых файлов
// или включаемых файлов дл€ конкретного проекта, которые часто используютс€, но
// не часто измен€ютс€
//

#pragma once

#include "targetver.h"

#include <stdio.h>
#include <tchar.h>

#define NOMINMAX

#define ENABLE_NLOHMANN_JSON

//#define FILTER_ANY_PROCESS
//#define ENABLE_CONSOLE
//#define ENABLE_DRIVER_COPYING
#define UNPACK_GZIP_CONTENT

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
	#ifdef ENABLE_NLOHMANN_JSON
		#include "json.hpp"
		using nlohmann::json;
	#endif // NLOHMANN_JSON
#pragma endregion

#pragma region Misc
	#include "Noncopyable.h"
#pragma endregion

#ifdef FUTURE
	typedef std::basic_string<TCHAR, std::char_traits<TCHAR> > tstring;

	#ifdef UNICODE
		#define to_lower(x) towlower(x)
		#define tprintf_s(...) wprintf_s(...)
	#else
		#define to_lower(x) tolower(x)
		#define tprintf_s(...) printf_s(...)
	#endif
#endif