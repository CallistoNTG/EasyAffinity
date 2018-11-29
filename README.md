# EasyAffinity
A program to simplify the process of forcing older PC games to use a single CPU core.

Copy EasyAffinity.exe into the game directory, and run it. If the game is whitelisted, everything will be handled automatically. If not, you will need to provide the executable name. The original executable will be copied to name_executable.exe, as well as backed up. The original executable will be replaced by a version that calls name_executable.exe via a batch file, setting affinity to one core in the process.

Running the program again will give the option of reverting the changes.

This program was created because many important PC games have problems with multicore CPUs, and nobody seemed to care that the process of setting affinity is needlessly complicated, especially for inexperienced users. If someone wants to play Knights of the Old Republic II, I'd like setting affinity to be the least complicated part of the process.
