

# kstar.sharp
C# Cross Platform app for getting data from KSTAR Inverter

- .NET 4.5
- ASP.MVC 5
- Xamarin for cross device apps
- SQlite Mono (Xamarin)
- Unity Game Engine ... Started as "Just for fun" but now I realised I can deploy to PS4, XBox, Linux, PSVita, WiiU, Nintento 3DS, Oculus Rift, Steam VR, Microsoft Hololens, Android TV, Samsung Smart TV, tvOS, Fire OS and last but not least Facebook Gameroom (whatever that is).


##Currently this Repository has these function##

 - Core inverter UDP communication read only
 - Parsing bytes/hex to C# Object Models
 - Cross platform SQlite implementation based on Xamarin.SQLite (Windows,*nix and Mac)
 - Console application executable runnable in Windows and on Mono (Communicate with Inverter, udpates console and saves to database)
 - MVC 4 Dashboard. Runnable on IIS or XSP or anything you configure to run ASP.NET MVC
 
##Todo:##
 
 - Windows UWP App (Native)
 - iOS App (Xamarin - But I dont have a mac so possibly Cordova but i think its too limited?)
 - Android App (Xamarin)
 
##Disclaimer##

The protocol has been reverse engineered and the goal of this code is for read only use only. 
I cannot guarantee it will work with all models and I cannot guarantee stability
For your safety this repository will never implement setting/writting anything back to the inverter.
Please use the official kstar app for any maintanence required on your inverter.


##Tested Platforms##

###Windows###
Obviosly all this works great on Windows.

###*nix###

To push the boundries I have the following lab setup for *nix
 - FreeNAS 10 with ZFS
 - FreeBSD 10 jail
 - Latest mono installed in the jail
 - Console app runs within a screen. Unfortunately UDP broadcast do not work in jails at the moment. You have to specifiy and ipadress using console.exe --ip-192.168.x.y
 - XSP running in another screen and set to the root of MVC4
 - nginx proxying port 80 to XSP

Currently this FreeNAS implemention has been running for days, logging data to sqlite file every 30 seconds. MVC reads from the same sqlite file and the basic dashboard works well. Thanks to the advantages of ZFS on FreeNAS it actually runs faster on my low end "nas" box than my beefy dev machine. +1 for ZFS!

XSP Command line

Set nginx_enable to YES in /etc/rc.conf  or use 'onestart'

fastcgi-mono-server4 /applications=/:/mnt/storage/mvc4/ /socket=tcp:127.0.0.1:9000


###Android###
Will reuse the C# library in Xamarin Android project

###iOS###
Will reuse the C# library in Xamarin iOS project but there seems to be initial problems with this. Firstly I don't have a mac and secondly Xamarin decided to not install the iOS SDK... Looking bleak as usual with iOS.

###Unity - Game Engine###

I was looking at Unity a few times while writting a plugin for Rimworld (http://steamcommunity.com/sharedfiles/filedetails/?id=760900903) One day I was bored and decided to download Unity and see how it works. (Unity 5.2)

- I started looking at the 2D capabilities and for some strange reason my first idea was to create an animation of the sun based on the time of day. Then added a solar panel, a home, battery and pwoergrid. Wow.. OK I got the basics in writting scripts, attaching functions and even some basic AI.
- I then added WebRequests to call the kstar.sharp.MVC5 site running on FreeNAS and that gets the current state of the Inverter.
- I added OpenWeatherMap API and display the temperature, sunrise and sunset times from there.
- I tweaked my tablet to not lock or sleep after some time. I started this "dashboard" on it and it has been happilly running for over 72 hours.

![kstar.sharp.unity](http://i.imgur.com/HSH7Hdv.png "kstar.sharp.unity running on my £30 Windows 10 Tablet")

##Tested Inverters##

Hybrid KSE4000 with WiFi, EZMeter and 10kW Battery array - Stable

Need help adding other kstar inverters.
I hope I can add other manufacturers too maybe - we'll see what happens

##Screens##

###Console###

![kstar.sharp.console](http://i.imgur.com/k97oYF6.png "kstar.sharp.console in Jail")

###MVC5 Dashboard###

![kstar.sharp.mvc5](http://i.imgur.com/s0fUrq1.png "kstar.sharp.mvc5 in Jail")

All data over some time


![kstar.sharp.mvc5](http://i.imgur.com/HOYTo1D.png "kstar.sharp.mvc5 in Jail")

Cross section of that data

![kstar.sharp.mvc5](http://i.imgur.com/uGHr8yF.png "kstar.sharp.mvc5 in Jail")


# License
 - This source code has been made available for educational uses only but is NOT licensed as any version of "Educational Community License"
 - Reverse engineering of some protocols were accomplished by monitoring unencrypted data within a sandboxed, private network. No attempts of hacking, brute force or use of non-public or leaked code was used.
 - Source Code is released under the "fair use" policy, to allow for interobility on various other platforms, but only in a limited form. 
 - The limitation is read only data collection to allow to create monitoring software for personal use
 - No further attempt to fully reverse engineer the protocol will be made. eg, to allow sending data back to hardware.
 - At no point was the main contributor under any NDA agreement.
 - Reusuing some parts of this source code in any capacity may violate copy right law
 - Redistribution of this software in source or binary forms shall be free of all charges or fees to the recipient of this software.
 - All 3rd party licenses must be adhered too.


