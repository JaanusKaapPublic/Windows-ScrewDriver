#pragma once
#include<ntifs.h>

#define MAX_NR_OF_DEVICES 128
#define MAX_NR_OF_CODES_PER_DEVICE 64
#define MAX_NR_OF_CHECKSUMS_PER_CODE 256


typedef struct _CTRLCODE_HISTORY
{
	ULONG code;
	UINT32 count;
	UINT32 size;
	UINT32 checksums[MAX_NR_OF_CHECKSUMS_PER_CODE];
	UINT32 checksum_count;
} CTRLCODE_HISTORY, * PCTRLCODE_HISTORY;

typedef struct _DEVICE_HISTORY
{
	PDEVICE_OBJECT device;
	CTRLCODE_HISTORY codes[MAX_NR_OF_CODES_PER_DEVICE];
} DEVICE_HISTORY, * PDEVICE_HISTORY;

class RequestFilter
{
private:
	static DEVICE_HISTORY devices[MAX_NR_OF_DEVICES];
public:
	static void reset();
	static bool isLoggingOk(UINT32 limit, PDEVICE_OBJECT device, ULONG code, UINT32 sizes, UINT32 checksum);
	static void resetDevice(PDEVICE_OBJECT device);
};