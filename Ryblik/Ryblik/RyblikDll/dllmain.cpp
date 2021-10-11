#include "pch.h"
#include "RyblikDriverIO.h"

#ifdef RYBLIKDLL_EXPORTS
#define RYBLIKDLL_API extern "C" __declspec(dllexport)
#else
#define RYBLIKDLL_API extern "C" __declspec(dllimport)
#endif

RyblikDriverIO driver;

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

RYBLIKDLL_API bool init()
{
	return driver.init();
}

RYBLIKDLL_API DWORD getDriverCount()
{
	return driver.getDriverCount();
}

RYBLIKDLL_API DWORD getDriverAddresses(PVOID* data, DWORD size)
{
	return driver.getDriverAddresses(data, size);
}

RYBLIKDLL_API DWORD getDriverName(PVOID addr, wchar_t* data, DWORD size)
{
	return driver.getDriverName(addr, data, size);
}

RYBLIKDLL_API DWORD getDriverDeviceCount(PVOID addr)
{
	return driver.getDriverDeviceCount(addr);
}

RYBLIKDLL_API DWORD getDriverDeviceAddresses(PVOID addr, PVOID* data, DWORD size)
{
	return driver.getDriverDeviceAddresses(addr, data, size);
}

RYBLIKDLL_API DWORD getDriverDeviceName(PVOID addr, PVOID devAddr, wchar_t* data, DWORD size)
{
	return driver.getDriverDeviceName(addr, devAddr, data, size);
}

RYBLIKDLL_API DWORD getDriverMajorFunctions(PVOID addr, PVOID* data, DWORD size)
{
	return driver.getDriverMajorFunctions(addr, data, size);
}

RYBLIKDLL_API DWORD devCtrlReq(wchar_t* driver, DWORD access, DWORD code, PVOID inBuffer, DWORD inBufferSize, PVOID outBuffer, DWORD outBufferSize)
{
	wchar_t name[256] = L"\\\\?\\GLOBALROOT";
	wcscat_s(name, 256, driver);
	HANDLE handle = CreateFile(name, access, 0, NULL, OPEN_EXISTING, 0, NULL);
	if (handle == INVALID_HANDLE_VALUE)
	{
		return -1;
	}
	if (DeviceIoControl(handle, code, inBuffer, inBufferSize, outBuffer, outBufferSize, &outBufferSize, NULL))
	{
		return outBufferSize;
	}
	return -1;
}

RYBLIKDLL_API bool hookDriver(PVOID drv, PVOID dev)
{
	return driver.hookDriver(drv, dev);
}

RYBLIKDLL_API bool unhookDriver(PVOID drv, PVOID dev)
{
	return driver.unhookDriver(drv, dev);
}

RYBLIKDLL_API bool isHookedDriver(PVOID drv, PVOID dev)
{
	return driver.isHookedDriver(drv, dev);
}

RYBLIKDLL_API bool setHookConfValue(PVOID drv, PVOID dev, UINT32 type, UINT32 value)
{
	return driver.setHookConfValue(drv, dev, type, value);
}

RYBLIKDLL_API UINT32 getHookConfValue(PVOID drv, PVOID dev, UINT32 type)
{
	return driver.getHookConfValue(drv, dev, type);
}

RYBLIKDLL_API bool startFileLog(wchar_t* filename, DWORD len)
{
	return driver.startFileLog(filename, len);
}

RYBLIKDLL_API bool stopFileLog()
{
	return driver.stopFileLog();
}

RYBLIKDLL_API bool isFileLogActivated()
{
	return driver.isFileLogActivated();
}
