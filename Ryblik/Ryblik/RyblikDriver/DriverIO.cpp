#include"DriverIO.h"
#include"Ryblik.h"

NTSTATUS DriverIO::getDriversCount(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten)
{
	if (pIoStackIrp->Parameters.DeviceIoControl.OutputBufferLength < 4)
		return STATUS_BUFFER_OVERFLOW;
	*(UINT32*)Irp->AssociatedIrp.SystemBuffer = Ryblik::getDriversCount();
	if(*(UINT32*)Irp->AssociatedIrp.SystemBuffer == 0xFFFFFFFF)
		return STATUS_INTERNAL_ERROR;
	*pdwDataWritten = 4;
	return STATUS_SUCCESS;
}

NTSTATUS DriverIO::getDriverAddresses(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten)
{
	*pdwDataWritten = Ryblik::getDriverAddresses((PVOID*)Irp->AssociatedIrp.SystemBuffer, pIoStackIrp->Parameters.DeviceIoControl.OutputBufferLength);
	return STATUS_SUCCESS;
}

NTSTATUS DriverIO::getDriverName(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten)
{
	if (pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength < sizeof(PVOID))
		return STATUS_NDIS_INVALID_LENGTH;
	UINT32 size = Ryblik::getDriverName(*(PVOID*)Irp->AssociatedIrp.SystemBuffer, (wchar_t*)Irp->AssociatedIrp.SystemBuffer, pIoStackIrp->Parameters.DeviceIoControl.OutputBufferLength);
	if (size == 0xFFFFFFFF)
		return STATUS_INVALID_PARAMETER;
	*pdwDataWritten = size;
	return STATUS_SUCCESS;
}

NTSTATUS DriverIO::getDriverDevicesCount(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten)
{
	if (pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength < sizeof(PVOID))
		return STATUS_NDIS_INVALID_LENGTH;
	UINT32 count = Ryblik::getDriverDevicesCount(*(PVOID*)Irp->AssociatedIrp.SystemBuffer);
	if (count == 0xFFFFFFFF)
		return STATUS_INVALID_PARAMETER;
	*(UINT32*)Irp->AssociatedIrp.SystemBuffer = count;
	*pdwDataWritten = 4;
	return STATUS_SUCCESS;
}

NTSTATUS DriverIO::getDriverDeviceAddresses(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten)
{
	if (pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength < sizeof(PVOID))
		return STATUS_NDIS_INVALID_LENGTH;
	UINT32 size = Ryblik::getDriverDeviceAddresses(*(PVOID*)Irp->AssociatedIrp.SystemBuffer, (PVOID*)Irp->AssociatedIrp.SystemBuffer, pIoStackIrp->Parameters.DeviceIoControl.OutputBufferLength);
	if (size == 0xFFFFFFFF)
		return STATUS_INVALID_PARAMETER;
	*pdwDataWritten = size;	
	return STATUS_SUCCESS;
}

NTSTATUS DriverIO::getDriverDeviceName(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten)
{
	if (pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength < sizeof(PVOID)*2)
		return STATUS_NDIS_INVALID_LENGTH;
	UINT32 size = Ryblik::getDriverDeviceName(((PVOID*)Irp->AssociatedIrp.SystemBuffer)[0], ((PVOID*)Irp->AssociatedIrp.SystemBuffer)[1], (wchar_t*)Irp->AssociatedIrp.SystemBuffer, pIoStackIrp->Parameters.DeviceIoControl.OutputBufferLength);
	if (size == 0xFFFFFFFF)
		return STATUS_INVALID_PARAMETER;
	*pdwDataWritten = size;
	return STATUS_SUCCESS;
}

NTSTATUS DriverIO::getDriverMajorFunctions(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten)
{
	if (pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength < sizeof(PVOID))
		return STATUS_NDIS_INVALID_LENGTH;
	if (pIoStackIrp->Parameters.DeviceIoControl.OutputBufferLength < (IRP_MJ_MAXIMUM_FUNCTION + 1) * sizeof(PVOID))
		return STATUS_BUFFER_OVERFLOW;
	UINT32 size = Ryblik::getDriverMajorFunctions(*(PVOID*)Irp->AssociatedIrp.SystemBuffer, (PVOID*)Irp->AssociatedIrp.SystemBuffer, pIoStackIrp->Parameters.DeviceIoControl.OutputBufferLength);
	if (size == 0xFFFFFFFF)
		return STATUS_INVALID_PARAMETER;
	*pdwDataWritten = size;
	return STATUS_SUCCESS;
}

NTSTATUS DriverIO::hookDriver(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten)
{
	UNREFERENCED_PARAMETER(pdwDataWritten);
	if (pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength < sizeof(PVOID) * 2)
		return STATUS_NDIS_INVALID_LENGTH;
	if (Ryblik::hookDriver(((PVOID*)Irp->AssociatedIrp.SystemBuffer)[0], ((PVOID*)Irp->AssociatedIrp.SystemBuffer)[1]))
		return STATUS_SUCCESS;
	return STATUS_INTERNAL_ERROR;
}

NTSTATUS DriverIO::unhookDriver(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten)
{
	UNREFERENCED_PARAMETER(pdwDataWritten);
	if (pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength < sizeof(PVOID) * 2)
		return STATUS_NDIS_INVALID_LENGTH;
	if (Ryblik::unhookDriver(((PVOID*)Irp->AssociatedIrp.SystemBuffer)[0], ((PVOID*)Irp->AssociatedIrp.SystemBuffer)[1]))
		return STATUS_SUCCESS;
	return STATUS_INTERNAL_ERROR;
}

void DriverIO::unhookAllDrivers()
{
	Ryblik::unhookDriver(NULL, NULL);
}

NTSTATUS DriverIO::isHookedDriver(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten)
{
	if (pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength < sizeof(PVOID) * 2)
		return STATUS_NDIS_INVALID_LENGTH;
	if (!pIoStackIrp->Parameters.DeviceIoControl.OutputBufferLength)
		return STATUS_BUFFER_OVERFLOW;
	if (Ryblik::isHookedDriver(((PVOID*)Irp->AssociatedIrp.SystemBuffer)[0], ((PVOID*)Irp->AssociatedIrp.SystemBuffer)[1]))
		*(PUINT8)Irp->AssociatedIrp.SystemBuffer = 1;
	else
		*(PUINT8)Irp->AssociatedIrp.SystemBuffer = 0;
	*pdwDataWritten = 1;
	return STATUS_SUCCESS;
}

NTSTATUS DriverIO::setHookConfValue(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten)
{
	UNREFERENCED_PARAMETER(pdwDataWritten);
	if (pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength < sizeof(PVOID) * 4)
		return STATUS_NDIS_INVALID_LENGTH;
	PUINT8 buf = (PUINT8)Irp->AssociatedIrp.SystemBuffer;
	HOOK_CONF_TYPE type = *(HOOK_CONF_TYPE*)(buf + sizeof(PVOID) * 2);	
	UINT32 value = *(UINT32*)(buf + sizeof(PVOID) * 3);
	if (Ryblik::setHookConfValue(((PVOID*)Irp->AssociatedIrp.SystemBuffer)[0], ((PVOID*)Irp->AssociatedIrp.SystemBuffer)[1], type, value))
		return STATUS_SUCCESS;
	return STATUS_INTERNAL_ERROR;
}

NTSTATUS DriverIO::getHookConfValue(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten)
{
	if (pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength < sizeof(PVOID) * 3)
		return STATUS_NDIS_INVALID_LENGTH;
	if (pIoStackIrp->Parameters.DeviceIoControl.OutputBufferLength < sizeof(UINT32))
		return STATUS_BUFFER_OVERFLOW;

	PUINT8 buf = (PUINT8)Irp->AssociatedIrp.SystemBuffer;
	HOOK_CONF_TYPE type = *(HOOK_CONF_TYPE*)(buf + sizeof(PVOID) * 2);
	UINT32 val = Ryblik::getHookConfValue(((PVOID*)Irp->AssociatedIrp.SystemBuffer)[0], ((PVOID*)Irp->AssociatedIrp.SystemBuffer)[1], type);
	if (val != 0xFFFFFFFF)
	{
		*(PUINT32)Irp->AssociatedIrp.SystemBuffer = val;
		*pdwDataWritten = sizeof(UINT32);
		return STATUS_SUCCESS;
	}
	return STATUS_INTERNAL_ERROR;
}

NTSTATUS DriverIO::startFileLog(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten)
{
	if (pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength > 1022 )
		return STATUS_NDIS_INVALID_LENGTH;
	UNREFERENCED_PARAMETER(pdwDataWritten);
	UNICODE_STRING uniName;
	uniName.Buffer = (wchar_t*)Irp->AssociatedIrp.SystemBuffer;
	uniName.Length = (USHORT)pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength;
	uniName.MaximumLength = (USHORT)pIoStackIrp->Parameters.DeviceIoControl.InputBufferLength;
	if (Ryblik::startFileLog(&uniName))
		return STATUS_SUCCESS;
	return STATUS_INTERNAL_ERROR;
}

NTSTATUS DriverIO::stopFileLog(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten)
{
	UNREFERENCED_PARAMETER(pdwDataWritten);
	UNREFERENCED_PARAMETER(pIoStackIrp);
	UNREFERENCED_PARAMETER(Irp);
	Ryblik::stopFileLog();
	return STATUS_SUCCESS;
}

NTSTATUS DriverIO::isFileLogActivated(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten)
{
	if (!pIoStackIrp->Parameters.DeviceIoControl.OutputBufferLength)
		return STATUS_BUFFER_OVERFLOW;
	if (Ryblik::isFileLogActivated())
		*(PUINT8)Irp->AssociatedIrp.SystemBuffer = 1;
	else
		*(PUINT8)Irp->AssociatedIrp.SystemBuffer = 0;
	*pdwDataWritten = 1;
	return STATUS_SUCCESS;
}