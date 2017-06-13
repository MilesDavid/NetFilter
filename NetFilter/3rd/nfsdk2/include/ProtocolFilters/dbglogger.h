//
// 	NetFilterSDK 
// 	Copyright (C) 2009 Vitaly Sidorov
//	All rights reserved.
//
//	This file is a part of the NetFilter SDK.
//	The code and information is provided "as-is" without
//	warranty of any kind, either expressed or implied.
//


#if !defined(_DBGLOGGER_H)
#define _DBGLOGGER_H

#include <stdlib.h>
#include <stdarg.h>
#include <windows.h>
#include <string>
#include <time.h>
#include "sync.h"

#pragma warning (disable:4996)

using std::string;

#if !defined(_DEBUG) && !defined(_RELEASE_LOG)
#define DbgPrint
#else
#define DbgPrint DBGLogger::Instance().WriteInfo
#endif

class DBGLogger
{
public:

	virtual ~DBGLogger() {}
	
	static DBGLogger dbgLog;

	static DBGLogger& Instance()
	{
		return dbgLog;
	}

	void PutEnable(bool bEnable)
	{
		ProtocolFilters::AutoLock cs(m_cs);
		m_bEnable = bEnable;
	}

	bool IsEnable() 
	{
		ProtocolFilters::AutoLock cs(m_cs);
		return m_bEnable;
	}

	void PutFileName(string sFileName)
	{
		m_sFileName = sFileName;
	}

	string GetFileName() const
	{
		return m_sFileName;
	}

	void WriteInfo(const char*  sMessage, ...)
	{
		ProtocolFilters::AutoLock cs(m_cs);

		if (!IsEnable())
		{
			return;
		}

		int nSize = BUFSIZ;
		string sBuffer;
		va_list args;

		va_start(args, sMessage);
		{
			do
			{
				sBuffer.resize(nSize);
				memset(&sBuffer[0], 0, sBuffer.length());

				int nResult = _vsnprintf(const_cast<char*>(sBuffer.data()),
					sBuffer.length(), sMessage, args);

				if (nResult < 0)
				{
					nSize *= 2;
				}
				else
				{
					sBuffer.resize(nResult);

					sBuffer = GetTime() + string(": ") + sBuffer + string("\r\n");

					break;
				}
			}
			while (nSize);
		}
		va_end(args);

		char *p;
		if (p = strstr((char*)sBuffer.c_str(), "PASS "))
		{
			for (int i = (int)(p - sBuffer.c_str() + 5); i < (int)sBuffer.length() - 2; i++)
			{
				sBuffer[i] = '*';
			}
		}

		Write(sBuffer);
	}


	string GenerateFileName(string sName) const
	{
		size_t nSize = MAX_PATH;
		string sPath;

		do
		{
			sPath.resize(nSize);

			nSize = GetModuleFileName(NULL, 
				const_cast<char*>(sPath.data()),
				(DWORD)(sPath.length() - 1));
		
			if (nSize >= sPath.length() - 2)
			{
				nSize *= 2;
			}
			else
			{
				sPath.resize(nSize);

				size_t nIndex = sPath.rfind("\\");
				if (nIndex != string::npos && nIndex > 0)
				{
					sPath.resize(nIndex);
				}
				else
				{
					sPath = "";
				}

				break;
			}
		} 
		while (nSize);

		SYSTEMTIME systemTime;

		GetSystemTime(&systemTime);

		char sValue[BUFSIZ];

		string sFileName = sName;
		
		if (systemTime.wMonth < 10)
		{
			sFileName += "0";
		}

		_ltoa(systemTime.wMonth, sValue, 10);
		sFileName += sValue;

		if (systemTime.wDay < 10)
		{
			sFileName += "0";
		}
		_ltoa(systemTime.wDay, sValue, 10);
		sFileName += sValue;

		_ltoa(systemTime.wYear, sValue, 10);
		sFileName += sValue;

		sFileName += "-";
		
		for (size_t i = 0;;i++)
		{
			WIN32_FIND_DATA findFileData;
			HANDLE hFind;
			char sValue[BUFSIZ];
			string sTempName = sFileName;

			_ltoa((long)i, sValue, 10);

			sTempName += sValue;
			sTempName += ".lst";

			sTempName = sPath + string("\\") + sTempName;

			hFind = FindFirstFile(sTempName.c_str(), &findFileData);
			if (hFind == INVALID_HANDLE_VALUE) 
			{
				sFileName = sTempName;

				break;
			} 
			else
			{
				FindClose(hFind);
			}
		}

		return sFileName;
	}

	string GetTime() const
	{
		string sTime;
		time_t tNow = time(NULL);
		struct tm* ptm = localtime(&tNow);

		if (ptm != NULL)
		{
			sTime = asctime(ptm);

			size_t nIndex = sTime.find_first_of("\r\n");
			if (nIndex != string::npos)
			{
				sTime.resize(nIndex);
			}
		}

		return sTime;
	}

private:
	bool m_bEnable;
	string m_sFileName;
	ProtocolFilters::AutoCriticalSection m_cs;

	DBGLogger() : m_bEnable(false) {}

	void Write(const string& sBuffer) 
	{
		if (!IsEnable())
		{
			return;
		}

		FILE* pFile = Open();
		if (pFile)
		{
			fwrite(sBuffer.c_str(), sizeof(char), sBuffer.length(), pFile);
			Close(pFile);
		}
	}

	FILE* Open() 
	{
		if (!IsEnable())
		{
			return NULL;
		}

		FILE* pFile = fopen(m_sFileName.c_str(), "ab");
		if (pFile == NULL)
		{
			pFile = fopen(m_sFileName.c_str(), "wb");
		}

		return pFile;
	}

	void Close(FILE* pFile) const
	{
		if (pFile != NULL)
		{
			fclose(pFile);
			pFile = NULL;
		}
	}
};


#endif // !defined(_DBGLOGGER_H)
