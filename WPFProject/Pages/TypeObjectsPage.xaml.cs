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
    /// Interaction logic for TypeObjectsPage.xaml
    /// </summary>
    public partial class TypeObjectsPage : Page
    {
        string NAME_TYPE;
        public TypeObjectsPage()
        {
            InitializeComponent();
            DataContext = this;
            TypeObjectDataGrid.ItemsSource = SourceCore.DB.TYPE_OBJECTS.ToList();
        }

        private void CloseEdChangeClick(object sender, RoutedEventArgs e)
        {
            ChangeColumn.Width = new GridLength(0);
            SplitterColumn.Width = new GridLength(0);
            TypeObjectDataGrid.IsHitTestVisible = true;
            TypeObjectsFilterComboBox.IsHitTestVisible = true;
            TypeObjectsFilterTextBox.IsHitTestVisible = true;
        }

        private void ShowButtonTypeObject(object sender, RoutedEventArgs e)
        {
            if (ChangeColumn.Width.Value == 0)
            {
                ChangeColumn.Width = new GridLength(175);
                SplitterColumn.Width = GridLength.Auto;
                TypeObjectDataGrid.IsHitTestVisible = false;
                TypeObjectsFilterComboBox.IsHitTestVisible = false;
                TypeObjectsFilterTextBox.IsHitTestVisible = false;
                if ((sender as Button).Content.ToString() == "Добавить")
                {
                    TypeObjectDataGrid.SelectedItem = null;
                }
                if ((sender as Button).Content.ToString() == "Копировать")
                {
                    NAME_TYPE = NameTypeTextBox.Text;
                    TypeObjectDataGrid.SelectedItem = null;
                    NameTypeTextBox.Text = NAME_TYPE;
                    
                }
            }
            else
            {
                ChangeColumn.Width = new GridLength(0);
                SplitterColumn.Width = new GridLength(0); 
            }
        }

        private void DeleteButtonTypeObject(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запись?", "Внимание!", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    // Ссылка на удаляемую книгу
                    Data.TYPE_OBJECTS DeletingTypeObjects = (Data.TYPE_OBJECTS)TypeObjectDataGrid.SelectedItem;
                    // Определение ссылки, на которую должен перейти указатель после удаления
                    if (TypeObjectDataGrid.SelectedIndex < TypeObjectDataGrid.Items.Count - 1)
                    {
                        TypeObjectDataGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (TypeObjectDataGrid.SelectedIndex > 0)
                        {
                            TypeObjectDataGrid.SelectedIndex--;
                        }
                    }
                    Data.TYPE_OBJECTS SelectingTypeObjects = (Data.TYPE_OBJECTS)TypeObjectDataGrid.SelectedItem;
                    TypeObjectDataGrid.SelectedItem = DeletingTypeObjects;
                    SourceCore.DB.TYPE_OBJECTS.Remove(DeletingTypeObjects);
                    SourceCore.DB.SaveChanges();
                    UpdateGrid(SelectingTypeObjects);
                }
                catch
                {
                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
                TypeObjectDataGrid.Focus();
            }
        }

        public void UpdateGrid(Data.TYPE_OBJECTS TYPE_OBJECTS)
        {
            if ((TYPE_OBJECTS == null) && (TypeObjectDataGrid.ItemsSource != null))
            {
                TYPE_OBJECTS = (Data.TYPE_OBJECTS)TypeObjectDataGrid.SelectedItem;
            }
            TypeObjectDataGrid.ItemsSource = SourceCore.DB.TYPE_OBJECTS.ToList();
            TypeObjectDataGrid.SelectedItem = TYPE_OBJECTS;
        }

        private void CommitButtonTypeObject(object sender, RoutedEventArgs e)
        {
            if (NameTypeTextBox.Text != "")
            {
                var A = new Data.TYPE_OBJECTS();
                A.NAME_TYPE = NameTypeTextBox.Text;
                if (TypeObjectDataGrid.SelectedItem == null)
                {
                    SourceCore.DB.TYPE_OBJECTS.Add(A);

                }
                SourceCore.DB.SaveChanges();
                CloseEdChangeClick(sender, e);
                UpdateGrid(A);
                TypeObjectDataGrid.Focus();
            }
            else
            {
                MessageBox.Show("Введите название типа объекта",
                                "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
            }
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
                Columns.Add(TypeObjectDataGrid.Columns[I].Header.ToString());
            }
            TypeObjectsFilterComboBox.ItemsSource = Columns;
            TypeObjectsFilterComboBox.SelectedIndex = 0;
            // Запрет на управление сортировкой щелчком по заголовку столбца
            foreach (DataGridColumn Column in TypeObjectDataGrid.Columns)
            {
                Column.CanUserSort = false;
            }

        }

        private void TypeObjectsFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TypeObjectsFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //BooksViewModel.View.Refresh();
            var textbox = sender as TextBox;
            switch (TypeObjectsFilterComboBox.SelectedIndex)
            {
                case 0:
                    TypeObjectDataGrid.ItemsSource = SourceCore.DB.TYPE_OBJECTS.Where(filtercase => filtercase.NAME_TYPE.Contains(textbox.Text)).ToList();
                break;
            }
        }
    }
}
