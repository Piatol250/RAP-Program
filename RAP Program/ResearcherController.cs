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
