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
        static void Main(string[] args)
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
                Checkdir(rootDir);
                Console.Write("\nThanks. ");
                Console.Write("Now, where shall SMMcli save stuff:\n");
                string saveLocation = Console.ReadLine();
                Checkdir(saveLocation);
                string saveFileTextRootDir = "rootDir: \"" + rootDir + "\"";
                string saveFileTextSaveLocation = "saveLocation: \"" + saveLocation + "\"";
                string[] saveFileText = { saveFileTextRootDir, saveFileTextSaveLocation };
                WriteFile("smmcli.conf", saveFileText);
            }
            Console.WriteLine("smmcli.conf found ...");
            Console.WriteLine("DEBUG: " + cwd + "\\smmcli.conf");
            Console.WriteLine("Press one of the numbers");
            Console.WriteLine("1) create new snapshot 2) load snapshot 3) edit game\n4) delete game");
            Console.ReadKey(false);
            Console.ReadKey();
        }

        private static string Checkdir(string arg)
        {
            string result;
            if (Directory.Exists(arg))
            {
                result = "Directory \"" + arg + "\" found!\n";
            }
            else
            {
                result = "Directory not found. Please try again...\nPlease create the directory" + arg + "\n";
            }
            return result;
        }

        private static void WriteFile(string fileName, string[] content)
        {
            string path = Directory.GetCurrentDirectory();
            try
            {
                using (System.IO.StreamWriter file =
                new StreamWriter(path + "\\" + fileName))
                {
                    foreach (string line in content)
                    {
                        file.WriteLine(line);
                        Console.Write("Writing file ...\n");
                    }
                }
                Console.Write("smmcli.conf at "+ path + " created...\n");
            }
            catch
            {
                Console.Write("Failed to write smmcli.conf at " + path + "\n");

            }
            return;
        }
    }
}
