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
            FileManager fe = new FileManager();
            ConsoleMenu menu = new ConsoleMenu(fe);

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
