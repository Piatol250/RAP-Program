using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
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
                }
                viewableResearchers = new ObservableCollection<Researcher>(researchers);
            }
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

        public int getPubsInYear(int year, Researcher researcher)
        {
            int count = 0; 

            foreach(Publication pub in researcher.Publications)
            {
                if(pub.PublicationDate == year)
                {
                    count++;
                }
            }
            return count;
        }
        public List<string> getCumulativeCount(Researcher researcher)
        {
            List<string> cumulativeCount = new List<string>();
            List<int> years = new List<int>();
            int currentYear = researcher.CommencedWithInstitution.Year;

            for(int i = 0; i < (DateTime.Now.Year-researcher.CommencedWithInstitution.Year); i++)
            {
                years.Add(currentYear + i);    
            }
            

            foreach(int year in years) 
            {
                cumulativeCount.Add(year.ToString() + ": " + getPubsInYear(year, researcher).ToString());
            }
                
            
            return cumulativeCount;
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
