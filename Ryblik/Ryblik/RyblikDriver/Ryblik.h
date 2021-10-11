#pragma once
#include<ntifs.h>
#include"RequestFilter.h"

#define DRIVER_HOOK_MAX_COUNT 0x800

typedef struct _DEVICE_MAP* PDEVICE_MAP;

typedef struct _OBJECT_DIRECTORY_ENTRY
{
	_OBJECT_DIRECTORY_ENTRY* ChainLink;
	PVOID Object;
	ULONG HashValue;
} OBJECT_DIRECTORY_ENTRY, * POBJECT_DIRECTORY_ENTRY;

typedef struct _OBJECT_DIRECTORY
{
	POBJECT_DIRECTORY_ENTRY HashBuckets[37];
	EX_PUSH_LOCK Lock;
	PDEVICE_MAP DeviceMap;
	ULONG SessionId;
	PVOID NamespaceEntry;
	ULONG Flags;
} OBJECT_DIRECTORY, * POBJECT_DIRECTORY;

typedef struct _DRIVER_HOOK
{
	UINT8 active;
	PDRIVER_OBJECT driver;
	PDEVICE_OBJECT device;
	UINT8 intercept;
	UINT8 debugLog;
	UINT8 debugBreak;
	UINT32 fileLog;
	UINT32 debugBreakPid;
	UINT32 debugBreakCode;
	PDRIVER_DISPATCH originalHandler;
}DRIVER_HOOK, * PDRIVER_HOOK;

typedef struct _FILE_LOG_RECORD
{
	UINT16 driverNameLen;
	UINT16 deviceNameLen;
	UINT32 code;
	UINT32 inputSize;
	UINT32 outputSize;
}FILE_LOG_RECORD, * PFILE_LOG_RECORD;

enum HOOK_CONF_TYPE
{
	dbgBreak = 1,
	dbgLog = 2,
	fileLog = 3,
	fuzz = 4,
	dbgBreakPid = 5,
	dbgBreakCode = 6
};

class Ryblik
{
private:
	static DRIVER_HOOK hooks[DRIVER_HOOK_MAX_COUNT];
	static HANDLE logFile;
	static FAST_MUTEX logHandleLock;
	static UINT8 locked;
	static RequestFilter filter;

	static bool isDriverAddr(PVOID addr);
	static NTSTATUS hook(PDEVICE_OBJECT DeviceObject, PIRP Irp);
	static void lock();
	static void unlock();

public:
	static void init();

	static UINT32 getDriversCount();
	static UINT32 getDriverAddresses(PVOID* dataOut, UINT32 size);	
	static UINT32 getDriverName(PVOID driverAddr, wchar_t* dataOut, UINT32 size);

	static UINT32 getDriverDevicesCount(PVOID driverAddr);
	static UINT32 getDriverDeviceAddresses(PVOID driverAddr, PVOID* dataOut, UINT32 size);
	static UINT32 getDriverDeviceName(PVOID driverAddr, PVOID deviceAddr, wchar_t* dataOut, UINT32 size);
	
	static UINT32 getDriverMajorFunctions(PVOID driverAddr, PVOID* dataOut, UINT32 size);

	static bool hookDriver(PVOID driverAddr, PVOID deviceAddr);
	static bool unhookDriver(PVOID driverAddr, PVOID deviceAddr);
	static bool isHookedDriver(PVOID driverAddr, PVOID deviceAddr);

	static bool setHookConfValue(PVOID driverAddr, PVOID deviceAddr, HOOK_CONF_TYPE type, UINT32 value);
	static UINT32 getHookConfValue(PVOID driverAddr, PVOID deviceAddr, HOOK_CONF_TYPE type);

	static bool startFileLog(PUNICODE_STRING filename);
	static bool stopFileLog();
	static bool isFileLogActivated();
	static bool isAnyHook();
};