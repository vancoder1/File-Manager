using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Explorer
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] menuItems = { "Item 1", "Item 2", "Item 3", "4", "5", "6"};
            FileExplorer fe = new FileExplorer(menuItems);
            FEConsoleMenu menu = new FEConsoleMenu(fe);

            while(true)
            {
                if (menu.PrintMenu() == 1)
                {
                    break;
                }
            }
        }
    }
}
