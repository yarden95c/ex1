using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientWpf
{
    class Message
    {
        public static MessageBoxResult ShowOKMessage(string message, string title)
        {
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            return MessageBox.Show(message, title, button, icon);

        }
        public static MessageBoxResult ShowOkCancelMessage(string message, string title)
        {
            MessageBoxButton button = MessageBoxButton.OKCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            return MessageBox.Show(message, title, button, icon);

        }
    }
}
