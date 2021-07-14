using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace File_Explorer
{
    class Files_And_Dirictories
    {
        public string Path { get; set; }
        public string Name { get; set; }

        public Files_And_Dirictories(string name, string path)
        {
            Name = name;
            Path = path;
        }
        public Files_And_Dirictories(string path)
        {
            Path = path;
            string temp = path.Substring(path.LastIndexOf('\\') + 1, path.Length - path.LastIndexOf('\\') - 1);
            Name = temp;
        }
    }
    class FileManager
    {
        public List<Files_And_Dirictories> directoryItems { get; set; }
        public string currentDirectoryPath { get; set; } = "C:\\";

        public FileManager()
        {
            directoryItems = new List<Files_And_Dirictories>();

            UpdateDirectory();
        }
        public int ChangeDirectoryOrRunProcess(string path)
        {
            if (path == "disk_choice")
            {
                return 1;
            }
            if (Directory.Exists(path))
            {
                currentDirectoryPath = path;
                UpdateDirectory();
                return 0;
            }
            else if (File.Exists(path))
            {
                ShowFileInfo(path);
                Console.ReadKey();
                Console.Clear();
                return 0;
            }
            return 0;
        }

        public List<string> Get_Disks()
        {
            List<string> Drives = new List<string>(Environment.GetLogicalDrives());
            foreach (string s in Drives)
            {
                Console.WriteLine(s);
            }
            return Drives;
        }

        public void UpdateDirectory()
        {
            try
            {
                directoryItems = new List<Files_And_Dirictories>();
                List<string> directoriesAndFiles = new List<string>();
                directoriesAndFiles = new List<string>(Directory.GetDirectories(currentDirectoryPath));
                directoriesAndFiles.AddRange(new List<string>(Directory.GetFiles(currentDirectoryPath)));

                if (currentDirectoryPath.LastIndexOf("\\") - currentDirectoryPath.IndexOf("\\") != 0)
                {
                    
                }
                else if (currentDirectoryPath.Count() == 3)
                {
                    directoryItems.Add(new Files_And_Dirictories("..Disc Choice", "disk_choice"));
                }
                else
                {
                    string backPath = currentDirectoryPath.Remove(currentDirectoryPath.LastIndexOf("\\") + 1);
                    directoryItems.Add(new Files_And_Dirictories("..", backPath));
                }
                
                for (int i = 0; i < directoriesAndFiles.Count; i++)
                {
                    directoryItems.Add(new Files_And_Dirictories(directoriesAndFiles[i]));
                }
            }
            catch(ArgumentOutOfRangeException)
            {
                Console.WriteLine("error");
            }
            
        }
        public void ShowFileInfo(string path)
        {
            FileInfo fi = new FileInfo(path);

            Console.Clear();
            Console.WriteLine("Press any key to return to the File Explorer");
            Console.WriteLine("\nFile name: " + fi.Name);
            Console.WriteLine("Path: " + fi.FullName);
            Console.WriteLine("Creation time: " + fi.CreationTimeUtc);
            Console.WriteLine("Size: " + fi.Length + " bytes");
        }
    }
    class FMConsoleMenu
    {
        public FileManager _fm { get; set; }
        public int _currentItem { get; private set; } = 0;
        private string _arrow = ">";

        public FMConsoleMenu(FileManager fm)
        {
            _fm = fm;
        }
        public int PrintMenu(string disk)
        {
            if (_fm.currentDirectoryPath.Length == 3)
            {
                _fm.currentDirectoryPath = disk;
                _fm.UpdateDirectory();
            }
            if (_currentItem > _fm.directoryItems.Count)
            {
                _currentItem = 0;
            }

            for (int i = 0; i < _fm.directoryItems.Count; i++)
            {
                if (i == _currentItem)
                {
                    Console.SetCursorPosition(0, i);                       
                    Console.Write(_arrow + " ");
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.SetCursorPosition(0, i);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("  ");
                }
                Console.WriteLine(_fm.directoryItems[i].Name);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);


            switch (keyInfo.Key)
            {

                case ConsoleKey.UpArrow:
                    if (_currentItem > 0)
                    {
                        _currentItem--;
                    }                           
                    break;
                case ConsoleKey.DownArrow:
                    if (_currentItem < _fm.directoryItems.Count - 1)
                    {
                        _currentItem++;
                    }                           
                    break;
                case ConsoleKey.Enter:
                    Console.Clear();
                    if (_fm.ChangeDirectoryOrRunProcess(_fm.directoryItems[_currentItem].Path) == 1)
                    {
                        _currentItem = 0;
                        return 1;
                    }
                    break;
                case ConsoleKey.Escape:
                    return 2;
            }
            return 0;
        }
        public string PrintMenu_DiskChoice(List<string> menuItems)
        {
            bool flag = true;
            while(flag)
            {
                for (int i = 0; i < menuItems.Count(); i++)
                {
                    if (i == _currentItem)
                    {
                        Console.SetCursorPosition(0, i);
                        Console.Write(_arrow + " ");
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.SetCursorPosition(0, i);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("  ");
                    }
                    Console.WriteLine(menuItems[i]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();


                switch (keyInfo.Key)
                {

                    case ConsoleKey.UpArrow:
                        if (_currentItem > 0)
                        {
                            _currentItem--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (_currentItem < menuItems.Count() - 1)
                        {
                            _currentItem++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        flag = false;
                        return menuItems[_currentItem];
                    case ConsoleKey.Escape:
                        Console.Clear();
                        flag = false;
                        return "esc";
                }
            }            
            return "continue";
        }
    }
}
