using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP_Program
{
    public enum LEVEL { A, B, C, D, E }
    internal class Researcher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Unit { get; set; }
        public string Campus { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public string JobtTitle { get; set; }
        public double Tenure { get; set; }
        public int NumPublications { get; set; }
        public List<Publication> Publications;
        public DateTime CommencedWithInstitution { get; set; }
        public DateTime CommencedCurrentPosition { get; set; }


    }

    internal class Employee : Researcher
    {
        public LEVEL Level { get; set; }
        public double Q1Percent { get; set; }
        public double ThreeYearAverage { get; set; }
        public double FundingReceived { get; set; }
        public double PerformanceByPublication { get; set; }
        public double PerformanceByFundingReceived { get; set; }
        public List<Student> Supervisions { get; set; }

    }

    internal class Student : Researcher
    {
        public string Degree { get; set; }
        public int SupervisorID { get; set; }

    }
}


