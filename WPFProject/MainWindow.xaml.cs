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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int i = -1;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ShowAreasPage(object sender, RoutedEventArgs e)
        {
            i = 1;
            ShowPage();
        }
        private void ShowDealsPage(object sender, RoutedEventArgs e)
        {
            i = 5;
            ShowPage();
        }
        private void ShowPeoplesPage(object sender, RoutedEventArgs e)
        {
            i = 2;
            ShowPage();
        }
        private void ShowProposalsPage(object sender, RoutedEventArgs e)
        {
            i = 3;
            ShowPage();
        }
        private void ShowTypeObjectsPage(object sender, RoutedEventArgs e)
        {
            i = 4;
            ShowPage();
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            i++;
            ShowPage();

        }

        private void ShowPage()
        {
            if (i == 1 || i == 6)
            {
                RealAgencyFrame.Navigate(new Pages.AreasPage());
                i = 1;
            }
            if (i == 2)
            {
                RealAgencyFrame.Navigate(new Pages.PeoplesPage());
            }
            if (i == 3)
            {
                RealAgencyFrame.Navigate(new Pages.ProposalPage());
            }
            if (i == 4)
            {
                RealAgencyFrame.Navigate(new Pages.TypeObjectsPage());
            }
            if (i == 5 || i == 0 )
            {
                RealAgencyFrame.Navigate(new Pages.DealsPage());
                i = 5;
            }
            if (i == -1)
            {
                RealAgencyFrame.Content = null;
            }
        }

        private void PreviosPageButton_Click(object sender, RoutedEventArgs e)
        {
            i--;
            ShowPage();
        }

        private void ClosePageButton_Click(object sender, RoutedEventArgs e)
        {
            i = -1;
            ShowPage();
        }
    }
}
