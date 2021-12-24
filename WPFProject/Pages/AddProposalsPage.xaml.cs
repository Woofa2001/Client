using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private Pages.DealsPage dealsPage;
        int steps;
        public AddProposalsPage(Pages.DealsPage DealPages)
        {
            InitializeComponent();
            DataContext = this;
            PeopleDataGrid.ItemsSource = SourceCore.DB.PEOPLE.ToList();
            PeopleDataGrid1.ItemsSource = SourceCore.DB.PEOPLE.ToList();
            ProposalsDataGrid.ItemsSource = SourceCore.DB.PROPOSALS.ToList();
            dealsPage = DealPages;
            steps = 0;
        }

        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {

            steps++;
            Proverka();

        }

        private void PeopleFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            switch (PeopleFilterComboBox.SelectedIndex)
            {
                case 0:
                    PeopleDataGrid.ItemsSource = SourceCore.DB.PEOPLE.Where(filtercase => filtercase.SURNAME.Contains(textbox.Text)).ToList();
                    break;
                case 1:
                    PeopleDataGrid.ItemsSource = SourceCore.DB.PEOPLE.Where(filtercase => filtercase.NAME.Contains(textbox.Text)).ToList();
                    break;
                case 2:
                    PeopleDataGrid.ItemsSource = SourceCore.DB.PEOPLE.Where(filtercase => filtercase.MIDNAME.Contains(textbox.Text)).ToList();
                    break;
            }
        }

        private void PeopleFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<String> Columns = new List<string>();
            for (int I = 0; I < 3; I++)
            {
                Columns.Add(PeopleDataGrid.Columns[I].Header.ToString());
            }
            PeopleFilterComboBox.ItemsSource = Columns;
            PeopleFilterComboBox.SelectedIndex = 0;
            foreach (DataGridColumn Column in PeopleDataGrid.Columns)
            {
                Column.CanUserSort = false;
            }

            List<String> ColumnsProposals = new List<string>();
            for (int J = 0; J < 11; J++)
            {
                ColumnsProposals.Add(ProposalsDataGrid.Columns[J].Header.ToString());
            }
            ProposalsFilterComboBox.ItemsSource = ColumnsProposals;
            ProposalsFilterComboBox.SelectedIndex = 0;
            foreach (DataGridColumn ColumnProposals in ProposalsDataGrid.Columns)
            {
                ColumnProposals.CanUserSort = false;
            }

            List<String> ColumnsPeople = new List<string>();
            for (int K = 0; K < 3; K++)
            {
                ColumnsPeople.Add(PeopleDataGrid1.Columns[K].Header.ToString());
            }
            People1FilterComboBox.ItemsSource = ColumnsPeople;
            People1FilterComboBox.SelectedIndex = 0;
            foreach (DataGridColumn ColumnPeople in PeopleDataGrid1.Columns)
            {
                ColumnPeople.CanUserSort = false;
            }

        }

        private void ProposalsFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((ProposalsFilterComboBox.SelectedIndex == 8) || (ProposalsFilterComboBox.SelectedIndex == 9) || (ProposalsFilterComboBox.SelectedIndex == 11))
            {
                ProposalsFilterTextBox1.Visibility = Visibility.Visible;
                ProposalsFilterTextBox.Width = 85;
            }
            else
            {
                ProposalsFilterTextBox1.Visibility = Visibility.Hidden;
                ProposalsFilterTextBox.Width = 175;
            }

        }

        private void ProposalsFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            switch (ProposalsFilterComboBox.SelectedIndex)
            {
                case 0:
                    ProposalsDataGrid.ItemsSource = SourceCore.DB.PROPOSALS.Where(filtercase => filtercase.PEOPLE.SURNAME.Contains(textbox.Text)).ToList();
                    break;
                case 1:
                    ProposalsDataGrid.ItemsSource = SourceCore.DB.PROPOSALS.Where(filtercase => filtercase.TYPE_OBJECTS.NAME_TYPE.Contains(textbox.Text)).ToList();
                    break;
                case 2:
                    ProposalsDataGrid.ItemsSource = SourceCore.DB.PROPOSALS.Where(filtercase => filtercase.AREAS.TYPE_AREA.Contains(textbox.Text)).ToList();
                    break;
                case 3:
                    ProposalsDataGrid.ItemsSource = SourceCore.DB.PROPOSALS.Where(filtercase => filtercase.STREET.Contains(textbox.Text)).ToList();
                    break;
                case 4:
                    ProposalsDataGrid.ItemsSource = SourceCore.DB.PROPOSALS.Where(filtercase => filtercase.FLAT.Value.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 5:
                    ProposalsDataGrid.ItemsSource = SourceCore.DB.PROPOSALS.Where(filtercase => filtercase.FLOOR.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 6:
                    ProposalsDataGrid.ItemsSource = SourceCore.DB.PROPOSALS.Where(filtercase => filtercase.COUNT_FLOORS.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 7:
                    ProposalsDataGrid.ItemsSource = SourceCore.DB.PROPOSALS.Where(filtercase => filtercase.COUNT_ROOMS.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 8:
                    if ((ProposalsFilterTextBox1.Text != "") || (ProposalsFilterTextBox.Text != ""))
                    {
                        int.TryParse(ProposalsFilterTextBox.Text, out int val);
                        int.TryParse(ProposalsFilterTextBox1.Text, out int val1);
                        if ((ProposalsFilterTextBox.Text != "") && (ProposalsFilterTextBox1.Text == ""))
                        {
                            ProposalsDataGrid.ItemsSource = SourceCore.DB.PROPOSALS.Where(filtercase => filtercase.TOTAL_AREA.Value >= val).ToList();
                        }
                        else if ((ProposalsFilterTextBox1.Text != "") && (ProposalsFilterTextBox.Text == ""))
                        {
                            ProposalsDataGrid.ItemsSource = SourceCore.DB.PROPOSALS.Where(filtercase => filtercase.TOTAL_AREA.Value <= val1).ToList();
                        }
                        else
                        {
                            ProposalsDataGrid.ItemsSource = SourceCore.DB.PROPOSALS.Where(filtercase => (filtercase.TOTAL_AREA.Value >= val) && (filtercase.TOTAL_AREA.Value <= val1)).ToList();
                        }
                    }
                    else
                        ProposalsDataGrid.ItemsSource = SourceCore.DB.PROPOSALS.ToList();
                    break;
                case 9:
                    if ((ProposalsFilterTextBox1.Text != "") || (ProposalsFilterTextBox.Text != ""))
                    {
                        int.TryParse(ProposalsFilterTextBox.Text, out int val);
                        int.TryParse(ProposalsFilterTextBox1.Text, out int val1);

                        if ((ProposalsFilterTextBox.Text != "") && (ProposalsFilterTextBox1.Text == ""))
                        {
                            ProposalsDataGrid.ItemsSource = SourceCore.DB.PROPOSALS.Where(filtercase => filtercase.LIVING_AREA.Value >= val).ToList();
                        }
                        else if ((ProposalsFilterTextBox1.Text != "") && (ProposalsFilterTextBox.Text == ""))
                        {
                            ProposalsDataGrid.ItemsSource = SourceCore.DB.PROPOSALS.Where(filtercase => filtercase.LIVING_AREA.Value <= val1).ToList();
                        }
                        else
                        {
                            ProposalsDataGrid.ItemsSource = SourceCore.DB.PROPOSALS.Where(filtercase => (filtercase.LIVING_AREA.Value >= val) && (filtercase.LIVING_AREA.Value <= val1)).ToList();
                        }
                    }
                    else
                        ProposalsDataGrid.ItemsSource = SourceCore.DB.PROPOSALS.ToList();
                    break;
                case 10:
                    ProposalsDataGrid.ItemsSource = SourceCore.DB.PROPOSALS.Where(filtercase => filtercase.DESCRIPTION.Contains(textbox.Text)).ToList();
                    break;
                case 11:
                    if ((ProposalsFilterTextBox1.Text != "") || (ProposalsFilterTextBox.Text != ""))
                    {
                        decimal.TryParse(ProposalsFilterTextBox.Text, out decimal val);
                        decimal.TryParse(ProposalsFilterTextBox1.Text, out decimal val1);
                        if ((ProposalsFilterTextBox.Text != "") && (ProposalsFilterTextBox1.Text == ""))
                        {
                            ProposalsDataGrid.ItemsSource = SourceCore.DB.PROPOSALS.Where(filtercase => filtercase.COST.Value >= val).ToList();
                        }
                        else if ((ProposalsFilterTextBox1.Text != "") && (ProposalsFilterTextBox.Text == ""))
                        {
                            ProposalsDataGrid.ItemsSource = SourceCore.DB.PROPOSALS.Where(filtercase => filtercase.COST.Value <= val1).ToList();
                        }
                        else
                        {
                            ProposalsDataGrid.ItemsSource = SourceCore.DB.PROPOSALS.Where(filtercase => (filtercase.COST.Value >= val) && (filtercase.COST.Value <= val1)).ToList();
                        }


                    }
                    else
                        ProposalsDataGrid.ItemsSource = SourceCore.DB.PROPOSALS.ToList();
                    break;
            }
        }

        private void People1FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            switch (People1FilterComboBox.SelectedIndex)
            {
                case 0:
                    PeopleDataGrid1.ItemsSource = SourceCore.DB.PEOPLE.Where(filtercase => filtercase.SURNAME.Contains(textbox.Text)).ToList();
                    break;
                case 1:
                    PeopleDataGrid1.ItemsSource = SourceCore.DB.PEOPLE.Where(filtercase => filtercase.NAME.Contains(textbox.Text)).ToList();
                    break;
                case 2:
                    PeopleDataGrid1.ItemsSource = SourceCore.DB.PEOPLE.Where(filtercase => filtercase.MIDNAME.Contains(textbox.Text)).ToList();
                    break;
            }
        }

        private void People1FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void Proverka()
        {
            if (steps == 1)
            {
                if (PeopleDataGrid.SelectedItem != null)
                {
                    LabelSteps.Content = "Шаг 2 - Выберите предложение";
                    GridReal.Width = new GridLength(0);
                    GridProposals.Width = new GridLength(1, GridUnitType.Star);
                }
                else
                {
                    MessageBox.Show("Выберите запись",
                       "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                    steps--;
                }
            }

            if (steps == 2)
            {
                if (ProposalsDataGrid.SelectedItem != null)
                {
                    LabelSteps.Content = "Шаг 3 - Выберите покупателя/дату/комиссию";
                    NextStepButton.Content = "Завершить";
                    GridProposals.Width = new GridLength(0);
                    GridCustomers.Width = new GridLength(1, GridUnitType.Star);
                    ChangeColumn.Width = new GridLength(150);
                }
                else
                {
                    MessageBox.Show("Выберите запись",
                                      "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                    steps--;
                }
            }

            if (steps == 3)
            {
                if (PeopleDataGrid1.SelectedItem != null)
                {
                    var A = new Data.DEALS();
                    A.PEOPLE1 = (Data.PEOPLE)PeopleDataGrid.SelectedItem;
                    A.PROPOSALS = (Data.PROPOSALS)ProposalsDataGrid.SelectedItem;
                    A.PEOPLE = (Data.PEOPLE)PeopleDataGrid1.SelectedItem;
                    A.COMM_REAL = Int32.Parse(RealCommTextBox.Text);
                    A.DATA_DEALS = DateTime.Parse(DateDealsPicker.Text);
                    SourceCore.DB.DEALS.Add(A);
                    SourceCore.DB.SaveChanges();
                    dealsPage.DealsFrame.Content = null;
                }
                else
                {
                    MessageBox.Show("Выберите запись",
                                     "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                    steps--;
                }
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
