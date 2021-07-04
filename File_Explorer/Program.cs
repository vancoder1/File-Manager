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
            FMConsoleMenu menu = new FMConsoleMenu(fe);

            List<string> disks = fe.Get_Disks();
            string disk_choice = "continue";
            while (disk_choice == "continue")
            {
                disk_choice = menu.PrintMenu_DiskChoice(disks);
            }
            
            if (disk_choice != "esc")
            {
                while (true)
                {
                    if(!menu.PrintMenu(disk_choice))
                    {
                        break;
                    }
                }
            }
        }
    }
}
