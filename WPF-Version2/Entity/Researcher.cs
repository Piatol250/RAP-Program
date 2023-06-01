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
        public string JobtTitle
        {
            get {
                switch (EmploymentLevel)
                {
                    case LEVEL.Student:
                        return "Student";
                    case LEVEL.A:
                        return "Research Associate";
                    case LEVEL.B:
                        return "Lecturer";
                    case LEVEL.C:
                        return "Assistant Professor";
                    case LEVEL.D:
                        return "Associate Professor";
                    case LEVEL.E:
                        return "Professor";
                    default:
                        return "Unknown";
                };
            }
            set
            {
            }
        }
        public int NumPublications { get { return Publications.Count; } set { } }
        public string Tenure { get; set; }
        public DateTime CommencedWithInstitution { get; set; }
        public DateTime CommencedCurrentPostion
        { get
            {
                DateTime lastDate = CommencedWithInstitution;

                for (int pos = 0; pos < Positions.Count; pos++)
                {
                    if (pos == Positions.Count - 1)
                    {
                        lastDate = Positions[pos].start;
                    }
                }
                return lastDate;
            } set { } }
        public List<Position> Positions { get; set; }
        public List<Publication> Publications;
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
            return Title + " " + Given_Name + " " + Family_Name;
        }


    }
}


