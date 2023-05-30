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
                Researcher currResearcher = (Researcher)e.AddedItems[0];
                DetailsPanel.DataContext = e.AddedItems[0];
                PositionList.ItemsSource = researcherController.getPositions(currResearcher);
                image.Source = researcherController.getPhoto(currResearcher);
                Count.DataContext = e.AddedItems[0];
            }
        }

        private void getCumulativeCount(object sender, RoutedEventArgs e)
        {
            string publications;
            publications = string.Join(Environment.NewLine, ((Researcher)(Count.DataContext)).CumulativeCount);

            MessageBox.Show(publications);
        }
        
    }
}
