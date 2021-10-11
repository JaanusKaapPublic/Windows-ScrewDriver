#pragma once
#include<Windows.h>
#include"SharedDefs.h"

class RyblikDriverIO
{
private:
	HANDLE handle;

public:
	bool init();
	bool devIOctrl(DWORD code, PVOID inBuffer, DWORD inBufferSize, PVOID outBuffer, DWORD* outBufferSize);
	DWORD getDriverCount();
	DWORD getDriverAddresses(PVOID* data, DWORD size);
	DWORD getDriverName(PVOID addr, wchar_t* data, DWORD size);
	DWORD getDriverDeviceCount(PVOID addr);
	DWORD getDriverDeviceAddresses(PVOID addr, PVOID* data, DWORD size);
	DWORD getDriverDeviceName(PVOID addr, PVOID devAddr, wchar_t* data, DWORD size);

	DWORD getDriverMajorFunctions(PVOID addr, PVOID* data, DWORD size);

	bool hookDriver(PVOID drv, PVOID dev);
	bool unhookDriver(PVOID drv, PVOID dev);
	bool isHookedDriver(PVOID drv, PVOID dev);

	bool setHookConfValue(PVOID drv, PVOID dev, UINT32 type, UINT32 value);
	UINT32 getHookConfValue(PVOID drv, PVOID dev, UINT32 type);

	bool startFileLog(wchar_t* filename, DWORD len);
	bool stopFileLog();
	bool isFileLogActivated();
};
