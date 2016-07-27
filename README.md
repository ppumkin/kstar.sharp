# kstar.sharp
C# Cross Platform app for getting data from KSTAR Inverter

##Currently this Repository has these function##

 - Core inverter UDP communication read only
 - Parsing
 - Cross platfomr SQlite implementation based on Xamarin.SQLite (Windows,*nix and Mac)
 - Console application executable runnable in Windows and on Mono (Communicated with Inverter, udpates console and saves to database)
 - MVC 4 Dashboard. Runnable on IIS or XSP
 
##Todo:##
 
 - Windows UWP App
 - iOs App
 - Android App
 
##Disclaimer##

The protocol has been reverse engineered and the goal of this code is for read only use only. 
For your safety this repository will never implement setting/writting anything back to the inverter.
Please use the official kstar app for any maintanence required on your inverter.
