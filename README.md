# EasyAffinity
Please be aware that setting Windows Compatibility Mode for applications to Windows XP will force applications to run using a single core, accomplish the same goal as this program. There are also a few other programs that modify game executable affinity, however they tend to require command line usage.

Copy EasyAffinity.exe into the game directory, and run it. If the game is whitelisted (only KOTOR and KOTOR II are whitelisted currently), everything will be handled automatically. If not, you will need to provide the executable name. The original executable will be copied to name_executable.exe, as well as backed up. The original executable will be replaced by a version that calls name_executable.exe via a batch file, setting affinity to one core in the process.

Running the program again will give the option of reverting the changes.

This is a cute little project that was fun to write. And to my pleasant surprise, its underlying ideas were useful for creating the 007 Blood Stone fix.
