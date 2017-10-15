using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SMMcli
{
    class SMMcli
    {
        static void Main()
        {
            string cwd = Directory.GetCurrentDirectory();
            Console.WriteLine("##########################");
            Console.WriteLine("Thank you for using SMMcli");
            Console.WriteLine("##########################");
            if (!File.Exists(cwd + "\\smmcli.conf"))
            {
                Console.Write("\nTo get started we need to configure SMMcli first");
                Console.Write("\nWe need to where your game files are located. \nFor this you need to enter the Path to your game files.");
                Console.Write("\nPlease let me know where you Call of Chernobyl folder is:\n");
                string rootDir = Console.ReadLine();
                if (!Directory.Exists(rootDir))
                {
                    Console.Write("Something went wrong ... Try again");
                    rootDir = Console.ReadLine();
                }
                Console.Write("\nThanks. ");
                Console.Write("Now, where shall SMMcli save stuff:\n");
                string saveLocation = Console.ReadLine();
                if (!Directory.Exists(saveLocation))
                {
                    Console.Write("Something went wrong ... Try again");
                    saveLocation = Console.ReadLine();
                }
                string saveFileTextRootDir = "rootDir: \"" + rootDir + "\"";
                string saveFileTextSaveLocation = "saveLocation: \"" + saveLocation + "\"";
                string[] saveFileText = { saveFileTextRootDir, saveFileTextSaveLocation };
                SmmcliFunction.WriteFile("smmcli.conf", saveFileText,"DEFAULT");
            }
            Console.WriteLine("smmcli.conf found ...");
            Console.WriteLine("DEBUG: " + cwd + "\\smmcli.conf");

            do
            {
                SmmcliFunction.Menu("menu");
            } while (true);
        }
    }
    class SmmcliFunction
    {
        
        internal class Snapshot
        {
            List<string> fd = new List<string>();
            public Snapshot(string dir)
            {
                string[] dirs = Directory.GetDirectories(dir);
                string[] files = Directory.GetFiles(dir);
                foreach (string d in dirs)
                {
                    Console.Write("looking at {0}\n", d);
                    new Snapshot(d);
                    fd.Add(d);
                }
                foreach (string f in files)
                {
                    Console.Write("looking at {0}\n", f);
                    fd.Add(f);
                }
                string[] content = fd.ToArray();
                WriteFile("gameconf.text", content, "DEFAULT");
                return;
            }
        }
        public static void WriteFile(string fileName, string[] content, string path)
        {
            if(path == "DEFAULT")
            {
                path = Directory.GetCurrentDirectory();
            }
            try
            {
                using (StreamWriter file =
                new StreamWriter(path + "\\" + fileName))
                {
                    foreach (string line in content)
                    {
                        file.WriteLine(line);
                        Console.Write("Writing file ...\n");
                    }
                }
                Console.Write(path + "\\" + fileName + " created...\n");
                return;
            }
            catch
            {
                Console.Write("Failed to write {0}\n", fileName);

            }
            return;
        }

        public static void Menu(string arg)
        {
            switch (arg)
            {
                case "1":
                    Console.Write("Path to snapshot:\n");
                    string path = Console.ReadLine();
                    Console.Write("Name of snapshot:\n");
                    string name = Console.ReadLine();
                    name = name + ".txt";
                    try
                    {
                        new Snapshot(path);
                        Console.Write("Erfolg!\n");
                    }
                    catch
                    {
                        Console.Write("Fehler! HAHA\n");
                    }
                    finally
                    {
                        Console.Write("Exiting ... \n");
                    }
                    break;
                case "2":
                    Console.WriteLine("Case 2");
                    break;
                case "3":
                    Console.WriteLine("Edit game");
                    Console.WriteLine("Enter game name:");
                    string game = Console.ReadLine();
                    Console.WriteLine("Enter game path:\n");
                    string gamePath = Console.ReadLine();

                    break;
                case "4":
                    Console.WriteLine("Case 2");
                    break;
                case "5":
                    break;
                case "menu":
                    Console.WriteLine("Enter one of the numbers");
                    Console.WriteLine("1) create new snapshot 2) load snapshot 3) edit game\n4) delete game 5) exit");
                    string choice = Console.ReadLine();
                    SmmcliFunction.Menu(choice);
                    break;
                default:
                    Console.WriteLine("Not a valid option");
                    break;
            }
            return;
        }
    }
}