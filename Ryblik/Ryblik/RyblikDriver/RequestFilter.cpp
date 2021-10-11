#include "RequestFilter.h"

DEVICE_HISTORY RequestFilter::devices[MAX_NR_OF_DEVICES];

void RequestFilter::reset()
{
	memset(&devices, 0x0, sizeof(devices));
}

bool RequestFilter::isLoggingOk(UINT32 limit, PDEVICE_OBJECT device, ULONG code, UINT32 size, UINT32 checksum)
{
	if (limit == 0xFFFFFFFF)
		return true;
	for (int x = 0; x < MAX_NR_OF_DEVICES; x++)
	{
		if (devices[x].device == device)
		{
			for (int y = 0; y < MAX_NR_OF_CODES_PER_DEVICE; y++)
			{
				if (devices[x].codes[y].code == code && devices[x].codes[y].size == size)
				{
					if (devices[x].codes[y].count >= limit)
						return false;
					for (UINT32 z = 0; z < devices[x].codes[y].checksum_count; z++)
						if (devices[x].codes[y].checksums[z] == checksum)
							return false;
					devices[x].codes[y].count++;
					if (devices[x].codes[y].checksum_count < MAX_NR_OF_CHECKSUMS_PER_CODE)
						devices[x].codes[y].checksums[devices[x].codes[y].checksum_count++] = checksum;
					return true;
				}
			}


			for (int y = 0; y < MAX_NR_OF_CODES_PER_DEVICE; y++)
			{
				if (devices[x].codes[y].code == 0)
				{
					devices[x].codes[y].code = code;
					devices[x].codes[y].size = size;
					devices[x].codes[y].count = 1;
					devices[x].codes[y].checksum_count = 1;
					devices[x].codes[y].checksums[0] = checksum;
					return true;
				}
			}
		}
	}


	for (int x = 0; x < MAX_NR_OF_DEVICES; x++)
	{
		if (devices[x].device == NULL)
		{
			devices[x].device = device;
			devices[x].codes[0].code = code;
			devices[x].codes[0].size = size;
			devices[x].codes[0].count = 1;
			devices[x].codes[0].checksum_count = 1;
			devices[x].codes[0].checksums[0] = checksum;
			return true;
		}
	}

	return false;
}

void RequestFilter::resetDevice(PDEVICE_OBJECT device)
{
	for (int x = 0; x < MAX_NR_OF_DEVICES; x++)
	{
		if (devices[x].device == device)
		{
			devices[x].device = device;
			memset(&(devices[x].codes), 0x0, sizeof(devices[x].codes));
		}
	}
}