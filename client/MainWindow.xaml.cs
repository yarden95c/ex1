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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Window settingWindow;
        public MainWindow()
        {
            InitializeComponent();

            Properties.Settings.Default.Reset();
        }

        private void singlePlayerButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void settingButton_Click(object sender, RoutedEventArgs e)
        {

            settingWindow = new SettingWindow();            settingWindow.ShowDialog();           // this.Close();

        }
    }
}
