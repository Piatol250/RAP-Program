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

                for(int pos = 0; pos < Positions.Count; pos++)
                {
                    if(pos == Positions.Count - 1)
                    {
                        lastPos = Positions[pos].start;
                    }
                }
                return lastPos;
            } set { } }
        public List<Position> Positions { get; set; }
        public List<Publication> Publications;

        public override string ToString()
        {
            return Title + " " + Given_Name + " " + Family_Name;
        }


    }
}


