using RAP_WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WPF_Version2;

namespace RAP_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string RESEARCHER_LIST_KEY = "researcherList"; //used to access the researchercontroller object
        private Controller.ResearcherController researcherController; //used to control a list of researchers, allows the filtering and creation of the list
        private Controller.PublicationController publicationController = new Controller.PublicationController();

        public MainWindow()
        {
            InitializeComponent();
            researcherController = (Controller.ResearcherController)(Application.Current.FindResource(RESEARCHER_LIST_KEY) as ObjectDataProvider).ObjectInstance;
        }


        private void searchBox_keyup(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                researcherController.filterResearchersByName(searchBox.Text);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                if (researcherController != null)
                {
                    researcherController.filterResearchersByLevel(Entity.Researcher.ConvertLevelStringToEnum(e.AddedItems[0].ToString()));
                }
                else
                {
                }
            }
        }

        private void researcherListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                ResearcherDetailsPanel.DataContext = e.AddedItems[0];
                PositionList.ItemsSource = researcherController.getPositions((Entity.Researcher)e.AddedItems[0]);
                image.Source = researcherController.getPhoto((Entity.Researcher)e.AddedItems[0]);
                Publications.ItemsSource = publicationController.sortPublications((Entity.Researcher)e.AddedItems[0]);
            }
        }

        private void getCumulativeCount(object sender, RoutedEventArgs e)
        {
            string publications;
            publications = string.Join(Environment.NewLine, ((Entity.Researcher)(ResearcherDetailsPanel.DataContext)).CumulativeCount);

            MessageBox.Show(publications);
        }

        private void getSuperVisions(object sender, RoutedEventArgs e)
        {
            string supervisions;
            supervisions = string.Join(Environment.NewLine, ((Entity.Staff)(ResearcherDetailsPanel.DataContext)).Supervisions);

            MessageBox.Show(supervisions);
        }

        private void getReports(object sender, RoutedEventArgs e)
        {
            Reports reports = new Reports();
            Tuple<List<string>, List<string>> stars = researcherController.getPerformanceReport((decimal)200.0, Decimal.MaxValue);
            Tuple<List<string>, List<string>> mins = researcherController.getPerformanceReport((decimal)110.0, (decimal)200.0);
            Tuple<List<string>, List<string>> belows = researcherController.getPerformanceReport((decimal)70.0, (decimal)110.0);
            Tuple<List<string>, List<string>> poors = researcherController.getPerformanceReport(0, (decimal)70.0);
            reports.Star.ItemsSource = stars.Item1;
            reports.Min.ItemsSource = mins.Item1;
            reports.Below.ItemsSource = belows.Item1;
            reports.Poor.ItemsSource = poors.Item1;
            reports.StarEmails.DataContext = stars.Item2;
            reports.MinEmails.DataContext = mins.Item2;
            reports.BelowEmails.DataContext = belows.Item2;
            reports.PoorEmails.DataContext = poors.Item2;
            reports.Show();
        }    

        public void publicationListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                PublicationDetailsPanel.DataContext = e.AddedItems[0];  
            }
        }
    }
}
