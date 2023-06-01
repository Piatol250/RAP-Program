using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP_WPF.Entity
{
    enum EMPLOYMENTlEVEL { A, B, C, D, E }
    internal class Position
    {
        public EMPLOYMENTlEVEL Level { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        
        //Summary: Converts employment level enum to matching job title
        public override string ToString()
        {
            string title;

            switch (Level)
            {
                case EMPLOYMENTlEVEL.A:
                    title = "Research Associate";
                    break;
                case EMPLOYMENTlEVEL.B:
                    title = "Lecturer";
                    break;
                case EMPLOYMENTlEVEL.C:
                    title = "Assistant Professor";
                    break;
                case EMPLOYMENTlEVEL.D:
                    title = "Associate Professor";
                    break;
                case EMPLOYMENTlEVEL.E:
                    title = "Professor";
                    break;
                default:
                    title = "Unknown";
                    break;
            };

            return start.ToShortDateString() + " " + end.ToShortDateString() + " " + title;
        }
    }



   
}
