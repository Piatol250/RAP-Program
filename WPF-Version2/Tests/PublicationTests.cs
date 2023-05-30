
using RAP_WPF.Tests;
using System.Collections.Generic;

namespace RAP_WPF.Tests
{
    internal class PositionTests
    {
        public static void Collect(List<RunType> tests)
        {
            tests.Add(new RunType("load publications from an ID which doesn't exist", () =>
                {
                    List<Entity.Publication> all = Controller.PublicationController.loadPublications(-1);
                    
                    return (all.Count == 0);
                }
            ));

            tests.Add(new RunType("load publications from an id which does exist", () =>
                {
                    List<Entity.Publication> all = Controller.PublicationController.loadPublications(2);
                    
                    return (all.Count != 0);
                }
            ));
        }
    }
}
