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


namespace WPFProject.Pages
{
    /// <summary>
    /// Interaction logic for DealsPage.xaml
    /// </summary>
    public partial class DealsPage : Page
    {
        public DealsPage()
        {
            InitializeComponent();
            DataContext = this;
            DealsDataGrid.ItemsSource = SourceCore.DB.PEOPLE.ToList();
        }

        private void CommitButtonProposals(object sender, RoutedEventArgs e)
        {

            SourceCore.DB.SaveChanges();
            CloseEdChangeClick(sender, e);
        }

        private void CloseEdChangeClick(object sender, RoutedEventArgs e)
        {

        }

        private void ShowButtonDeals(object sender, RoutedEventArgs e)
        {
            DealsFrame.Navigate(new Pages.AddProposalsPage(this));
        }

        private void DeleteButtonDeals(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запись?", "Внимание!", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                SourceCore.DB.DEALS.Remove((Data.DEALS)DealsDataGrid.SelectedItem);
                SourceCore.DB.SaveChanges();
            }
        }

    }
}
