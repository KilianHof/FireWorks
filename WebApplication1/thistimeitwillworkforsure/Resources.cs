using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    /// <summary>
    /// Resource objects are used to store information about items that were used in a Deployment.
    /// </summary>
    public class Resources
    {
        public string Description { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Identifier = "Resource";
        

        public Resources(string d, int inv, string n)
        {
            Description = d;
            Id = inv;
            Name = n;
        }
        public string GetIdentifier()
        {
            return Identifier;
        }
        public override string ToString()
        {
            return Description + " " + Id;
        }
    }
    public class Hose : Resources
    {
        public new string Identifier = "Hose";
        public char Letter;
        public char GetLetter()
        {
            return Letter;
        }
        public void SetLetter(char value)
        {
            if (!value.Equals(null)) { }
            if (value == 'B' || value == 'C' || value == 'D')
                Letter = value;
        }
        public int HoseLength;
        public int GetHoseLength()
        {
            return HoseLength;
        }
        public void SetHoseLength(int value)
        {
            if (!value.Equals(null)) { }
            if (value == 5 || value == 10 || value == 20 || value == 30)
                HoseLength = value;
        }
        public Hose(string d, int inv, char l, int len) :
            base(d, inv, "Hose")
        {
            Name = "Hose";
            Letter = l;
            HoseLength = len;
        }
    }
    public class Gasanalyzer : Resources
    {
        public new string Identifier = "Gasanalyzer";
        public DateTime LastExaminationDate;
        public DateTime GetLastExaminationDate()
        {
            return LastExaminationDate;
        }
        public void SetLastExaminationDate()
        {
            LastExaminationDate = DateTime.Now;
        }
        public Gasanalyzer(string d, int inv) :
               base(d, inv, "Gasanalyzer")
        {
            LastExaminationDate = DateTime.Now;
        }
        public bool GasTimerCheck()
        {
            DateTime CurrentDate = DateTime.Now;    //Berechnet die Differenz zwischen der jetzigen Zeit und dem Zeitpunkt der letzten Gasmesserüberprüfung, checkt ob die Differenz über 30 Tagen liegt
            TimeSpan diff = CurrentDate.Subtract(LastExaminationDate);
            diff = diff.Duration();
            if (diff.TotalDays > 30)
            {
                LastExaminationDate = CurrentDate;
                return true;
            }
            return false;
        }
    }
    public class Distributer : Resources
    {
        public new string Identifier = "Distributer";
        public Distributer(string d, int inv) :
               base(d, inv, "Distributer")
        {
        }
    }
    public class Jetnozzle : Resources
    {
        public new string Identifier = "Jetnozzle";
        public Jetnozzle(string d, int inv) :
               base(d, inv, "Jetnozzle")
        {
        }
    }
}
