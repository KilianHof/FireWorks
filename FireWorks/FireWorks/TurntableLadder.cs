using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireWorks
{
    class TurntableLadder : ToolCarrier
    {
        public int LadderHeight { get; set; }
        public TurntableLadder(string t, int ep, int s, bool cs, int lh) :
        base(t, ep, s, cs)
        {
            LadderHeight = lh;
        }
    }
}
