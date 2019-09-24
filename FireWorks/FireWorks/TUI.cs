using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    class TUI : IUserLayer
    {
        private readonly int width = 120;
        private readonly int height = 30;
        private readonly int maxLength = 115;
        private readonly bool isFancy = false;

        public TUI()
        {
            Console.SetWindowSize(width, height);
        }
        public void Display(string str)
        {
            if (isFancy)
            {
                for (int i = 0; i < width - 1; i++)         // Top Row
                {
                    Console.Write("X");
                }

                Console.Write("\n");
                Console.Write("X ");
                string Timedate = DateTime.Now.ToString("dddd, dd MMMM yyyy HH: mm:ss");
                Console.Write("Time and Date: " + Timedate);

                for (int i = 0; i < width - (1 + Timedate.Length + 18); i++)         // Statusbar puffer
                {
                    Console.Write(" ");
                }
                Console.Write("X\n");
                for (int i = 0; i < width - 1; i++)         // Second Row
                {
                    Console.Write("X");
                }
                Console.Write("\n");
                string[] splitted = str.Split('\n');
                for (int j = 0; j < splitted.Length; j++)
                {
                    Console.Write("X ");
                    Console.Write(splitted[j]);
                    for (int i = 0; i < width - (1 + splitted[j].Length + 3); i++)         // display puffer
                    {
                        Console.Write(" ");
                    }

                    Console.Write("X\n");
                }
               
                Console.Write("X\n");

            }
            else
                Console.Write(str);
        }
        public string LineConstructor(string str)
        {
            string tmp = "";
            string[] splits = SplitStrings(str);
            foreach (var item in splits)
            {
                
                tmp += "X "+ item + "";
                for (int i = 0; i < maxLength-(item.Length); i++)
                {
                    tmp += " ";
                }
                tmp += " X\n";    
                    
            }
            return tmp;
        }
        public string[] SplitStrings(string str)
        {
            List<string> tmp = new List<string>();
            string[] splits = str.Split('\n');
            for (int i = 0; i < splits.Length; i++)
            {
                if (splits[i].Length > maxLength)
                {
                    tmp.AddRange(Chunk(splits[i]));
                }
                else
                {
                    tmp.Add(splits[i]);
                }
            }
            return tmp.ToArray();
        }
        public string[] Chunk(string str)
        {
            List<string> normalized = new List<string>();
            string[] splits = str.Split(' ');

            int count = 0;
            string tmp = "";
            for (int i = 0; i < splits.Length; i++)
            {
                tmp += splits[i];
                tmp += " ";
                count += splits[i].Length;
                count++; //leerzeichen
                if (i < splits.Length - 1)
                {
                    if (count+splits[i+1].Length > maxLength)
                    {
                        normalized.Add(tmp);
                        count = 0;
                        tmp = "";
                    }
                }
            }
            normalized.Add(tmp);
            return normalized.ToArray();
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
