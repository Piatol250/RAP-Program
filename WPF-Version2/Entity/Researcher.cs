using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP_WPF.Entity
{
    public enum LEVEL { Student, A, B, C, D, E }
    internal class Researcher
    {
        public LEVEL EmploymentLevel { get; set; }
        public int Id { get; set; }
        public string Given_Name { get; set; }
        public string Family_Name { get; set; }
        public string Title { get; set; }
        public string Unit { get; set; }
        public string Campus { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        private static List<string> all_levels = new List<string>()
        {
            "Student",
            "Research Associate",
            "Lecturer",
            "Assistant Professor",
            "Associate Professor",
            "Professor"
        };
        public static LEVEL ConvertLevelStringToEnum(string level)
        {
            for(int i = 0; i < all_levels.Count; i++)
            {
                if (all_levels[i] == level)
                {
                    return (LEVEL)i;
                }
            }

            throw new Exception("tried to convert invalid level string");
        }
        public List<string> GetAllLevels() { return all_levels; }
        public string JobTitle { get { return all_levels[(int)EmploymentLevel]; } set { } }
        public int NumPublications { get { return Publications.Count; } set { } }
        public string Tenure { get; set; }
        public DateTime CommencedWithInstitution { get; set; }
        public DateTime CommencedCurrentPostion
        { get
            {
                DateTime lastPos = CommencedWithInstitution;

                for (int pos = 0; pos < Positions.Count; pos++)
                {
                    if (pos == Positions.Count - 1)
                    {
                        lastPos = Positions[pos].start;
                    }
                }
                return lastPos;
            } set { } }
        public List<Position> Positions { get; set; }
        public List<Publication> Publications;
        public Decimal ThreeYearAverage 
        { 
           
            get 
            {
                Decimal sum = 0;

                for(int count = 0; count < 3;  count++)
                {
                    sum += getPubsInYear(DateTime.Now.Year - count - 2);    
                }
                return Math.Round((sum / 3), 2);
            } 
            set { } 
        }
        public List<string> CumulativeCount
        {
            get
            {
                List<string> cumulativeCount = new List<string>();
                List<int> years = new List<int>();
                int currentYear = CommencedWithInstitution.Year;

                for (int i = 0; i < (DateTime.Now.Year - CommencedWithInstitution.Year)-1; i++)
                {
                    years.Add(currentYear + i);
                }


                foreach (int year in years)
                {
                    cumulativeCount.Add(year.ToString() + ": " + getPubsInYear(year).ToString());
                }


                return cumulativeCount;
            }
            set { }
        }
        public string Performance
        { 
            get {
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
                return Math.Round(100*(ThreeYearAverage/expectedPubs), 1).ToString() + "%"; 
            } 
            set { } 
        }

        public string Q1Percent 
        {   
            get 
            {
                Decimal q1Count = 0M;

                foreach(Publication pub in this.Publications)
                {
                    if(pub.Rank == RANKING.Q1)
                    {
                        q1Count++;
                    }
                }
                return Math.Round(100*(q1Count/((Decimal)this.Publications.Count())), 1).ToString() + "%";
            } 
            set { } 
        }

        public int getPubsInYear(int year)
        {
            int count = 0;

            foreach (Publication pub in Publications)
            {
                if (pub.PublicationYear == year)
                {
                    count++;
                }
            }
            return count;
        }
        public override string ToString()
        {
            return Family_Name + ", " + Given_Name + " (" + Title + ")";
        }


    }
}


