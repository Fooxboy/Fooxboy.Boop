using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fooxboy.Boop.Client.WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для CreateUserAccountView.xaml
    /// </summary>
    public partial class CreateUserAccountView : Page
    {
        public CreateUserAccountView()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Loading.Visibility = Visibility.Visible;
            ContentGrid.Visibility = Visibility.Collapsed;

            var api = Services.ApiService.Get();

            try
            {
                var result = await api.Register.User(nickname.Text, "null", email.Text, firstName.Text, lastName.Text, position.Text, password.Text, specc.Text, group.Text);

            }catch(Exception ex)
            {
                Loading.Visibility = Visibility.Collapsed;
                ContentGrid.Visibility = Visibility.Visible;
                MessageBox.Show($"ИСКЛЮЧЕНИЕ: {ex.Message} \n Stack Trace: \n {ex.StackTrace}", $"Произошла ошибка {ex.GetType()}");
            }


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
