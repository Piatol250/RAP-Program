using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP_Program
{
    internal class ResearcherController
    {
        public List<Researcher> researchers;
        public ResearcherController()
        {
            researchers = DBInterpreter.loadResearchers();

            foreach(Researcher researcher in researchers)
            {
                Console.WriteLine("Loaded " + researcher.Given_Name + " " + researcher.Family_Name + "\n" + "Tenure: " + researcher.Tenure);
                researcher.Publications = PublicationController.loadPublications(researcher.Id);
                researcher.Positions = DBInterpreter.loadPositions(researcher.Id);
                
                if(researcher.researcherType == TYPE.Staff)
                {
                    ((Staff)researcher).FundingReceived = DBInterpreter.getFunding(researcher.Id);
                    Console.WriteLine(((Staff)researcher).FundingReceived);
                }
            }
        }

        public List<Researcher> filterResearchers(TYPE researcherType)
        {
            var filtered = from Researcher researcher in researchers
                           where researcher.researcherType == researcherType
                           select researcher;

            return new List<Researcher>(filtered);
        }

    }
}
