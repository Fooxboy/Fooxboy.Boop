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
    /// Логика взаимодействия для DialogsView.xaml
    /// </summary>
    public partial class DialogsView : Page
    {
        private DialogsViewModel _vm;
        public DialogsView()
        {
            InitializeComponent();
            _vm = new DialogsViewModel();
            DataContext = _vm;
        }

        private async void DialogsView_OnLoaded(object sender, RoutedEventArgs e)
        {
           await _vm.GetDialogs();
        }
    }
}
