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
                Console.WriteLine("Loaded " + researcher.Name);
                researcher.Publications = DBInterpreter.loadPublications(researcher.Id);
            }
         
        }

    }
}
