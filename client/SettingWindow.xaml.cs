using System;
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
using System.Windows.Shapes;

namespace client
{
    /// <summary>
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        private SettingsViewModel vm;
        public SettingWindow()
        {
            InitializeComponent();
            vm = new SettingsViewModel();
            this.DataContext = vm;
        }
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            vm.SaveSettings();
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            vm.CancelSettings();
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }
        
    }
}
