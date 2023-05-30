using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace RAP_Program_WPF
{
    public enum RANKING { Q1, Q2, Q3, Q4 }
    public enum PUBTYPE { Conference, Journal, Other }

    internal class Publication
    {

        public RANKING Rank { get; set; }
        public PUBTYPE Type { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public string Doi { get; set; }
        public int PublicationYear { get; set; }
        public string CiteAs { get; set; }
        public DateTime AvailabilityDate { get; set; }
        public int Age
        {
            get
            {
                return ((DateTime.Now.Year - AvailabilityDate.Year)*365) + ((DateTime.Now.Month - AvailabilityDate.Month)*30) + (DateTime.Now.Day - AvailabilityDate.Day);
            }
            set { }
        }

        public override string ToString()
        {
            return PublicationYear + " " + Title;
        }
        


    }
    
}
