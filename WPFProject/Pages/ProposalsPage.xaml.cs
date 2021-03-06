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
    /// Логика взаимодействия для ProposalPage.xaml
    /// </summary>
    /// 



    public partial class ProposalPage : Page
    {
        string SURNAME;
        string NAME;
        string MIDNAME;
        string NAME_TYPE;
        string TYPE_AREA;
        string STREET;
        string FLAT;
        string FLOOR;
        string COUNT_FLOORS;
        string COUNT_ROOMS;
        string TOTAL_AREA;
        string LIVING_AREA;
        string DESCRIPTION;
        string COST;

        public ProposalPage()
        {
            InitializeComponent();
            DataContext = this;
            ProposalsDataGrid.ItemsSource = SourceCore.DB.PROPOSALS.ToList();
            PeopleComboBox.ItemsSource = SourceCore.DB.PEOPLE.ToList();
            TypeObjectComboBox.ItemsSource = SourceCore.DB.TYPE_OBJECTS.ToList();
            AreasComboBox.ItemsSource = SourceCore.DB.AREAS.ToList();
        }

        private void CommitButtonProposals(object sender, RoutedEventArgs e)
        {
            if (PeopleComboBox.SelectedItem != null)
            {
                if (AreasComboBox.SelectedItem != null)
                {
                    if (TypeObjectComboBox.SelectedItem != null)
                    {
                        if (ProposalsStreetTextBox.Text != "")
                        {
                            if (ProposalsFlatTextBox.Text != "")
                            {
                                if (ProposalsFloorTextBox.Text != "")
                                {
                                    if (ProposalsCFloorTextBox.Text != "")
                                    {
                                        if (ProposalsRoomsTextBox.Text != "")
                                        {
                                            if (ProposalsTAreaTextBox.Text != "")
                                            {
                                                if (ProposalsTAreaTextBox.Text != "")
                                                {
                                                    if (ProposalsCostTextBox.Text != "")
                                                    {

                                                        var A = new Data.PROPOSALS();
                                                        A.PEOPLE = (Data.PEOPLE)PeopleComboBox.SelectedItem;
                                                        A.AREAS = (Data.AREAS)AreasComboBox.SelectedItem;
                                                        A.TYPE_OBJECTS = (Data.TYPE_OBJECTS)TypeObjectComboBox.SelectedItem;
                                                        A.STREET = ProposalsStreetTextBox.Text;
                                                        A.FLAT = Int32.Parse(ProposalsFlatTextBox.Text);
                                                        A.FLOOR = Int32.Parse(ProposalsFloorTextBox.Text);
                                                        A.COUNT_FLOORS = Int32.Parse(ProposalsCFloorTextBox.Text);
                                                        A.COUNT_ROOMS = Int32.Parse(ProposalsRoomsTextBox.Text);
                                                        A.TOTAL_AREA = Int32.Parse(ProposalsTAreaTextBox.Text);
                                                        A.LIVING_AREA = Int32.Parse(ProposalsLAreaTextBox.Text);
                                                        A.COST = (decimal)double.Parse(ProposalsCostTextBox.Text.Replace(".", ","));
                                                        A.DESCRIPTION = ProposalsDiscriptionTextBox.Text;
                                                        if (ProposalsDataGrid.SelectedItem == null)
                                                        {
                                                            SourceCore.DB.PROPOSALS.Add(A);
                                                        }
                                                        SourceCore.DB.SaveChanges();
                                                        UpdateGrid(A);
                                                        ProposalsDataGrid.Focus();
                                                        CloseEdChangeClick(sender, e);

                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Введите стоимость","Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Укажите жилую площадь","Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Укажите общую площадь","Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Укажите кол-во комнат","Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Укажите кол-во этажей в доме","Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Укажите этаж","Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Укажите номер дома","Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Укажите улицу","Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите тип объекта","Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                    }
                }
                else
                {
                    MessageBox.Show("Выберите район","Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
            }
            else
            {
                MessageBox.Show("Выберите продавца","Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
            }
        }



        private void CloseEdChangeClick(object sender, RoutedEventArgs e)
        {
            ChangeColumn.Width = new GridLength(0);
            SplitterColumn.Width = new GridLength(0);
            ProposalsDataGrid.IsHitTestVisible = true;
            ProposalsFilterComboBox.IsHitTestVisible = true;
            ProposalsFilterTextBox.IsHitTestVisible = true;
        }

        private void ShowButtonProposals(object sender, RoutedEventArgs e)
        {
            if (ChangeColumn.Width.Value == 0)
            {
                ChangeColumn.Width = new GridLength(300);
                SplitterColumn.Width = GridLength.Auto;
                ProposalsDataGrid.IsHitTestVisible = false;
                ProposalsFilterComboBox.IsHitTestVisible = false;
                ProposalsFilterTextBox.IsHitTestVisible = false;
                if ((sender as Button).Content.ToString() == "Добавить")
                {
                    ProposalsDataGrid.SelectedItem = null;

                }
                if ((sender as Button).Content.ToString() == "Копировать")
                {
                    PeopleComboBox.SelectedIndex = 1;

                    Data.PEOPLE people = (Data.PEOPLE)PeopleComboBox.SelectedItem;
                    SURNAME = people.SURNAME;
                    NAME = people.NAME;
                    MIDNAME = people.MIDNAME;
                    NAME_TYPE = TypeObjectComboBox.Text;
                    TYPE_AREA = AreasComboBox.Text;
                    STREET = ProposalsStreetTextBox.Text;
                    FLAT = ProposalsFlatTextBox.Text;
                    FLOOR = ProposalsFloorTextBox.Text;
                    COUNT_FLOORS = ProposalsCFloorTextBox.Text;
                    COUNT_ROOMS = ProposalsRoomsTextBox.Text;
                    TOTAL_AREA = ProposalsTAreaTextBox.Text;
                    LIVING_AREA = ProposalsLAreaTextBox.Text;
                    DESCRIPTION = ProposalsDiscriptionTextBox.Text;
                    COST = ProposalsCostTextBox.Text;
                    ProposalsDataGrid.SelectedItem = null;
                    PeopleComboBox.Text = SURNAME;
                    TypeObjectComboBox.Text = NAME_TYPE;
                    AreasComboBox.Text = TYPE_AREA;
                    ProposalsStreetTextBox.Text = STREET;
                    ProposalsFlatTextBox.Text = FLAT;
                    ProposalsFloorTextBox.Text = FLOOR;
                    ProposalsCFloorTextBox.Text = COUNT_FLOORS;
                    ProposalsRoomsTextBox.Text = COUNT_ROOMS;
                    ProposalsTAreaTextBox.Text = TOTAL_AREA;
                    ProposalsLAreaTextBox.Text = LIVING_AREA;
                    ProposalsDiscriptionTextBox.Text = DESCRIPTION;
                    ProposalsCostTextBox.Text = COST;

                }
            }
            else
            {
                ChangeColumn.Width = new GridLength(0);
                SplitterColumn.Width = new GridLength(0);
            }
        }

        private void DeleteButtonProposals(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запись?", "Внимание!", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    // Ссылка на удаляемую книгу
                    Data.PROPOSALS DeletingPROPOSALS = (Data.PROPOSALS)ProposalsDataGrid.SelectedItem;
                    // Определение ссылки, на которую должен перейти указатель после удаления
                    if (ProposalsDataGrid.SelectedIndex < ProposalsDataGrid.Items.Count - 1)
                    {
                        ProposalsDataGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (ProposalsDataGrid.SelectedIndex > 0)
                        {
                            ProposalsDataGrid.SelectedIndex--;
                        }
                    }
                    Data.PROPOSALS SelectingPROPOSALS = (Data.PROPOSALS)ProposalsDataGrid.SelectedItem;
                    ProposalsDataGrid.SelectedItem = DeletingPROPOSALS;
                    SourceCore.DB.PROPOSALS.Remove(DeletingPROPOSALS);
                    SourceCore.DB.SaveChanges();
                    UpdateGrid(SelectingPROPOSALS);
                }
                catch
                {
                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
                ProposalsDataGrid.Focus();
            }
        }

        public void UpdateGrid(Data.PROPOSALS PROPOSALS)
        {
            if ((PROPOSALS == null) && (ProposalsDataGrid.ItemsSource != null))
            {
                PROPOSALS = (Data.PROPOSALS)ProposalsDataGrid.SelectedItem;
            }
            ProposalsDataGrid.ItemsSource = SourceCore.DB.PROPOSALS.ToList();
            ProposalsDataGrid.SelectedItem = PROPOSALS;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Первоначальная настройка фильтра данных для быстрого поиска,
            // при этом из фильтрации нужно исключить столбец "Управление"
            // Создание собствнного списка заголовков столбцов
            List<String> Columns = new List<string>();
            // Перебор и добавление в новый список только необходимых заголовков
            // Исключен столбец 4
            for (int I = 0; I < 12; I++)
            {
                Columns.Add(ProposalsDataGrid.Columns[I].Header.ToString());
            }
            ProposalsFilterComboBox.ItemsSource = Columns;
            ProposalsFilterComboBox.SelectedIndex = 0;
            // Запрет на управление сортировкой щелчком по заголовку столбца

            foreach (DataGridColumn Column in ProposalsDataGrid.Columns)
            {
                Column.CanUserSort = false;
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
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }

}
