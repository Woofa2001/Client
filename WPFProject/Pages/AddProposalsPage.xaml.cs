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
    /// Логика взаимодействия для AddProposalsPage.xaml
    /// </summary>
    public partial class AddProposalsPage : Page
    {
        int steps;
        public AddProposalsPage()
        {
            InitializeComponent();
            steps = 0;
            DataContext = this;
            PeopleDataGrid.ItemsSource = SourceCore.DB.PEOPLE.ToList();
            PeopleDataGrid1.ItemsSource = SourceCore.DB.PEOPLE.ToList();
            ProposalsDataGrid.ItemsSource = SourceCore.DB.PROPOSALS.ToList();
        }

        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            steps++;
            if (steps == 1)
            {
                LabelSteps.Content = "Шаг 2 - Выберите предложение";
                GridReal.Width = new GridLength(0);
                GridProposals.Width = new GridLength(1, GridUnitType.Star);
            }
            if(steps == 2)
            {
                LabelSteps.Content = "Шаг 3 - Выберите покупателя/дату/комиссию";
                NextStepButton.Content = "Завершить";
                GridProposals.Width = new GridLength(0);
                GridCustomers.Width = new GridLength(1, GridUnitType.Star);
                ChangeColumn.Width = new GridLength(150);
            }
            if(steps == 3)
            {
                var A = new Data.DEALS();
                A.PEOPLE1= (Data.PEOPLE)PeopleDataGrid.SelectedItem;
                A.PROPOSALS = (Data.PROPOSALS)ProposalsDataGrid.SelectedItem;
                A.PEOPLE = (Data.PEOPLE)PeopleDataGrid1.SelectedItem;
                A.COMM_REAL = Int32.Parse(RealCommTextBox.Text);
                A.DATA_DEALS = DateTime.Parse(DateDealsPicker.Text);
                SourceCore.DB.DEALS.Add(A);
                SourceCore.DB.SaveChanges();
             //   Pages.DealsPage.Close
            }
        }
    }
}
