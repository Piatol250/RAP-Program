using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_Version2
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Window
    {
        public Reports()
        {
            InitializeComponent();
        }

        public void copyEmails(object sender, RoutedEventArgs e)
        {
            var baseobj = sender as FrameworkElement;
            var myObject = baseobj.DataContext as List<string>;
            string emails;

            if (myObject != null)
            {
                emails = string.Join(" ", myObject);
                Clipboard.SetText(emails);
            }


        }
    }
}
