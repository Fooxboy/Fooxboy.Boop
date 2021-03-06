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
    /// Логика взаимодействия для NoSelectChat.xaml
    /// </summary>
    public partial class NoSelectChat : Page
    {
        public NoSelectChat()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Открытие окна с созданием чата
            Services.NavigationService.GetService().GoToChat(new CreateChatView());
        }
    }
}
