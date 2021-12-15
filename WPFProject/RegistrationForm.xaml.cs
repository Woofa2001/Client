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
using System.Windows.Shapes;

namespace WPFProject
{
    /// <summary>
    /// Interaction logic for RegistrationForm.xaml
    /// </summary>
    public partial class RegistrationForm : Window
    {
        private Data.Real_Estate_AgencyEntities Database;
        public RegistrationForm(Data.Real_Estate_AgencyEntities Database)
        {
            InitializeComponent();
            this.Database = Database;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Создание и инициализация нового пользователя системы
            Data.USERS_TABLE User = new Data.USERS_TABLE();
            User.USER_NAME = LoginTextBox.Text;
            User.PASSWORD = PasswordPasswordBox.Password != "" ? PasswordPasswordBox.Password : PasswordTextBox.Text;
            User.ROLE = 0;
            // Добавление его в базу данных
            Database.USERS_TABLE.Add(User);
            // Сохранение изменений
            Database.SaveChanges();
            Close();
        }

        private void PasswordButton_Click(object sender, RoutedEventArgs e)
        {
            String Password = PasswordPasswordBox.Password;
            Visibility Visibility = PasswordPasswordBox.Visibility;
            double Width = PasswordPasswordBox.ActualWidth;
            // Изменение подписи на кнопке
            PasswordButton.Content = Visibility == Visibility.Visible ? "Скрыть" : "Показать";
            // Переброска информации из TextBox'а в PasswordBox
            PasswordPasswordBox.Password = PasswordTextBox.Text;
            PasswordPasswordBox.Visibility = PasswordTextBox.Visibility;
            PasswordPasswordBox.Width = PasswordTextBox.Width;
            // Возврат информации из временных буферов в TextBox
            PasswordTextBox.Text = Password;
            PasswordTextBox.Visibility = Visibility;
            PasswordTextBox.Width = Width;
        }
    }
}
