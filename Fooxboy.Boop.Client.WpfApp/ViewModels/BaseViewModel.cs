using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Fooxboy.Boop.Client.WpfApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void Changed(string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
