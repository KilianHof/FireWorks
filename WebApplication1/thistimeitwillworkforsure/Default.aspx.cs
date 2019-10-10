using System.Windows.Markup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace FireWorks
{
    public partial class _Default : Page
    {
        public void Page_Load(object sender, EventArgs e)
        {
            textout.Text = Globals.textout;
        }
        protected void Submit_button(object sender, EventArgs e)
        {
            Globals.textin = textin.Value.ToString();
            Globals.Submitted = 1;
        }
        protected void Refresh(object sender, EventArgs e)
        {
        }
        protected void Program_Start(object sender, EventArgs e)
        {
            FireWorksMain program = new FireWorksMain();
            program.Run();
        }
    }
    class TUI : IUserLayer
    {
        private int width = 120;
        private int height = 30;
        private int maxLength = 115;
        private readonly bool isFancy = Globals.debug;
        private string message = "";

        const char leftTop = '╔';
        const char rightTop = '╗';
        const char leftBottom = '╚';
        const char rightBottom = '╝';

        const char vertical = '║';
        const char horizontal = '═';

        const char vertRight = '╠';
        const char vertLeft = '╣';
        public TUI()
        {
            //Console.SetWindowSize(width, height);
        }
        private void Update()
        {
            if (isFancy)
            {
                Test(message);
                message = "";
            }
        }
        public void Display(string str)
        {
            Globals.textout += str;
        }

        private void Test(string str)
        {
            string internmessage = "";
            Console.Clear();
            width = Console.WindowWidth;
            height = Console.WindowHeight;
            maxLength = width - 5;

            internmessage += leftTop;
            for (int i = 0; i < width - 3; i++)         // Top Row
            {
                internmessage += horizontal;
            }
            internmessage += rightTop;
            internmessage += "<br />";

            internmessage += vertical + " ";
            string Timedate = DateTime.Now.ToString("dddd, dd MMMM yyyy HH: mm:ss"); // Statusbar
            internmessage += "Time and Date: " + Timedate;

            for (int i = 0; i < width - (1 + Timedate.Length + 18); i++)         // Statusbar puffer
            {
                internmessage += " ";
            }
            internmessage += vertical + "<br />";

            internmessage += vertRight;
            for (int i = 0; i < width - 3; i++)         // Second Row
            {
                internmessage += horizontal;
            }
            internmessage += vertLeft;

            string[] splitted = str.Split('\n');
            for (int j = 0; j < splitted.Length; j++)
            {
                internmessage += "<br />" + vertical + " ";
                internmessage += splitted[j];
                for (int i = 0; i < width - (1 + splitted[j].Length + 3); i++)         // display puffer
                {
                    internmessage += " ";
                }
                internmessage += vertical;
            }

            internmessage += "<br />";
            internmessage += leftBottom;

            for (int i = 0; i < width - 3; i++)         // Bottom Row
            {
                internmessage += horizontal;
            }
            internmessage += rightBottom;
            internmessage += "<br />";
            Console.WriteLine(internmessage);
        }

        public string LineConstructor(string str)
        {
            string tmp = "";
            string[] splits = SplitStrings(str);
            foreach (var item in splits)
            {
                tmp += "X " + item + "";
                for (int i = 0; i < maxLength - (item.Length); i++)
                {
                    tmp += " ";
                }
                tmp += " X";
            }
            return tmp;
        }
        private string[] SplitStrings(string str)
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
        private string[] Chunk(string str)
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
                    if (count + splits[i + 1].Length > maxLength)
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
        public string GetInput()
        {
            while (Globals.Submitted == 0)
            {
            }
        string input = Globals.textin;
        Globals.Submitted = 0;
        return input;
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
            string str = GetInput();
            while (str == "") { str = GetInput(); }
            return str;
        }
        public bool GetBool()
        {
            //Console.Write(" (y/n)?<br />");
            Update();
            string str = GetInput();
            if ((str == "y") || (str == "Y") || (str == "Yes") || (str == "yes") || (str == "YES"))
                return true;
            else
                return false;
        }
    }
}