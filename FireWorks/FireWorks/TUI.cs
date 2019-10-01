using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    class TUI : IUserLayer
    {
        private int width = 120;
        private int height = 30;
        private readonly int maxLength = 115;
        private readonly bool isFancy = Globals.debug;
        public string message = "";

        public TUI()
        {
            Console.SetWindowSize(width, height);
        }
        public void Update()
        {
            if (isFancy || true)
            {
                Console.WriteLine(message);
                message = "";
            }
        }
        public void Display(string str)
        {
            char leftTop = '╔';
            char rightTop = '╗';
            char leftBottom = '╚';
            char rightBottom = '╝';

            char vertical = '║';
            char horizontal = '═';

            char vertRight = '╠';
            char vertLeft = '╣';


            if (isFancy || true)
            {
                Console.Clear();
                width = Console.WindowWidth;
                height = Console.WindowHeight;

                message += leftTop;
                for (int i = 0; i < width - 3; i++)         // Top Row
                {
                    message += horizontal;
                }
                message += rightTop;

                message += "\n";






                message += vertical+" ";
                string Timedate = DateTime.Now.ToString("dddd, dd MMMM yyyy HH: mm:ss"); // Statusbar
                message += "Time and Date: " + Timedate;




                for (int i = 0; i < width - (1 + Timedate.Length + 18); i++)         // Statusbar puffer
                {
                    message += " ";
                }
                message += vertical+"\n";





                message += vertRight;
                for (int i = 0; i < width - 3; i++)         // Second Row
                {
                    message += horizontal;
                }
                message += vertLeft;



                string[] splitted = str.Split('\n');
                for (int j = 0; j < splitted.Length; j++)
                {
                    message += "\n"+vertical+" ";
                        message += splitted[j];
                    for (int i = 0; i < width - (1 + splitted[j].Length + 3); i++)         // display puffer
                    {
                        message += " ";
                    }

                    message += vertical;
                }





                message += "\n";
                message += leftBottom;

                for (int i = 0; i < width - 3; i++)         // Bottom Row
                {
                    message += horizontal;
                }
                message += rightBottom;
                message += "\n";






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
                tmp += " X";    
                    
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
            Update();
            //Console.WriteLine("Type a number");
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
            Update();
            string str = Console.ReadLine();
            while (str == "") { str = Console.ReadLine(); }
            return str;
        }
        public bool GetBool()
        {
            //Console.Write(" (y/n)?\n");
            Update();
            string str = GetString();
            if ((str == "y") || (str == "Y") || (str == "Yes") || (str == "yes"))
                return true;
            else
                return false;
        }


    }
}
