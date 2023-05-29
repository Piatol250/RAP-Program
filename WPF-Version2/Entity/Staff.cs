using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP_Program_WPF
{
    internal class Staff : Researcher
    {
        private int pos;
        int count = 0;
        public double Q1Percent { get; set; }
        public double ThreeYearAverage { get; set; }
        public double FundingReceived { get; set; }
        public double PerformanceByPublication { get; set; }
        public double PerformanceByFundingReceived { get; set; }
        public List<Student> Supervisions { get; set; }

    }
}
