using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RAP_Program_WPF
{
    internal class ResearcherController
    {
        private List<Researcher> researchers;
        public List<Researcher> Researchers { get { return researchers; } set{ } }
        private ObservableCollection<Researcher> viewableResearchers;
        public ObservableCollection<Researcher> VisibleResearchers { get { return viewableResearchers; } set { } }

        public ResearcherController()
        {
            researchers = DBInterpreter.loadResearchers();

            foreach(Researcher researcher in researchers)
            {
                researcher.Publications = PublicationController.loadPublications(researcher.Id);
                researcher.Positions = DBInterpreter.loadPositions(researcher.Id);
                
                if(researcher.EmploymentLevel != LEVEL.Student)
                {
                    ((Staff)researcher).FundingReceived = DBInterpreter.getFunding(researcher.Id);
                    ((Staff)researcher).Supervisions = new List<Student>();

                    foreach (Researcher student in researchers)
                    {
                        if(student.EmploymentLevel == LEVEL.Student && ((Student)(student)).SupervisorID == researcher.Id)
                        {
                            ((Staff)(researcher)).Supervisions.Add((Student)student);
                        }   
                    }
                }
                else
                {
                    ((Student)(researcher)).Supervisor = getStaffById(((Student)(researcher)).SupervisorID);
                }
            }
            viewableResearchers = new ObservableCollection<Researcher>(researchers);
        }

        public Staff getStaffById(int id)
        {
            Staff match = null;

            foreach(Researcher researcher in researchers)
            {
                if(researcher.EmploymentLevel != LEVEL.Student && researcher.Id == id)
                {
                    match = (Staff)researcher;
                }
            }

            return match;
        }

        public ObservableCollection<Researcher> GetViewableList()
        {
            Console.WriteLine(VisibleResearchers);
            return VisibleResearchers;
        }

        public ObservableCollection<string> getPositions(Researcher researcher)
        {
            ObservableCollection<string> positions = new ObservableCollection<string>();

            if(researcher.Positions.Count > 1)
            {
                foreach (Position position in researcher.Positions)
                {
                    if(position != researcher.Positions.Last())
                    {
                        positions.Add(position.ToString());
                    }
                }
            }

            return positions;
        }

        public BitmapImage getPhoto(Researcher researcher)
        {
            BitmapImage bitmap = new BitmapImage();
            
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(researcher.Photo, UriKind.Absolute);
            bitmap.DecodePixelHeight = 101;
            bitmap.DecodePixelWidth = 90;
            bitmap.EndInit();
           
            return bitmap;
        }

        public void filterResearchersByLevel(LEVEL level)
        {
            var filtered = from Researcher researcher in researchers
                           where researcher.EmploymentLevel == level
                           select researcher;

            viewableResearchers.Clear();
            filtered.ToList().ForEach(viewableResearchers.Add);
        }

        public void filterResearchersByName(String filterText)
        {
            var filtered = from Researcher researcher in researchers
                           where researcher.Given_Name.ToLower().Contains(filterText.ToLower()) || researcher.Family_Name.ToLower().Contains(filterText.ToLower())
                           select researcher;
            viewableResearchers.Clear();
            filtered.ToList().ForEach(viewableResearchers.Add);
        }

    }
}
