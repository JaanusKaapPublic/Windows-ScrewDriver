#include "RyblikDriverIO.h"
#include <stdio.h>

bool RyblikDriverIO::init()
{
	handle = CreateFile(L"\\\\.\\Ryblik", GENERIC_READ | GENERIC_WRITE, 0, NULL, OPEN_EXISTING, 0, NULL);
	return (handle != INVALID_HANDLE_VALUE);
}

bool RyblikDriverIO::devIOctrl(DWORD code, PVOID inBuffer, DWORD inBufferSize, PVOID outBuffer, DWORD* outBufferSize)
{
	bool result = false;
	DWORD recv, outBufferSizeVal = 0;
	if (outBufferSize)
		outBufferSizeVal = *outBufferSize;

	if (DeviceIoControl(handle, code, inBuffer, inBufferSize, outBuffer, outBufferSizeVal, &recv, NULL))
	{
		result = true;
		if (outBufferSize)
			*outBufferSize = recv;
	}
	return result;
}

DWORD RyblikDriverIO::getDriverCount()
{
	DWORD count, ret = 4;
	if (!devIOctrl(IOCTL_RYBLIK_GET_DRIVER_COUNT, NULL, 0, &count, &ret))
		return 0xFFFFFFFF;
	return count;
}

DWORD RyblikDriverIO::getDriverAddresses(PVOID* data, DWORD size)
{
	if (!devIOctrl(IOCTL_RYBLIK_GET_DRIVER_ADDRESSES, NULL, 0, data, &size))
		return 0xFFFFFFFF;
	return size;
}

DWORD RyblikDriverIO::getDriverName(PVOID addr, wchar_t* data, DWORD size)
{
	if (!devIOctrl(IOCTL_RYBLIK_GET_DRIVER_NAME, &addr, sizeof(PVOID), data, &size))
		return 0xFFFFFFFF;
	return size;
}

DWORD RyblikDriverIO::getDriverDeviceCount(PVOID addr)
{
	DWORD count, ret = 4;
	if (!devIOctrl(IOCTL_RYBLIK_GET_DRIVER_DEVICE_COUNT, &addr, sizeof(PVOID), &count, &ret))
		return 0xFFFFFFFF;
	return count;
}

DWORD RyblikDriverIO::getDriverDeviceAddresses(PVOID addr, PVOID* data, DWORD size)
{
	if (!devIOctrl(IOCTL_RYBLIK_GET_DRIVER_DEVICE_ADDRESSES, &addr, sizeof(PVOID), data, &size))
		return 0xFFFFFFFF;
	return size;
}

DWORD RyblikDriverIO::getDriverDeviceName(PVOID addr, PVOID devAddr, wchar_t* data, DWORD size)
{
	PVOID dataIn[2] = { addr, devAddr };
	if (!devIOctrl(IOCTL_RYBLIK_GET_DRIVER_DEVICE_NAME, dataIn, sizeof(dataIn), data, &size))
		return 0xFFFFFFFF;
	return size;
}

DWORD RyblikDriverIO::getDriverMajorFunctions(PVOID addr, PVOID* data, DWORD size)
{
	if (!devIOctrl(IOCTL_RYBLIK_GET_DRIVER_FUNCTIONS, &addr, sizeof(PVOID), data, &size))
		return 0xFFFFFFFF;
	return size;
}

bool RyblikDriverIO::hookDriver(PVOID drv, PVOID dev)
{
	PVOID data[2] = { drv, dev };
	if (!devIOctrl(IOCTL_RYBLIK_HOOK, data, sizeof(PVOID) * 2, NULL, NULL))
		return false;
	return true;
}

bool RyblikDriverIO::unhookDriver(PVOID drv, PVOID dev)
{
	PVOID data[2] = { drv, dev };
	if (!devIOctrl(IOCTL_RYBLIK_UNHOOK, data, sizeof(PVOID) * 2, NULL, NULL))
		return false;
	return true;
}

bool RyblikDriverIO::isHookedDriver(PVOID drv, PVOID dev)
{
	PVOID data[2] = { drv, dev };
	bool out;
	DWORD outLen = 1;
	if (!devIOctrl(IOCTL_RYBLIK_IS_HOOKED, data, sizeof(PVOID) * 2, &out, &outLen))
		return false;
	return out;
}

bool RyblikDriverIO::setHookConfValue(PVOID drv, PVOID dev, UINT32 type, UINT32 value)
{

	PVOID data[4] = { drv, dev, (PVOID)(UINT64)type, (PVOID)(UINT64)value};
	if (!devIOctrl(IOCTL_RYBLIK_SET_HOOK_CONF_VALUE, data, sizeof(PVOID) * 4, NULL, NULL))
		return false;
	return true;
}

UINT32 RyblikDriverIO::getHookConfValue(PVOID drv, PVOID dev, UINT32 type)
{
	PVOID data[3] = { drv, dev, (PVOID)(UINT64)type};
	UINT32 ret;
	DWORD outLen = 4;
	if (!devIOctrl(IOCTL_RYBLIK_GET_HOOK_CONF_VALUE, data, sizeof(PVOID) * 3, &ret, &outLen))
		return 0xFFFFFFFF;
	return ret;
}

bool RyblikDriverIO::startFileLog(wchar_t* filename, DWORD len)
{
	wchar_t* buffer = new wchar_t[len/2 + 12];
	memcpy(buffer, L"\\DosDevices\\", 24);
	memcpy(buffer + 12, filename, len);
	if (!devIOctrl(IOCTL_RYBLIK_START_FILE_LOG, buffer, 24 + len, NULL, NULL))
	{
		delete buffer;
		return false;
	}
	delete buffer;
	return true;
}

bool RyblikDriverIO::stopFileLog()
{
	if (!devIOctrl(IOCTL_RYBLIK_STOP_FILE_LOG, NULL, 0, NULL, NULL))
		return false;
	return true;
}

bool RyblikDriverIO::isFileLogActivated()
{
	bool out;
	DWORD outLen = 1;
	if (!devIOctrl(IOCTL_RYBLIK_IS_FILE_LOG_ACTIVATED, NULL, 0, &out, &outLen))
		return out;
	return out;
}

