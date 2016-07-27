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


##Tested Platforms##

Obviosly all this works great on Windows.

To push the boundries I have the following lab setup for *nix
 - FreeNAS 10 with ZFS
 - FreeBSD 10 jail
 - Latest mono installed in the jail
 - Console app runs within a screen. Unfortunately UDP broadcast do not work in jails at the moment. You have to specifiy and ipadress using console.exe --ip-192.168.x.y
 - XSP running in another screen and set to the root of MVC4
 - nginx proxying port 80 to XSP

Currently this FreeNAS implemention has been running for days, logging data to sqlite file every 30 seconds. MVC reads from the same sqlite file and the basic dashboard works well. Thanks to the advantages of ZFS on FreeNAS it actually runs faster on my low end "nas" box than my beefy dev machine. +1 for ZFS!
