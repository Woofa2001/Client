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
            switch(i)
                {
                case 0:
                    RealAgencyFrame.Navigate(new Pages.DealsPage());
                    i = 5;
                    break;     
                case 1:
                    RealAgencyFrame.Navigate(new Pages.AreasPage());
                break;
                case 2:
                    RealAgencyFrame.Navigate(new Pages.PeoplesPage());
                    break;
                case 3:
                    RealAgencyFrame.Navigate(new Pages.ProposalPage());
                    break;
                case 4:
                    RealAgencyFrame.Navigate(new Pages.TypeObjectsPage());
                    break;
                case 5:
                    RealAgencyFrame.Navigate(new Pages.DealsPage());
                    break;
                case 6:
                    RealAgencyFrame.Navigate(new Pages.AreasPage());
                    i = 1;
                    break;
                default:
                    RealAgencyFrame.Content = null;
                    break;

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
        public void ClosePage()
        {
            i = 5;
            ShowPage();
        }
    }

}
