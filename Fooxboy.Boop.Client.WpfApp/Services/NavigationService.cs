﻿using System;
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
        public void GoTo(object page, object data = null)
        {
            _frame.Navigate(page, data);
        }

        private Frame _dialogsFrame;
        private Frame _chatFrame;
        public void GoToChat(string path, object data = null)
        {

            _chatFrame.Navigate(new Uri(path, UriKind.Relative), data);

        }

        public void GoToChat(object page, object data = null)
        {

            _chatFrame.Navigate(page, data);

        }

        public void BackDialogs()
        {
            _frame.GoBack();
        }

        public void GoToDialogsFrame(string path, object data = null)
        {
            _dialogsFrame.Navigate(new Uri(path, UriKind.Relative), data);

        }

        public void GoToDialogsFrame(object page, object data = null)
        {
            _dialogsFrame.Navigate(page, data);

        }

        public void InitChatsFrame(Frame dialogs, Frame chat)
        {
            _dialogsFrame = dialogs;
            _chatFrame = chat;
        }

    }
}
