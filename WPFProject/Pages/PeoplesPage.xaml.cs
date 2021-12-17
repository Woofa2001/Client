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
    /// Interaction logic for PeoplePage.xaml
    /// </summary>
    public partial class PeoplesPage : Page
    {
        string SURNAME;
        string NAME;
        string MIDNAME;
        public PeoplesPage()
        {
            InitializeComponent();
            DataContext = this;
            PeopleDataGrid.ItemsSource = SourceCore.DB.PEOPLE.ToList();
        }

        private void CommitButtonPeople(object sender, RoutedEventArgs e)
        {
            var A = new Data.PEOPLE();
            A.SURNAME = PeopleFTextBox.Text;
            A.NAME = PeopleITextBox.Text;
            A.MIDNAME = PeopleOTextBox.Text;
            if (PeopleDataGrid.SelectedItem == null)
            {
                SourceCore.DB.PEOPLE.Add(A);

            }
            SourceCore.DB.SaveChanges();
            UpdateGrid(A);
            PeopleDataGrid.Focus();
            CloseEdChangeClick(sender, e);
        }

        private void CloseEdChangeClick(object sender, RoutedEventArgs e)
        {
            ChangeColumn.Width = new GridLength(0);
            SplitterColumn.Width = new GridLength(0);
            PeopleDataGrid.IsHitTestVisible = true;
            PeopleFilterComboBox.IsHitTestVisible = true;
            PeopleFilterTextBox.IsHitTestVisible = true; 
        }

        private void ShowButtonPeople(object sender, RoutedEventArgs e)
        {
            if (ChangeColumn.Width.Value == 0)
            {
                ChangeColumn.Width = new GridLength(175);
                SplitterColumn.Width = GridLength.Auto;
                PeopleDataGrid.IsHitTestVisible = false;
                PeopleFilterComboBox.IsHitTestVisible = false;
                PeopleFilterTextBox.IsHitTestVisible = false;
                if ((sender as Button).Content.ToString() == "Добавить")
                {
                    PeopleDataGrid.SelectedItem = null;
                }
                if ((sender as Button).Content.ToString() == "Копировать")
                {
                    SURNAME = PeopleFTextBox.Text;
                    NAME = PeopleITextBox.Text;
                    MIDNAME = PeopleOTextBox.Text;
                    PeopleDataGrid.SelectedItem = null;
                    PeopleFTextBox.Text = SURNAME;
                    PeopleITextBox.Text = NAME;
                    PeopleOTextBox.Text = MIDNAME;
 
                }
            }
            else
            {
                ChangeColumn.Width = new GridLength(0);
                SplitterColumn.Width = new GridLength(0);
            }
        }

        public void UpdateGrid(Data.PEOPLE People)
        {
            if ((People == null) && (PeopleDataGrid.ItemsSource != null))
            {
                People = (Data.PEOPLE)PeopleDataGrid.SelectedItem;
            }
            PeopleDataGrid.ItemsSource = SourceCore.DB.PEOPLE.ToList();
            PeopleDataGrid.SelectedItem = People;
        }

        private void DeleteButtonPeople(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запись?", "Внимание!", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    // Ссылка на удаляемую книгу
                    Data.PEOPLE DeletingPeople = (Data.PEOPLE)PeopleDataGrid.SelectedItem;
                    // Определение ссылки, на которую должен перейти указатель после удаления
                    if (PeopleDataGrid.SelectedIndex < PeopleDataGrid.Items.Count - 1)
                    {
                        PeopleDataGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (PeopleDataGrid.SelectedIndex > 0)
                        {
                            PeopleDataGrid.SelectedIndex--;
                        }
                    }
                    Data.PEOPLE SelectingPeople = (Data.PEOPLE)PeopleDataGrid.SelectedItem;
                    PeopleDataGrid.SelectedItem = DeletingPeople;
                    SourceCore.DB.PEOPLE.Remove(DeletingPeople);
                    SourceCore.DB.SaveChanges();
                    UpdateGrid(SelectingPeople);
                }
                catch
                {
                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
                PeopleDataGrid.Focus();
            }
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
            // Первоначальная настройка фильтра данных для быстрого поиска,
            // при этом из фильтрации нужно исключить столбец "Управление"
            // Создание собствнного списка заголовков столбцов
            List<String> Columns = new List<string>();
            // Перебор и добавление в новый список только необходимых заголовков
            // Исключен столбец 4
            for (int I = 0; I < 3; I++)
            {
                Columns.Add(PeopleDataGrid.Columns[I].Header.ToString());
            }
            PeopleFilterComboBox.ItemsSource = Columns;
            PeopleFilterComboBox.SelectedIndex = 0;
            // Запрет на управление сортировкой щелчком по заголовку столбца
            foreach (DataGridColumn Column in PeopleDataGrid.Columns)
            {
                Column.CanUserSort = false;
            }

        }
    }
}
