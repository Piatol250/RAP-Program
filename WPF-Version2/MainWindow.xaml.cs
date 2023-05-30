using RAP_Program_WPF;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace RAP_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string RESEARCHER_LIST_KEY = "researcherList"; //used to access the researchercontroller object
        private ResearcherController researcherController; //used to control a list of researchers, allows the filtering and creation of the list
        private PublicationController publicationController = new PublicationController();

        public MainWindow()
        {
            InitializeComponent();
            researcherController = (ResearcherController)(Application.Current.FindResource(RESEARCHER_LIST_KEY) as ObjectDataProvider).ObjectInstance;
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
                    researcherController.filterResearchersByLevel(DBInterpreter.ParseEnum<LEVEL>(e.AddedItems[0].ToString()));
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
                PositionList.ItemsSource = researcherController.getPositions((Researcher)e.AddedItems[0]);
                image.Source = researcherController.getPhoto((Researcher)e.AddedItems[0]);
                Publications.ItemsSource = publicationController.sortPublications((Researcher)e.AddedItems[0]);
            }
        }

        private void getCumulativeCount(object sender, RoutedEventArgs e)
        {
            string publications;
            publications = string.Join(Environment.NewLine, ((Researcher)(ResearcherDetailsPanel.DataContext)).CumulativeCount);

            MessageBox.Show(publications);
        }

        private void getSuperVisions(object sender, RoutedEventArgs e)
        {
            string supervisions;
            supervisions = string.Join(Environment.NewLine, ((Staff)(ResearcherDetailsPanel.DataContext)).Supervisions);

            MessageBox.Show(supervisions);
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
