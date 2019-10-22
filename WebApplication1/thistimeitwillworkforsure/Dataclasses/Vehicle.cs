using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    public class Vehicle
    {
        public string JSON { get; set; }
        public string Identifier = "Vehicle";
        public int Id { get; set; }
        public string Type { get; set; }
        public int EnginePower { get; set; }
        public int Seats { get; set; } 
        public Vehicle()
        {

        }
        public Vehicle(string type, int engine, int seats)

        {
            Type = type;
            EnginePower = engine;
            Seats = seats;
        } 
        public string GetIdentifier()
        {
            return Identifier;
        }
        public override string ToString()
        {
            return Type + " " + EnginePower + " " + Seats;
        }
        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Vehicle p = (Vehicle)obj;
                return (Type == p.Type) && (EnginePower == p.EnginePower) && (Seats == p.Seats);
            }
        }
        public override int GetHashCode()
        {
            var hashCode = 877455370;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
            hashCode = hashCode * -1521134295 + EnginePower.GetHashCode();
            hashCode = hashCode * -1521134295 + Seats.GetHashCode();
            return hashCode;
        }
    }
    public class ToolCarrier : Vehicle
    {
        public ToolCarrier()
        {

        }
        public bool Chainsaw { get; set; }
        public ToolCarrier(string type, int engine, int seats, bool cs) :
        base(type, engine, seats)
        {
            Chainsaw = cs;
        }
    }
    public class Turntableladder : ToolCarrier
    {
        public new string Identifier = "Turntableladder";
        public int LadderHeight { get; set; }
        Turntableladder()
        {

        }
        public Turntableladder(string type, int engine, int seats, bool cs, int ladder) :
        base(type, engine, seats, cs)
        {
            LadderHeight = ladder;
        }
    }
    public class Firetruck : ToolCarrier
    {
        public new string Identifier = "Firetruck";
        public int FillQuantity { get; set; }
        Firetruck()
        {

        }
        public Firetruck(string type, int engine, int seats, bool cs, int fill) :
        base(type, engine, seats, cs)
        {
            FillQuantity = fill;
        }
    }
    public class Ambulance : Vehicle
    {

        public new string Identifier = "Ambulance";
        public int PatientWeight { get; set; }
        public Ambulance()
        {

        }
        public Ambulance(string type, int engine, int seats, int Pweight) :
        base(type, engine, seats)
        {
            PatientWeight = Pweight;
        }
    }
    public class Pkw : Vehicle
    {

        public new string Identifier = "Pkw";
        public Pkw()
        {

        }
        public Pkw(string type, int engine, int seats) :
        base(type, engine, seats)
        {

        }
    }
}
