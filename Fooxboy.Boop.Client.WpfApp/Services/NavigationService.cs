using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Fooxboy.Boop.Client.WpfApp.Services
{
    public class NavigationService
    {
        private static NavigationService _service;

        private Frame _frame;
        private NavigationService(Frame frame)
        {
            _frame = frame;
        }

        public static NavigationService GetService(Frame frame = null)
        {
            return _service ??= new NavigationService(frame);
        }


        public void GoTo(string path, object data = null)
        {
            _frame.Navigate(new Uri(path, UriKind.Relative), data);
        }

    }
}
