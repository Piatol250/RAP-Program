using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP_Program
{
    public enum LEVEL { A, B, C, D, E }
    public enum TYPE { Employee, Student}
    internal class Researcher
    {
        public TYPE researcherType { get; set; }
        public int Id { get; set; }
        public string Given_Name { get; set; }
        public string Family_Name { get; set; }
        public string Title { get; set; }
        public string Unit { get; set; }
        public string Campus { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public string JobtTitle { get; set; }
        public int NumPublications { get; set; }
        public float Tenure { get; set; }
        public DateTime CommencedWithInstitution;
        public List<Position> Positions { get; set; }
        public List<Publication> Publications;

        public override string ToString()
        {
            return Title + " " + Given_Name + " " + Family_Name + " " + Unit + " " + Campus;
        }


    }
}


