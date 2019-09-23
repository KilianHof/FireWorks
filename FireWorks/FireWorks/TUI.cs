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
            Console.Write(str);
        }
        public int GetInt()                                             //negative??
        {
            Console.WriteLine("Type a number");
            string Input = GetString();
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
            string str = Console.ReadLine();
            while (str == "") { str = Console.ReadLine(); }
            return str;
        }
        public bool GetBool()
        {
            Console.Write(" (y/n)?\n");
            string str = GetString();
            if ((str == "y") || (str == "Y") || (str == "Yes") || (str == "yes"))
                return true;
            else
                return false;
        }


    }
}
