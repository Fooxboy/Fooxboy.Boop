﻿using System;
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
using Fooxboy.Boop.Client.WpfApp.Views.LoginsView;

namespace Fooxboy.Boop.Client.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
            var config = AppGlobalConfig.ConfigSerivce.GetConfig();
            if(config.ShowWelcomePage) Services.NavigationService.GetService(MainFrame).GoTo("Views/WelcomeView.xaml");
            else Services.NavigationService.GetService(MainFrame).GoTo(new SelectView());

            //todo: проверка есть ли уже авторизация.
            Services.ApiService.Init("https://localhost:2020", "token");
        }
    }
}