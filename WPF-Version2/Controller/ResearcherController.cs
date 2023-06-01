using RAP_WPF.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RAP_WPF.Controller
{
    internal class ResearcherController
    {
        private List<Entity.Researcher> researchers;
        public List<Entity.Researcher> Researchers { get { return researchers; } set{ } }
        private ObservableCollection<Entity.Researcher> viewableResearchers;
        public ObservableCollection<Entity.Researcher> VisibleResearchers { get { return viewableResearchers; } set { } }

        public ResearcherController()
        {
            researchers = Data.DBInterpreter.loadResearchers();

            foreach(Entity.Researcher researcher in researchers)
            {
                researcher.Publications = PublicationController.loadPublications(researcher.Id);
                researcher.Positions = Data.DBInterpreter.loadPositions(researcher.Id);
                
                if(researcher.EmploymentLevel != Entity.LEVEL.Student)
                {
                    ((Entity.Staff)researcher).FundingReceived = Data.DBInterpreter.getFunding(researcher.Id);
                    ((Entity.Staff)researcher).Supervisions = new List<Entity.Student>();

                    foreach (Entity.Researcher student in researchers)
                    {
                        if(student.EmploymentLevel == Entity.LEVEL.Student && ((Entity.Student)(student)).SupervisorID == researcher.Id)
                        {
                            ((Entity.Staff)(researcher)).Supervisions.Add((Entity.Student)student);
                        }   
                    }
                }
                else
                {
                    ((Entity.Student)(researcher)).Supervisor = getStaffById(((Entity.Student)(researcher)).SupervisorID);
                }
            }
            viewableResearchers = new ObservableCollection<Entity.Researcher>(researchers);
        }

        public Entity.Staff getStaffById(int id)
        {
            Entity.Staff match = new Entity.Staff();

            foreach(Entity.Researcher researcher in researchers)
            {
                if(researcher.EmploymentLevel != Entity.LEVEL.Student && researcher.Id == id)
                {
                    match = (Entity.Staff)researcher;
                }
            }

            return match;
        }

        public ObservableCollection<Entity.Researcher> GetViewableList()
        {
            return VisibleResearchers;
        }

        public ObservableCollection<string> getPositions(Entity.Researcher researcher)
        {
            ObservableCollection<string> positions = new ObservableCollection<string>();

            if(researcher.Positions.Count > 1)
            {
                foreach (Entity.Position position in researcher.Positions)
                {
                    if(position != researcher.Positions.Last())
                    {
                        positions.Add(position.ToString());
                    }
                }
            }

            return positions;
        }

        public BitmapImage getPhoto(Entity.Researcher researcher)
        {
            BitmapImage bitmap = new BitmapImage();
            
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(researcher.Photo, UriKind.Absolute);
            bitmap.DecodePixelHeight = 101;
            bitmap.DecodePixelWidth = 90;
            bitmap.EndInit();
           
            return bitmap;
        }

        public void filterResearchersByLevel(Entity.LEVEL level)
        {
            var filtered = from Entity.Researcher researcher in researchers
                           where researcher.EmploymentLevel == level
                           select researcher;

            viewableResearchers.Clear();
            filtered.ToList().ForEach(viewableResearchers.Add);
        }

        public void filterResearchersByName(String filterText)
        {
            var filtered = from Entity.Researcher researcher in researchers
                           where researcher.Given_Name.ToLower().Contains(filterText.ToLower()) || researcher.Family_Name.ToLower().Contains(filterText.ToLower())
                           select researcher;
            viewableResearchers.Clear();
            filtered.ToList().ForEach(viewableResearchers.Add);
        }

        public Tuple<List<string>, List<string>> getPerformanceReport(Decimal performanceLower, Decimal performanceUpper)
        {
            Decimal performance;
            List<Entity.Staff> matchingStaff = new List<Entity.Staff>();
            List<string> orderedPerformances = new List<string>();
            List<string> emails = new List<string>();   
            Tuple<List<string>, List<string>> combo;

            foreach (Entity.Researcher researcher in researchers)
            {
                if(researcher.EmploymentLevel != Entity.LEVEL.Student)
                {
                    performance = Decimal.Parse(((Entity.Staff)(researcher)).Performance.Replace("%", ""));
                    Console.WriteLine(performanceLower <= performance && performance < performanceUpper);

                    if (performanceLower <= performance && performance < performanceUpper)
                    {
                        matchingStaff.Add((Entity.Staff)researcher);
                    }
                }
            }
            matchingStaff = matchingStaff.OrderBy(x => Decimal.Parse(x.Performance.Replace("%", "")))
                                    .ToList();

            foreach(Entity.Staff staff in matchingStaff)
            {
                orderedPerformances.Add(staff.Performance + "\t" + staff.Given_Name + " " + staff.Family_Name);
                emails.Add(staff.Email);
            }

            if(performanceLower == (Decimal)70.0 || performanceUpper == (Decimal)70.0)
            {
            }
            else
            {
                orderedPerformances.Reverse();
            }

            combo = new Tuple<List<string>, List<string>>(orderedPerformances, emails);

            return combo;
        }

    }
}
