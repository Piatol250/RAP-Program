using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP_Program_WPF
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
                        expectedPubs = 0.5M;
                        break;
                    default:
                        return "Unknown";
                };
                return Math.Round(100*(expectedPubs/ThreeYearAverage), 1).ToString() + "%"; 
            } 
            set { } 
        }
        public int getPubsInYear(int year)
        {
            int count = 0;

            foreach (Publication pub in Publications)
            {
                if (pub.PublicationDate == year)
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


