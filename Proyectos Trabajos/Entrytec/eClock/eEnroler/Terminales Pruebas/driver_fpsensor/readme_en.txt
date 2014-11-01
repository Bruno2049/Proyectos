ZKFinger SDK  installation notes
------------------------------------------------

1. Install Device Drivers

    Before using the equipment,the driver must be installed , if there are the old version of the driver in the system,

    Please uninstall it before installation. ,  Do not connect the Sensor with PC before installing the driver , run Setup.exe to complete the installation.

    after the Driver installation is completed, Conect the sensor to PC , the PC will find new equipment in the normal state, just follow the prompt of operation,

    If the system suggested that is a restart,  then "no" is your choice, just take out the sensor,   reinserted  it again once.

2. Registration Biokey.ocx (ActiveX Control), Biokey.ocx mainly completed registration and identification, such as fingerprints, driver is not responsible for

    This file installation, according to the required the developer will package this file .

    Biokey.ocx  can be put to the  specified user's directory, it is better to copy it under the Windows system directory

    Registration way:  in the command running line enter following:

   Regsvr32 c: \ windows \ system32 \ biokey.ocx (directory self)

3. when upgrade UareU Series fingerprint sensor  to Zksensor6000, the first update drivers, the second update biokey.ocx, the original program
   will not be affected, but if the original program supports multiple fingerprint device,the use of zksensor6000 does not recommend , because this two sensors are able to

   access a computer at the same time , but if zksensor6000 can and UarU fingerprint are used together at same time, the program need to do the following changes to the UareU

    Fingerprint device, the control attributes SensorIndex set to 0, on Zksensor6000 the SensorIndex attribute set to 1000.

                               2007.10.15