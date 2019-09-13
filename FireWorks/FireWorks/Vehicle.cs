using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    public class Vehicle
    {
        public string Type { get; set; }
        public int EnginePower { get; set; }
        public int Seats { get; set; }
        public Vehicle(string t, int ep, int s)
        {
            Type = t;
            EnginePower = ep;
            Seats = s;
        }
    }
    public class ToolCarrier : Vehicle
    {
        public bool Chainsaw { get; set; }
        public ToolCarrier(string t, int ep, int s, bool cs) :
        base(t, ep, s)
        {
            Chainsaw = cs;
        }
    }
    class TurntableLadder : ToolCarrier
    {
        public int LadderHeight { get; set; }
        public TurntableLadder(string t, int ep, int s, bool cs, int lh) :
        base(t, ep, s, cs)
        {
            LadderHeight = lh;
        }
    }
    class FireTruck : ToolCarrier
    {
        public int FillQuantity { get; set; }
        public FireTruck(string t, int ep, int s, bool cs, int fq) :
        base(t, ep, s, cs)
        {
            FillQuantity = fq;
        }
    }
    class Ambulance : Vehicle
    {

        public int PatientWeight { get; set; }
        public Ambulance(string t, int ep, int s, int pw) :
        base(t, ep, s)
        {
            PatientWeight = pw;
        }
    }
    class Pkw : Vehicle
    {

        public Pkw(string t, int ep, int s) :
        base(t, ep, s)
        {
        }
    }
}
