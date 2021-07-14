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
                    int menu_num = menu.PrintMenu(disk_choice);
                    if (menu_num == 2)
                    {
                        break;
                    }
                    else if (menu_num == 1)
                    {
                        disks = fe.Get_Disks();
                        disk_choice = "continue";
                        while (disk_choice == "continue")
                        {
                            disk_choice = menu.PrintMenu_DiskChoice(disks);
                        }
                    }
                }
            }
        }
    }
}
