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
using Fooxboy.Boop.Client.WpfApp.ViewModels;

namespace Fooxboy.Boop.Client.WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для UserView.xaml
    /// </summary>
    public partial class UserView : Page
    {
        private long _userId;
        private UserViewModel _vm;
        public UserView(long userId)
        {
            InitializeComponent();
            _userId = userId;
            _vm = new UserViewModel();
            DataContext = _vm;
        }


        private async void UserView_OnLoaded(object sender, RoutedEventArgs e)
        {
            
            await _vm.LoadUserInfo(_userId);
        }
    }
}
