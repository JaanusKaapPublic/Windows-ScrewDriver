#pragma once
#include<ntifs.h>

class DriversUtils
{
public:
	static UINT32 getInputBufferSize(PIRP Irp);
	static UINT32 getOutputBufferSize(PIRP Irp);
	static PVOID getInputBuffer(PIRP Irp);
};