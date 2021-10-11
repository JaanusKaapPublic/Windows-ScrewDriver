#include "DriversUtils.h"

UINT32 DriversUtils::getInputBufferSize(PIRP Irp)
{
	PIO_STACK_LOCATION pIoStackIrp = IoGetCurrentIrpStackLocation(Irp);
	return pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength;
	
}

UINT32 DriversUtils::getOutputBufferSize(PIRP Irp)
{
	PIO_STACK_LOCATION pIoStackIrp = IoGetCurrentIrpStackLocation(Irp);
	return pIoStackIrp->Parameters.DeviceIoControl.OutputBufferLength;
}

PVOID DriversUtils::getInputBuffer(PIRP Irp)
{
	PIO_STACK_LOCATION pIoStackIrp = IoGetCurrentIrpStackLocation(Irp);
	if (!pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength)
		return NULL;
	
	UINT32 method = pIoStackIrp->Parameters.DeviceIoControl.IoControlCode & 0x3;
	switch (method)
	{
	case METHOD_BUFFERED:
	case METHOD_IN_DIRECT:
	case METHOD_OUT_DIRECT:
		return Irp->AssociatedIrp.SystemBuffer;
	case METHOD_NEITHER:
		return pIoStackIrp->Parameters.DeviceIoControl.Type3InputBuffer;
	}

	return NULL;
}