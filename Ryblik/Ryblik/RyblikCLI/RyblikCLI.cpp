#include <windows.h>
#include <stdio.h>
#include "RyblikDriverIO.h"

int main()
{
	wchar_t name[1024];
	RyblikDriverIO aaa;
	aaa.init();
	DWORD count = aaa.getDriverCount();
	printf("count = 0x%X\n", count);

	PVOID* data = new PVOID[count];
	count = aaa.getDriverAddresses(data, count * sizeof(PVOID));
	if (count == 0xFFFFFFFF)
		return 0;
	for (DWORD x = 0; x < count / sizeof(PVOID); x++)
	{
		memset(name, 0, sizeof(name));
		aaa.getDriverName(data[x], name, sizeof(name));
		printf("0x%llX %S", data[x], name);
		printf("\n");
		
		DWORD count2 = aaa.getDriverDeviceCount(data[x]);
		printf("  [0x%X devices]\n", count2);
		PVOID* data2 = new PVOID[count2];
		count2 = aaa.getDriverDeviceAddresses(data[x], data2, count2*sizeof(PVOID));
		for (DWORD y = 0; y < count2 / sizeof(PVOID); y++)
		{
			memset(name, 0, sizeof(name));
			DWORD z = aaa.getDriverDeviceName(data[x], data2[y], name, sizeof(name));
			if (name[0] == 0x0000)
			{
				printf("  0x%llX <0x%X>", data2[y], z);
			}
			else
			{
				printf("  0x%llX %S", data2[y], name);
			}
			printf("\n");
		}
	}
}
