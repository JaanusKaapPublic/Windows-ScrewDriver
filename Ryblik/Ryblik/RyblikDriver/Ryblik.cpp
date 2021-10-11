#include "Ryblik.h"
#include "Utils.h"
#include "DriversUtils.h"

DRIVER_HOOK Ryblik::hooks[DRIVER_HOOK_MAX_COUNT];
HANDLE Ryblik::logFile = NULL;
FAST_MUTEX Ryblik::logHandleLock;
UINT8 Ryblik::locked = 0;


void Ryblik::init()
{
	memset(hooks, 0, sizeof(hooks));
	ExInitializeFastMutex(&logHandleLock);
}

void Ryblik::lock()
{
	while (true)
	{
		ExAcquireFastMutex(&logHandleLock);
		if(!locked)
		{
			locked = 1;
			ExReleaseFastMutex(&logHandleLock);
			break;
		}
		ExReleaseFastMutex(&logHandleLock);
	}
}

void Ryblik::unlock()
{
	ExAcquireFastMutex(&logHandleLock);
	locked = 0;
	ExReleaseFastMutex(&logHandleLock);
}

bool Ryblik::isDriverAddr(PVOID addr)
{
	HANDLE handle{};
	OBJECT_ATTRIBUTES attributes{};
	UNICODE_STRING directory_name{};
	PVOID directory{};
	PDRIVER_OBJECT driver;

	RtlInitUnicodeString(&directory_name, L"\\Driver");
	InitializeObjectAttributes(&attributes, &directory_name, OBJ_CASE_INSENSITIVE, NULL, NULL);
	NTSTATUS status = ZwOpenDirectoryObject(&handle, DIRECTORY_ALL_ACCESS, &attributes);
	if (!NT_SUCCESS(status))
		return false;
	status = ObReferenceObjectByHandle(handle, DIRECTORY_ALL_ACCESS, nullptr, KernelMode, &directory, nullptr);
	if (!NT_SUCCESS(status))
	{
		ZwClose(handle);
		return false;
	}

	POBJECT_DIRECTORY directory_object = POBJECT_DIRECTORY(directory);
	ExAcquirePushLockExclusiveEx(&directory_object->Lock, 0);

	for (POBJECT_DIRECTORY_ENTRY entry : directory_object->HashBuckets)
	{
		if (entry == NULL)
			continue;
		while (entry != nullptr && entry->Object != nullptr)
		{
			driver = PDRIVER_OBJECT(entry->Object);
			if (driver == addr)
			{
				ExReleasePushLockExclusiveEx(&directory_object->Lock, 0);
				ObDereferenceObject(directory);
				ZwClose(handle);
				return true;
			}
			entry = entry->ChainLink;
		}
	}

	ExReleasePushLockExclusiveEx(&directory_object->Lock, 0);
	ObDereferenceObject(directory);
	ZwClose(handle);
	return false;
}

UINT32 Ryblik::getDriversCount()
{
	UINT32 count = 0;
	HANDLE handle{};
	OBJECT_ATTRIBUTES attributes{};
	UNICODE_STRING directory_name{};
	PVOID directory{};
	PDRIVER_OBJECT driver;

	RtlInitUnicodeString(&directory_name, L"\\Driver");
	InitializeObjectAttributes(&attributes, &directory_name, OBJ_CASE_INSENSITIVE, NULL, NULL);
	NTSTATUS status = ZwOpenDirectoryObject(&handle, DIRECTORY_ALL_ACCESS, &attributes);
	if (!NT_SUCCESS(status))
		return 0xFFFFFFFF;;
	status = ObReferenceObjectByHandle(handle, DIRECTORY_ALL_ACCESS, nullptr, KernelMode, &directory, nullptr);
	if (!NT_SUCCESS(status)) 
	{
		ZwClose(handle);
		return 0xFFFFFFFF;
	}

	POBJECT_DIRECTORY directory_object = POBJECT_DIRECTORY(directory);
	ExAcquirePushLockExclusiveEx(&directory_object->Lock, 0);

	for (POBJECT_DIRECTORY_ENTRY entry : directory_object->HashBuckets)
	{
		if (entry == NULL)
			continue;
		while (entry != nullptr && entry->Object != nullptr)
		{
			count++;
			driver = PDRIVER_OBJECT(entry->Object);
			entry = entry->ChainLink;
		}
	}

	ExReleasePushLockExclusiveEx(&directory_object->Lock, 0);
	ObDereferenceObject(directory);
	ZwClose(handle);
	return count;
}

UINT32 Ryblik::getDriverAddresses(PVOID* dataOut, UINT32 size)
{
	UINT32 count = 0;
	HANDLE handle{};
	OBJECT_ATTRIBUTES attributes{};
	UNICODE_STRING directory_name{};
	PVOID directory{};
	PDRIVER_OBJECT driver;

	RtlInitUnicodeString(&directory_name, L"\\Driver");
	InitializeObjectAttributes(&attributes, &directory_name, OBJ_CASE_INSENSITIVE, NULL, NULL);
	NTSTATUS status = ZwOpenDirectoryObject(&handle, DIRECTORY_ALL_ACCESS, &attributes);
	if (!NT_SUCCESS(status))
		return 0;
	status = ObReferenceObjectByHandle(handle, DIRECTORY_ALL_ACCESS, nullptr, KernelMode, &directory, nullptr);
	if (!NT_SUCCESS(status))
	{
		ZwClose(handle);
		return 0;
	}

	POBJECT_DIRECTORY directory_object = POBJECT_DIRECTORY(directory);
	ExAcquirePushLockExclusiveEx(&directory_object->Lock, 0);

	for (POBJECT_DIRECTORY_ENTRY entry : directory_object->HashBuckets)
	{
		if (entry == NULL)
			continue;
		while (entry != nullptr && entry->Object != nullptr)
		{
			driver = PDRIVER_OBJECT(entry->Object);
			entry = entry->ChainLink;
			if ((count + 1) * sizeof(PVOID) <= size)
				dataOut[count++] = driver;
		}
	}

	ExReleasePushLockExclusiveEx(&directory_object->Lock, 0);
	ObDereferenceObject(directory);
	ZwClose(handle);
	return count * sizeof(PVOID);
}

UINT32 Ryblik::getDriverName(PVOID driverAddr, wchar_t* dataOut, UINT32 size)
{
	if (!isDriverAddr(driverAddr))
		return 0xFFFFFFFF;
	PDRIVER_OBJECT driver = PDRIVER_OBJECT(driverAddr);
	if(driver->DriverName.Length > size)
		return 0xFFFFFFFF;
	memcpy(dataOut, driver->DriverName.Buffer, driver->DriverName.Length);
	return driver->DriverName.Length;
}

UINT32 Ryblik::getDriverDevicesCount(PVOID driverAddr)
{
	if (!isDriverAddr(driverAddr))
		return 0xFFFFFFFF;
	UINT32 count = 0;
	PDRIVER_OBJECT driver = PDRIVER_OBJECT(driverAddr);

	PDEVICE_OBJECT dev = driver->DeviceObject;
	while (dev != NULL)
	{
		count++;
		dev = dev->NextDevice;
	}
	return count;
}

UINT32 Ryblik::getDriverDeviceAddresses(PVOID driverAddr, PVOID* dataOut, UINT32 size)
{
	if (!isDriverAddr(driverAddr))
		return 0xFFFFFFFF;
	UINT32 count = 0;
	PDRIVER_OBJECT driver = PDRIVER_OBJECT(driverAddr);

	PDEVICE_OBJECT dev = driver->DeviceObject;
	while (dev != NULL)
	{
		if ((count + 1) * sizeof(PVOID) <= size)
			dataOut[count++] = dev;
		dev = dev->NextDevice;
	}
	return count * sizeof(PVOID);
}

UINT32 Ryblik::getDriverDeviceName(PVOID driverAddr, PVOID deviceAddr, wchar_t* dataOut, UINT32 size)
{
	if (!isDriverAddr(driverAddr))
		return 0xFFFFFFFF;
	PDRIVER_OBJECT driver = PDRIVER_OBJECT(driverAddr);

	PDEVICE_OBJECT dev = driver->DeviceObject;
	while (dev != NULL)
	{
		if (dev == deviceAddr)
		{
			ULONG ret;
			POBJECT_NAME_INFORMATION nameInfo;
			nameInfo = (POBJECT_NAME_INFORMATION)ExAllocatePoolWithQuotaTag(PagedPool, size, 'rblk');
			memset(nameInfo, 0, size);
			NTSTATUS stat = ObQueryNameString(dev, nameInfo, size, &ret);
			if (stat == STATUS_SUCCESS)
			{
				ret = nameInfo->Name.Length;
				memcpy(dataOut, nameInfo->Name.Buffer, nameInfo->Name.Length);
				ExFreePool(nameInfo);
				return ret;
			}
			ExFreePool(nameInfo);
			return 0xFFFFFFFF;
		}
		dev = dev->NextDevice;
	}
	return 0xFFFFFFFF;
}

UINT32 Ryblik::getDriverMajorFunctions(PVOID driverAddr, PVOID* dataOut, UINT32 size)
{
	if (!isDriverAddr(driverAddr))
		return 0xFFFFFFFF;
	if(size < (IRP_MJ_MAXIMUM_FUNCTION+1)*sizeof(PVOID))
		return 0xFFFFFFFF;

	PDRIVER_OBJECT driver = PDRIVER_OBJECT(driverAddr);
	for (int x = 0; x <= IRP_MJ_MAXIMUM_FUNCTION; x++)
		dataOut[x] = driver->MajorFunction[x];
	return (IRP_MJ_MAXIMUM_FUNCTION + 1) * sizeof(PVOID);
}

bool Ryblik::hookDriver(PVOID driverAddr, PVOID deviceAddr)
{
	if (!isDriverAddr(driverAddr))
	{
		DbgPrint("[Ryblik::hookDriver] BAD DRIVER PTR\n");
		return false;
	}

	INT32 count = 0;
	PDRIVER_OBJECT driver = PDRIVER_OBJECT(driverAddr);
	PDEVICE_OBJECT dev = driver->DeviceObject;
	while (dev != NULL)
	{
		for (int x = 0; x < DRIVER_HOOK_MAX_COUNT; x++)
		{
			if (hooks[x].active == 0)
			{
				count++;
				memset(&(hooks[x]), 0, sizeof(DRIVER_HOOK));
				hooks[x].driver = driver;
				hooks[x].device = dev;
				hooks[x].originalHandler = driver->MajorFunction[IRP_MJ_DEVICE_CONTROL];
				hooks[x].active = 1;
				if (deviceAddr == dev || deviceAddr == NULL)
					hooks[x].intercept = 1;
				else
					hooks[x].intercept = 0;
				break;
			}
		}
		dev = dev->NextDevice;
	}
	if (!count)
	{
		DbgPrint("[Ryblik::hookDriver] TOOO MANYYYYYYY\n");
		return false;
	}
	driver->MajorFunction[IRP_MJ_DEVICE_CONTROL] = &hook;
	return true;
}


bool Ryblik::unhookDriver(PVOID driverAddr, PVOID deviceAddr)
{
	if (!isDriverAddr(driverAddr) && driverAddr)
		return false;

	INT32 left = 0;
	for (int x = 0; x < DRIVER_HOOK_MAX_COUNT; x++)
	{
		if (driverAddr == NULL || (hooks[x].active == 1 && hooks[x].driver == driverAddr))
		{
			if (deviceAddr == hooks[x].device || deviceAddr == NULL || hooks[x].intercept == 0)
				hooks[x].intercept = 0;
			else
				left++;
		}
	}
	if (!left)
	{
		for (int x = 0; x < DRIVER_HOOK_MAX_COUNT; x++)
		{
			if (driverAddr == NULL || (hooks[x].active == 1 && hooks[x].driver == driverAddr))
			{
				__try
				{
					hooks[x].driver->MajorFunction[IRP_MJ_DEVICE_CONTROL] = hooks[x].originalHandler;
				}
				__except (EXCEPTION_EXECUTE_HANDLER) {	}
				hooks[x].active = 0;
			}
		}
	}
	return true;
}

bool Ryblik::isHookedDriver(PVOID driverAddr, PVOID deviceAddr)
{
	for (int x = 0; x < DRIVER_HOOK_MAX_COUNT; x++)
		if (hooks[x].active == 1 && hooks[x].driver == driverAddr)
			if (deviceAddr == NULL || (hooks[x].device == deviceAddr && hooks[x].intercept))
				return true;
	return false;
}


bool Ryblik::setHookConfValue(PVOID driverAddr, PVOID deviceAddr, HOOK_CONF_TYPE type, UINT32 value)
{
	bool ok = false;
	for (int x = 0; x < DRIVER_HOOK_MAX_COUNT; x++)
	{
		if (!hooks[x].active)
			continue;
		if (driverAddr == NULL || hooks[x].driver == driverAddr)
		{
			if (deviceAddr == hooks[x].device || deviceAddr == NULL)
			{
				switch (type)
				{
				case dbgBreak:
					hooks[x].debugBreak = (UINT8)value;
					break;
				case dbgLog:
					hooks[x].debugLog = (UINT8)value;
					break;
				case fileLog:
					hooks[x].fileLog = value;
					break;
					sizeof(DRIVER_HOOK);
				case dbgBreakPid:
					hooks[x].debugBreakPid = (UINT32)value;
					break;
				case dbgBreakCode:
					hooks[x].debugBreakCode = (UINT32)value;
					break;
				}
				ok = true;
			}
		}
	}
	return ok;
}

UINT32 Ryblik::getHookConfValue(PVOID driverAddr, PVOID deviceAddr, HOOK_CONF_TYPE type)
{
	for (int x = 0; x < DRIVER_HOOK_MAX_COUNT; x++)
	{
		if (!hooks[x].active)
			continue;
		if (hooks[x].driver == driverAddr)
		{
			if (deviceAddr == hooks[x].device)
			{
				switch (type)
				{
				case dbgBreak:
					return hooks[x].debugBreak;
					break;
				case dbgLog:
					return hooks[x].debugLog;
					break;
				case fileLog:
					return hooks[x].fileLog;
					break;
				case dbgBreakPid:
					return hooks[x].debugBreakPid;
					break;
				case dbgBreakCode:
					return hooks[x].debugBreakCode;
					break;
				}
			}
		}
	}
	return 0xFFFFFFFF;
}

bool Ryblik::startFileLog(PUNICODE_STRING filename)
{
	if (logFile)
		return false;

	lock();
	OBJECT_ATTRIBUTES objAttr;
	InitializeObjectAttributes(&objAttr, filename, OBJ_CASE_INSENSITIVE | OBJ_KERNEL_HANDLE, NULL, NULL);
	IO_STATUS_BLOCK    ioStatusBlock;
	NTSTATUS ntstatus = ZwCreateFile(&logFile, GENERIC_WRITE, &objAttr, &ioStatusBlock, NULL, FILE_ATTRIBUTE_NORMAL, 0, FILE_OVERWRITE_IF, FILE_SYNCHRONOUS_IO_NONALERT, NULL, 0);
	if (!NT_SUCCESS(ntstatus))
	{
		logFile = NULL;
		unlock();
		return false;
	}
	ZwWriteFile(logFile, NULL, NULL, NULL, &ioStatusBlock, "RBLK", 4, NULL, NULL);
	unlock();
	return TRUE;
}

bool Ryblik::stopFileLog()
{
	lock();
	ZwClose(logFile);
	logFile = NULL;
	unlock();
	return true;
}

bool Ryblik::isFileLogActivated()
{
	return (logFile != NULL);
}

NTSTATUS Ryblik::hook(PDEVICE_OBJECT DeviceObject, PIRP Irp)
{
	UNREFERENCED_PARAMETER(DeviceObject);
	UNREFERENCED_PARAMETER(Irp);
	bool breakExecution = false;

	for (int x = 0; x < DRIVER_HOOK_MAX_COUNT; x++)
	{
		if (hooks[x].active == 1 && hooks[x].device == DeviceObject)
		{
			PIO_STACK_LOCATION pIoStackIrp = IoGetCurrentIrpStackLocation(Irp);

			if (hooks[x].debugLog || hooks[x].debugBreak 
				|| (hooks[x].debugBreakPid && hooks[x].debugBreakPid == (UINT32)PsGetCurrentProcessId())
				|| (hooks[x].debugBreakCode && hooks[x].debugBreakCode == (UINT32)pIoStackIrp->Parameters.DeviceIoControl.IoControlCode)
				)
			{
				wchar_t drvName[128], devName[128];
				UINT32 len = getDriverName(DeviceObject->DriverObject, drvName, 256);
				if (len == 0xFFFFFFFF)
					drvName[0] = 0x00;
				else
					drvName[len/2] = 0x00;
				len = getDriverDeviceName(DeviceObject->DriverObject, DeviceObject, devName, 256);
				if (len == 0xFFFFFFFF)
					devName[0] = 0x00;
				else
					devName[len / 2] = 0x00;

				DbgPrint("<RYBLIK LOG> DeviceIoControl called for %S (%S) with code 0x%X, input buffer @ 0x%llX sized 0x%X, output sized 0x%X\n", 
					devName, drvName,
					pIoStackIrp->Parameters.DeviceIoControl.IoControlCode, 
					DriversUtils::getInputBuffer(Irp),
					pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength,
					pIoStackIrp->Parameters.DeviceIoControl.OutputBufferLength);
			}
			if (hooks[x].debugBreak)
			{
				DbgPrint("Breaking like required. To disable this over windbg, run: eb 0x%llX 0\n", &(hooks[x].debugBreak));
				breakExecution = true;
			}
			if (hooks[x].debugBreakPid && hooks[x].debugBreakPid == (UINT32)PsGetCurrentProcessId())
			{
				DbgPrint("Breaking call from process 0x%X (%d) like required. To disable this over windbg, run: ed 0x%llX 0\n", hooks[x].debugBreakPid, hooks[x].debugBreakPid, &(hooks[x].debugBreakPid));
				breakExecution = true;
			}
			if (hooks[x].debugBreakCode && hooks[x].debugBreakCode == (UINT32)pIoStackIrp->Parameters.DeviceIoControl.IoControlCode)
			{
				DbgPrint("Breaking call because code value. To disable this over windbg, run: ed 0x%llX 0\n", &(hooks[x].debugBreakCode));
				breakExecution = true;
			}
			if (hooks[x].fileLog && KeGetCurrentIrql() == PASSIVE_LEVEL)
			{
				lock();
				if (logFile)
				{

					UINT32 checksum = Utils::xcrc32((unsigned char*)DriversUtils::getInputBuffer(Irp), pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength, 0x12345678);
					if (RequestFilter::isLoggingOk(hooks[x].fileLog, DeviceObject, pIoStackIrp->Parameters.DeviceIoControl.IoControlCode, pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength, checksum))
					{
						FILE_LOG_RECORD record;
						wchar_t drvName[128], devName[128];
						UINT32 drvNameLen, devNameLen;

						drvNameLen = getDriverName(DeviceObject->DriverObject, drvName, 256);
						devNameLen = getDriverDeviceName(DeviceObject->DriverObject, DeviceObject, devName, 256);

						record.code = pIoStackIrp->Parameters.DeviceIoControl.IoControlCode;
						record.driverNameLen = (UINT16)drvNameLen;
						record.deviceNameLen = (UINT16)devNameLen;
						record.inputSize = pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength;
						record.outputSize = pIoStackIrp->Parameters.DeviceIoControl.OutputBufferLength;

						IO_STATUS_BLOCK    ioStatusBlock;

						ZwWriteFile(logFile, NULL, NULL, NULL, &ioStatusBlock, &record, sizeof(FILE_LOG_RECORD), NULL, NULL);
						ZwWriteFile(logFile, NULL, NULL, NULL, &ioStatusBlock, drvName, drvNameLen, NULL, NULL);
						ZwWriteFile(logFile, NULL, NULL, NULL, &ioStatusBlock, devName, devNameLen, NULL, NULL);
						ZwWriteFile(logFile, NULL, NULL, NULL, &ioStatusBlock, DriversUtils::getInputBuffer(Irp), record.inputSize, NULL, NULL);
					}
				}
				unlock();
			}

			if(breakExecution)
				DbgBreakPoint();
			return hooks[x].originalHandler(DeviceObject, Irp);
		}
	}

	//Trying to save things
	for (int x = 0; x < DRIVER_HOOK_MAX_COUNT; x++)
		if (hooks[x].driver == DeviceObject->DriverObject)
			return hooks[x].originalHandler(DeviceObject, Irp);

	DbgPrint("[ERROR] WTF, did not find handle for device 0x%llX!\n", DeviceObject);
	DbgBreakPoint();
	return 0;
}

bool Ryblik::isAnyHook()
{
	for (int x = 0; x < DRIVER_HOOK_MAX_COUNT; x++)
		if (hooks[x].active == 1)
			return true;
	return false;
}