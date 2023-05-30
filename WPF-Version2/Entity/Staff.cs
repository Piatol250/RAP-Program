using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP_WPF.Entity
{
    internal class Staff : Researcher
    {
        private int pos;
        int count = 0;
        public double Q1Percent { get; set; }
        public double FundingReceived { get; set; }
        public string PerformanceByPublication 
        { 
            get 
            {
                Decimal tenure = (DateTime.Now.Year - this.CommencedWithInstitution.Year) - 2;
                Decimal performance = Math.Round(((Decimal)this.NumPublications / tenure), 1);

                return performance.ToString();    
            }
            set { } }
        public string PerformanceByFundingReceived 
        {
            get 
            {
                Decimal tenure = (DateTime.Now.Year - this.CommencedWithInstitution.Year) - 2;
                Decimal performance = Math.Round((Decimal)FundingReceived/tenure, 0);

                return performance.ToString() + "AUD/yr";
            }
            set { } 
        }
        public List<Student> Supervisions { get; set; }

        public int SupervisionsCount { get { return Supervisions.Count();  } set { } }
    }
}
