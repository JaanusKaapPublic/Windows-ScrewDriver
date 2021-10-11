//#include<ntddk.h>
#include<ntifs.h>
#include"Driver.h"
#include"Ryblik.h"
#include"DriverIO.h"
#include"SharedDefs.h"

NTSTATUS DriverIoControl(PDEVICE_OBJECT DeviceObject, PIRP Irp);
NTSTATUS TracedrvDispatchOpenClose(IN PDEVICE_OBJECT pDO, IN PIRP Irp);
VOID DriverUnload(PDRIVER_OBJECT  DriverObject);


extern "C" NTSTATUS DriverEntry(PDRIVER_OBJECT pDriverObject, PUNICODE_STRING pRegistryPath)
{
	UNREFERENCED_PARAMETER(pRegistryPath);
#ifdef _DEBUG
	DbgPrint("[DEBUG][DriverEntry]Starting Ryblik\n");
#endif

	NTSTATUS NtStatus = STATUS_SUCCESS;
	PDEVICE_OBJECT pDeviceObject = NULL;
	UNICODE_STRING usDriverName, usDosDeviceName;

	pDriverObject->MajorFunction[IRP_MJ_CLOSE] = TracedrvDispatchOpenClose;
	pDriverObject->MajorFunction[IRP_MJ_CREATE] = TracedrvDispatchOpenClose;
	pDriverObject->MajorFunction[IRP_MJ_DEVICE_CONTROL] = DriverIoControl;

	RtlInitUnicodeString(&usDriverName, DEVICE_NAME);
	RtlInitUnicodeString(&usDosDeviceName, SYMBOLIC_LINK_NAME);
	NtStatus = IoCreateDevice(pDriverObject, 0, &usDriverName, FILE_DEVICE_UNKNOWN, FILE_DEVICE_SECURE_OPEN, FALSE, &pDeviceObject);
	if (NtStatus == STATUS_SUCCESS)
	{
		IoCreateSymbolicLink(&usDosDeviceName, &usDriverName);
		pDriverObject->DriverUnload = DriverUnload;
	}

	RequestFilter::reset();
	Ryblik::init();	
	return NtStatus;
}

NTSTATUS TracedrvDispatchOpenClose(IN PDEVICE_OBJECT pDO, IN PIRP Irp)
{
	UNREFERENCED_PARAMETER(pDO);
	Irp->IoStatus.Status = STATUS_SUCCESS;
	Irp->IoStatus.Information = 0;
	PAGED_CODE();
	IoCompleteRequest(Irp, IO_NO_INCREMENT);
	return STATUS_SUCCESS;
}

VOID DriverUnload(PDRIVER_OBJECT  DriverObject)
{
	UNICODE_STRING usDosDeviceName;
	if(Ryblik::isAnyHook())
	{
		DriverIO::unhookAllDrivers();
	}
	RtlInitUnicodeString(&usDosDeviceName, SYMBOLIC_LINK_NAME);
	IoDeleteSymbolicLink(&usDosDeviceName);
	IoDeleteDevice(DriverObject->DeviceObject);
#ifdef _DEBUG
	DbgPrint("[DEBUG][DriverEntry]Ryblik stopped\n");
#endif
}

NTSTATUS DriverIoControl(PDEVICE_OBJECT DeviceObject, PIRP Irp)
{
	UNREFERENCED_PARAMETER(DeviceObject);
	NTSTATUS NtStatus = STATUS_NOT_SUPPORTED;
	PIO_STACK_LOCATION pIoStackIrp = IoGetCurrentIrpStackLocation(Irp);
	ULONGLONG dataLen = 0;

	if (pIoStackIrp)
	{
#ifdef _DEBUG
		//DbgPrint("[DEBUG][DriverIoControl]Called with control code 0x%X\n", pIoStackIrp->Parameters.DeviceIoControl.IoControlCode);
#endif
		switch (pIoStackIrp->Parameters.DeviceIoControl.IoControlCode)
		{
		case IOCTL_RYBLIK_GET_DRIVER_COUNT:
			NtStatus = DriverIO::getDriversCount(Irp, pIoStackIrp, &dataLen);
			break;
		case IOCTL_RYBLIK_GET_DRIVER_ADDRESSES:
			NtStatus = DriverIO::getDriverAddresses(Irp, pIoStackIrp, &dataLen);
			break;
		case IOCTL_RYBLIK_GET_DRIVER_NAME:
			NtStatus = DriverIO::getDriverName(Irp, pIoStackIrp, &dataLen);
			break;
		case IOCTL_RYBLIK_GET_DRIVER_DEVICE_COUNT:
			NtStatus = DriverIO::getDriverDevicesCount(Irp, pIoStackIrp, &dataLen);
			break;
		case IOCTL_RYBLIK_GET_DRIVER_DEVICE_ADDRESSES:
			NtStatus = DriverIO::getDriverDeviceAddresses(Irp, pIoStackIrp, &dataLen);
			break;
		case IOCTL_RYBLIK_GET_DRIVER_DEVICE_NAME:
			NtStatus = DriverIO::getDriverDeviceName(Irp, pIoStackIrp, &dataLen);
			break;
		case IOCTL_RYBLIK_GET_DRIVER_FUNCTIONS:
			NtStatus = DriverIO::getDriverMajorFunctions(Irp, pIoStackIrp, &dataLen);
			break;
		case IOCTL_RYBLIK_HOOK:
			NtStatus = DriverIO::hookDriver(Irp, pIoStackIrp, &dataLen);
			break;
		case IOCTL_RYBLIK_UNHOOK:
			NtStatus = DriverIO::unhookDriver(Irp, pIoStackIrp, &dataLen);
			break;
		case IOCTL_RYBLIK_IS_HOOKED:
			NtStatus = DriverIO::isHookedDriver(Irp, pIoStackIrp, &dataLen);
			break;
		case IOCTL_RYBLIK_SET_HOOK_CONF_VALUE:
			NtStatus = DriverIO::setHookConfValue(Irp, pIoStackIrp, &dataLen);
			break;
		case IOCTL_RYBLIK_GET_HOOK_CONF_VALUE:
			NtStatus = DriverIO::getHookConfValue(Irp, pIoStackIrp, &dataLen);
			break;
		case IOCTL_RYBLIK_START_FILE_LOG:
			NtStatus = DriverIO::startFileLog(Irp, pIoStackIrp, &dataLen);
			break;
		case IOCTL_RYBLIK_STOP_FILE_LOG:
			NtStatus = DriverIO::stopFileLog(Irp, pIoStackIrp, &dataLen);
			break;
		case IOCTL_RYBLIK_IS_FILE_LOG_ACTIVATED:
			NtStatus = DriverIO::isFileLogActivated(Irp, pIoStackIrp, &dataLen);
			break;
		}
	}

	Irp->IoStatus.Status = NtStatus;
	Irp->IoStatus.Information = dataLen;
	IoCompleteRequest(Irp, IO_NO_INCREMENT);

	return NtStatus;

}