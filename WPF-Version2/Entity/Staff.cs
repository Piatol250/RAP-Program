using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP_WPF.Entity
{
    internal class Staff : Researcher
    { 
        public double FundingReceived { get; set; }
        public Decimal ThreeYearAverage
        {

            get
            {
                Decimal sum = 0;

                for (int count = 0; count < 3; count++)
                {
                    sum += getPubsInYear(DateTime.Now.Year - count - 2);
                }
                return Math.Round((sum / 3), 2);
            }
            set { }
        }
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
        public string Performance
        {
            get
            {
                Decimal expectedPubs;

                switch (EmploymentLevel)
                {
                    case LEVEL.Student:
                        return "None";
                    case LEVEL.A:
                        expectedPubs = 0.5M;
                        break;
                    case LEVEL.B:
                        expectedPubs = 1M;
                        break;
                    case LEVEL.C:
                        expectedPubs = 2M;
                        break;
                    case LEVEL.D:
                        expectedPubs = 3.2M;
                        break;
                    case LEVEL.E:
                        expectedPubs = 4M;
                        break;
                    default:
                        return "Unknown";
                };
                return Math.Round(100 * (ThreeYearAverage / expectedPubs), 1).ToString() + "%";
            }
            set { }
        }
        public List<Student> Supervisions { get; set; }

        public int SupervisionsCount { get { return Supervisions.Count();  } set { } }
    }
}
