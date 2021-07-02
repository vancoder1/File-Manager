﻿using System;
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
        public string currentDirectoryPath { get; private set; } = "C:\\";

        public FileManager()
        {
            directoryItems = new List<Files_And_Dirictories>();

            UpdateDirectory();
        }
        public void ChangeDirectoryOrRunProcess(string path)
        {
            currentDirectoryPath = path;
            UpdateDirectory();
        }

        public void UpdateDirectory()
        {

            List<string> directoriesAndFiles = new List<string>();
            directoriesAndFiles = new List<string>(Directory.GetDirectories(currentDirectoryPath));
            directoriesAndFiles.AddRange(new List<string>(Directory.GetFiles(currentDirectoryPath)));
            for (int i = 0; i < directoriesAndFiles.Count; i++)
            {
                directoryItems.Add(new Files_And_Dirictories(directoriesAndFiles[i]));
            }           
            
        }
    }
    class ConsoleMenu
    {
        public FileManager _fm { get; set; }
        public int _currentItem { get; private set; } = 0;
        private string _arrow = ">";

        public ConsoleMenu(FileManager fm)
        {
            _fm = fm;
        }
        public int PrintMenu()
        {
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
                    if (_currentItem < _fm.directoryItems.Count - 1)
                    {
                        _currentItem++;
                    }                           
                    break;
                case ConsoleKey.Enter:
                    Console.Clear();
                    _fm.ChangeDirectoryOrRunProcess(_fm.directoryItems[_currentItem].Path);
                    break;
                case ConsoleKey.Escape:
                    return 1;
            }

            return 0;
        }
    }
}
