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
    /// Interaction logic for AreasPage.xaml
    /// </summary>
    public partial class AreasPage : Page
    {
        string TYPE_AREA;
        public AreasPage()
        {
            InitializeComponent();
            DataContext= this;
            AreasDataGrid.ItemsSource = SourceCore.DB.AREAS.ToList();
        }

        private void ShowButtonAreas(object sender, RoutedEventArgs e)
        {
            if (ChangeColumn.Width.Value == 0)
            {
                ChangeColumn.Width = new GridLength(250);
                SplitterColumn.Width = GridLength.Auto;
                AreasDataGrid.Focus();
                AreasDataGrid.IsHitTestVisible = false;
                AreasFilterComboBox.IsHitTestVisible = false;
                AreasFilterTextBox.IsHitTestVisible = false;
                if ((sender as Button).Content.ToString() == "Добавить")
                {
                    AreasDataGrid.SelectedItem = null;
                }
                if (((sender as Button).Content.ToString() == "Копировать")&&(AreasDataGrid.SelectedItem!=null))
                {
                    TYPE_AREA = AreaTypeTextBox.Text;
                    AreasDataGrid.SelectedItem = null;
                    AreaTypeTextBox.Text = TYPE_AREA;
                }
            }
            else
            {
                ChangeColumn.Width = new GridLength(0);
                SplitterColumn.Width = new GridLength(0);
            }
        }

        private void CloseEdChangeClick(object sender, RoutedEventArgs e)
        {
            ChangeColumn.Width = new GridLength(0);
            SplitterColumn.Width = new GridLength(0);
            AreasDataGrid.IsHitTestVisible = true;
            AreasFilterComboBox.IsHitTestVisible = true;
            AreasFilterTextBox.IsHitTestVisible = true;
        }

        private void CommitButtonAreas(object sender, RoutedEventArgs e)
        {
            if (AreaTypeTextBox.Text != "")
            {
                var A = new Data.AREAS();
                A.TYPE_AREA = AreaTypeTextBox.Text;
                if (AreasDataGrid.SelectedItem == null)
                {
                    SourceCore.DB.AREAS.Add(A);
                }
                SourceCore.DB.SaveChanges();
                CloseEdChangeClick(sender, e);
                UpdateGrid(A);
                AreasDataGrid.Focus();
            }
            else
            {
                MessageBox.Show("Введите Район",
                "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
            }
        }

        private void DeleteButtonAreas(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Удалить запись?","Внимание!",MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    // Ссылка на удаляемую книгу
                    Data.AREAS DeletingAreas = (Data.AREAS)AreasDataGrid.SelectedItem;
                    // Определение ссылки, на которую должен перейти указатель после удаления
                    if (AreasDataGrid.SelectedIndex < AreasDataGrid.Items.Count - 1)
                    {
                        AreasDataGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (AreasDataGrid.SelectedIndex > 0)
                        {
                            AreasDataGrid.SelectedIndex--;
                        }
                    }
                    Data.AREAS SelectingArea = (Data.AREAS)AreasDataGrid.SelectedItem;
                    AreasDataGrid.SelectedItem = DeletingAreas;
                    SourceCore.DB.AREAS.Remove(DeletingAreas);
                    SourceCore.DB.SaveChanges();
                    UpdateGrid(SelectingArea);
                }
                catch
                {
                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
                AreasDataGrid.Focus();
            }
        }

        public void UpdateGrid(Data.AREAS Areas)
        {
            if ((Areas == null) && (AreasDataGrid.ItemsSource != null))
            {
                Areas = (Data.AREAS)AreasDataGrid.SelectedItem;
            }
            AreasDataGrid.ItemsSource = SourceCore.DB.AREAS.ToList();
            AreasDataGrid.SelectedItem = Areas;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Первоначальная настройка фильтра данных для быстрого поиска,
            // при этом из фильтрации нужно исключить столбец "Управление"
            // Создание собствнного списка заголовков столбцов
            List<String> Columns = new List<string>();
            // Перебор и добавление в новый список только необходимых заголовков
            // Исключен столбец 4
            for (int I = 0; I < 1; I++)
            {
                Columns.Add(AreasDataGrid.Columns[I].Header.ToString());
            }
            AreasFilterComboBox.ItemsSource = Columns;
            AreasFilterComboBox.SelectedIndex = 0;
            // Запрет на управление сортировкой щелчком по заголовку столбца
            foreach (DataGridColumn Column in AreasDataGrid.Columns)
            {
                Column.CanUserSort = false;
            }

        }

        private void AreasFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AreasFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            //BooksViewModel.View.Refresh();
            var textbox = sender as TextBox;
            switch (AreasFilterComboBox.SelectedIndex)
            {
                case 0:
                    AreasDataGrid.ItemsSource = SourceCore.DB.AREAS.Where(filtercase => filtercase.TYPE_AREA.Contains(textbox.Text)).ToList();
                break;
            }

        }
    }
}
