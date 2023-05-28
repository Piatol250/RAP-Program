using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP_Program
{
    class Program
    {
        public static void Main(string[] args)
        {
            //TestDriver.Run();
            //return;

            List<Researcher> filteredResearchers;
            ResearcherController controller = new ResearcherController();
            string type;
            TYPE filter = TYPE.Student;

            Console.WriteLine("Enter the type of researcher you want to filter by: ");
            type = Console.ReadLine();

            switch(type.ToLower())
            {
            case "student":
                filter = TYPE.Student;
                break;
            case "employee":
                filter = TYPE.Staff;
                break;
            }
            
            filteredResearchers = controller.filterResearchers(filter);

            foreach(Researcher researcher in filteredResearchers)
            {
                Console.WriteLine(researcher);
            }

            Console.ReadLine();
        }
    }
}
