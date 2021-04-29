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

namespace Fooxboy.Boop.Client.WpfApp.Views.LoginsView
{
    /// <summary>
    /// Логика взаимодействия для CheckCodeView.xaml
    /// </summary>
    public partial class CheckCodeView : Page
    {
        long code = 0;
        public CheckCodeView()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Loading.Visibility = Visibility.Visible;
            ContentGrid.Visibility = Visibility.Collapsed;

            code = long.Parse(CodeText.Text);
            var api = Services.ApiService.Get();
            var result = await api.Register.CheckCode(code);

            if(result)
            {
                Services.NavigationService.GetService().GoTo(new RegisterAdminView(code));
            }else
            {
                Loading.Visibility = Visibility.Collapsed;
                ContentGrid.Visibility = Visibility.Visible;
                InvalidCode.Visibility = Visibility.Visible;
            }
        }
    }
}
