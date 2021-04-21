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
using Fooxboy.Boop.Client.WpfApp.Helpers;
using Fooxboy.Boop.Client.WpfApp.Services;
using Fooxboy.Boop.Client.WpfApp.Views;
using Fooxboy.Boop.SDK.Exceptions;
using Fooxboy.Boop.Shared.Models;
using Fooxboy.Boop.Shared.Models.Messages;

namespace Fooxboy.Boop.Client.WpfApp.Controls
{
    /// <summary>
    /// Логика взаимодействия для MessageControl.xaml
    /// </summary>
    public partial class MessageControl : UserControl
    {
        public MessageControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(Message), typeof(MessageControl));

        public Message Message
        {
            get => (Message) GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        private Attach _attach;

        private async void MessageControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextMessage.Text = Message.Text;
            Time.Text = TimeSpan.FromSeconds(Message.Time).ToString(@"hh\:mm");
            NameUser.Text = Message.NameSender;

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.DecodePixelHeight = 50;
            bitmap.DecodePixelWidth = 50;
            bitmap.UriSource = new Uri(await ImageHelper.GetImage(Message.ImageSender));

            bitmap.EndInit();

            photoUser.ImageSource = bitmap;

            if(Message.Attach != 0)
            {
                var api = ApiService.Get();
               
                var attach = await api.Messages.GetAttachmentAsync(Message.Attach);
                if(attach.File is null)
                {
                    FileName.Text = "NULL";
                }else
                {
                    _attach = attach;
                    if (attach.TypeAttach == 2)
                    {
                        AttachFile.Visibility = Visibility.Visible;
                        var fileName = attach.File.Split("/")[^1].Split("--")[1];
                        FileName.Text = fileName;
                    }
                    else if (attach.TypeAttach == 1)
                    {
                        AttachImage.Visibility = Visibility.Visible;
                        var bitmap1 = new BitmapImage();
                        bitmap1.BeginInit();
                        //bitmap1.DecodePixelHeight = 400;
                        //bitmap1.DecodePixelWidth = 300;
                        var fileName = attach.File.Split("/")[^1].Split("--")[1];
                        var file = await ImageHelper.GetImage(attach.File, fileName);
                        bitmap1.UriSource = new Uri(file);
                        bitmap1.EndInit();

                        if(bitmap1.PixelWidth > bitmap1.PixelHeight)
                        {
                            RectImg.Width = 400;
                            RectImg.Height = 300;
                        }else if(bitmap1.PixelHeight > bitmap1.PixelWidth)
                        {
                            RectImg.Width = 300;
                            RectImg.Height = 400;
                        }else
                        {
                            RectImg.Width = 400;
                            RectImg.Height = 400;
                        }

                        ImageAttach.ImageSource = bitmap1;

                    }
                } 
            }
        }


        private void UIElement_OnMouseEnter(object sender, MouseEventArgs e)
        {
            ShadowPhoto.Opacity = 0.4;
        }

        private void UIElement_OnMouseLeave(object sender, MouseEventArgs e)
        {
            ShadowPhoto.Opacity = 0.0;
        }

        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var api = Services.ApiService.Get();
            try
            {
                var idUser = Message.SenderId;
                Services.NavigationService.GetService().GoToChat(new UserView(idUser), idUser);
            }
            catch (BoopRootException ee)
            {
                MessageBox.Show($"Код ошибки: {ee.Code}. Сообщение: {ee.Message}");
            }
            catch (Exception ee)
            {
                MessageBox.Show($"{ee.Message}", "Ошибка");

            }
        }
    }
}
