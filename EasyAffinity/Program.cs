using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Need System.IO for file manipulation.
using System.IO;
//needed for processes?
using System.Diagnostics;
using System.ComponentModel;

namespace EasyAffinity {
    //This is a simple, easy to use program that simplifies the task of fixing games that require certain CPU affinity settings to operate properly. It replaces the game's original executable with one that redirects to a batch file that sets the game affinity at boot.
    class Program {
        static void Main(string[] args) {
            //Decide which mode we are operating in based on the filename of the current executable. If the file executable is EasyCoreCompatibility, then attempt to update the game. Beware of recursive stuff.
            //If the filename is NOT EasyCoreCompatiblity.exe, then assume we are the videogame replacement executable and 

            if (System.AppDomain.CurrentDomain.FriendlyName == "EasyAffinity.exe") {
                PatchGame();
            }
            else {
                RunProgram();
            }

        }

        static void PatchGame() {
            string gameName = FindGame();
            if(File.Exists(gameName + "_backup.exe")) {
                //TODO implement code to revert the change.
                while(true) {
                    Console.WriteLine("This game already appears to have been converted. Revert changes? [Y/N] ");
                    string response = Console.ReadLine();
                    if (response == "y" || response == "Y") {
                        Revert(gameName);
                        Environment.Exit(0);
                    }
                    if (response == "n" || response == "N") {
                        Console.WriteLine("Placeholder.");
                        Environment.Exit(0);
                    }
                }
            }
            GenerateBat(gameName);
            ReplaceExecutable(gameName);
        }

        static void Revert(string gameName) {
            Console.WriteLine("Replacing game's exe file with backup version & removing bat file.");
            File.Delete(gameName + ".exe");
            File.Copy(gameName + "_backup.exe", gameName + ".exe");
            File.Delete("affinity_launch.bat");
            Console.WriteLine("Success?");
        }

        static bool IsThisCorrect(string gameName) {
            while (true) {
                Console.Write(gameName + " found. Is this correct? [Y/N] ");
                    string response = Console.ReadLine();
                    if (response == "y" || response == "Y") {
                        return true;
                    }
                    if (response == "n" || response == "N") {
                        return false;
                    }
            }
        }
        static string FindGame() {
            //Search the current directory for known games.
            if (File.Exists("swkotor2.exe")) {
                if (IsThisCorrect("Knights of the Old Republic II")) {
                    return "swkotor2";
                }
            }
            if (File.Exists("swkotor.exe")) {
                if (IsThisCorrect("Knights of the Old Republic")) {
                    return "swkotor";
                }
            }
            Console.Write("Please enter the name of the game's executable (eg, swkotor2.exe): ");
            while (true) {
                string gameName = Console.ReadLine();
                if (File.Exists(gameName)) {
                    //This is a dirty little hack where we strip off the game's exe if it exists. It could cause problems for some filenames.
                    string gameNameHack = gameName.Replace(".exe", "");
                    return gameNameHack;
                }
                Console.WriteLine();
                Console.Write("Filename does not exist. Please try again: ");
            }
        }
        //Generate the bat file by passing it an argument with the name of the discovered executable.
        //C# will replace the file if it already exists.
        static void GenerateBat(string executable) {
            string[] rawString = { "Start /affinity 1 " + executable + "_executable.exe" };
            System.IO.File.WriteAllLines(@"affinity_launch.bat", rawString);
        }

        static void ReplaceExecutable(string executable) {
            File.Copy(executable + ".exe", executable + "_executable.exe");
            File.Copy(executable + ".exe", executable + "_backup.exe");
            File.Delete(executable + ".exe");
            File.Copy("EasyCoreCompatibility.exe", executable + ".exe");
            //File.Replace(System.AppDomain.CurrentDomain.FriendlyName, executable, executable + "_executable");
        }

        static void RunProgram() {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "affinity_launch.bat";
            proc.Start();
        }
    }
}
