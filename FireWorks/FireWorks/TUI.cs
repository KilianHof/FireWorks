using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    class TUI : IUserLayer
    {
        public TUI()
        {

        }
        public void Display(string str)
        {
            Console.WriteLine(str);
        }
        public int GetInt()
        {
            Console.WriteLine("Type a number");
            string Input = Console.ReadLine();
            for (int i = 0; i < Input.Length; i++)
            {
                if (!char.IsDigit(Input[i]))
                {
                    return GetInt();
                }
            }
            return int.Parse(Input);
        }
        public string GetString()
        {
            return Console.ReadLine();
        }
        public bool GetBool()
        {
            Console.WriteLine("(y/n)?");
            string str = Console.ReadLine();
            if ((str == "y") || (str == "Y") || (str == "Yes") || (str == "yes"))
                return true;
            else
                return false;
        }

    }
}
