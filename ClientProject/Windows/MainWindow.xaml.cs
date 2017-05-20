using System;
using System.Configuration;
using System.Windows;
namespace ClientWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // string path = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
        private Window settingWindow;
        private Window singlePlayerWindow;
        private Window multiPlayerWindow;
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            // Properties.Settings.Default.Reset();
        }

        private void singlePlayerButton_Click(object sender, RoutedEventArgs e)
        {
            this.singlePlayerWindow = new SinglePlayerWindow();
            this.singlePlayerWindow.Show();
            this.Close();
        }

        private void settingButton_Click(object sender, RoutedEventArgs e)
        {
            settingWindow = new SettingsWindow();
            settingWindow.Show();
            this.Close();
        }

        private void multiPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            multiPlayerWindow = new MultiPlayer.multiPlayerWindow();
            multiPlayerWindow.Show();
            this.Close();
        }
    }
}
