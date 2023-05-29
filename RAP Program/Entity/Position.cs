using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP_Program
{
    enum EMPLOYMENTlEVEL { A, B, C, D, E }
    internal class Position
    {
        public EMPLOYMENTlEVEL Level { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }

        public override string ToString()
        {
            return start.ToString();
        }
    }



   
}
