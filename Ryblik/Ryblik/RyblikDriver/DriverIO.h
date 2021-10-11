#pragma once
#include<ntifs.h>

class DriverIO
{
public:
	static NTSTATUS getDriversCount(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten);
	static NTSTATUS getDriverAddresses(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten);
	static NTSTATUS getDriverName(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten);
	static NTSTATUS getDriverDevicesCount(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten);
	static NTSTATUS getDriverDeviceAddresses(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten);
	static NTSTATUS getDriverDeviceName(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten);

	static NTSTATUS getDriverMajorFunctions(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten);

	static NTSTATUS hookDriver(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten);
	static NTSTATUS unhookDriver(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten);
	static void unhookAllDrivers();
	static NTSTATUS isHookedDriver(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten);

	static NTSTATUS setHookConfValue(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten);
	static NTSTATUS getHookConfValue(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten);

	static NTSTATUS startFileLog(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten);
	static NTSTATUS stopFileLog(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten);
	static NTSTATUS isFileLogActivated(PIRP Irp, PIO_STACK_LOCATION pIoStackIrp, PULONGLONG pdwDataWritten);
};