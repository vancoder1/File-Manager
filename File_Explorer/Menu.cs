using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Explorer
{
    class FileExplorer
    {
        public string[] menuItems { get; set; }
        public string currentDirectoryPath { get; private set; }
        public FileExplorer(string[] items)
        {
            menuItems = items;
        }
    }
    class FEConsoleMenu
    {
        FileExplorer fileExplorer;
        internal int _currentItem { get; private set; } = 0;
        private string _arrow = ">";

        public FEConsoleMenu(FileExplorer fe)
        {
            fileExplorer = fe;
        }
        public int PrintMenu()
        {
            for (int i = 0; i < fileExplorer.menuItems.Length; i++)
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
                Console.WriteLine(fileExplorer.menuItems[i]);
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
                    if (_currentItem < fileExplorer.menuItems.Length - 1)
                    {
                        _currentItem++;
                    }                           
                    break;
                case ConsoleKey.Escape:
                    return 1;
            }

            return 0;
        }
    }
}
