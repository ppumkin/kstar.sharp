


*Linux*
Install atleast version 3.3 of Mono to get this going

*SQLite*
The provided sqlite.dll is x86 so it can run on Windows
If you need to amd64 version for Windows replace the DLL

On Linux/Mac make sure the latest SQLite3 is installed.


*PATHS*
The path for the file on linux must meet the forward slash as expected or it wont create the file or create it in some b
lack hole never to be found again (because the test for if file exists passes but I cant find it)

Windows doesnt care about the slash so I left it forward slash.

*UNICAST*
Unicast ip discovery does not work in BSDJails. Not sure why yet but you have to specify IP using "--ip-aaa.bbb.ccc.ddd" on the command line.
Using a fully fledged network connection the IP should be discovered automatically.