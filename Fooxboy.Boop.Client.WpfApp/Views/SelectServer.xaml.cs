﻿using System;
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
    /// Логика взаимодействия для SelectServer.xaml
    /// </summary>
    public partial class SelectServer : Page
    {
        public SelectServer()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Services.ApiService.ChangeAddress(AddressServer.Text+ ":2020");
            Services.NavigationService.GetService().GoTo("Views/LoginsView/SelectView.xaml");

        }
    }
}
