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
            try
            {
                this.singlePlayerWindow.Show();
                this.Close();

            }
            catch (Exception)
            {
                this.ShowMessage("we are sorry, there is a problem with the connection", "ERROE");
            }
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
            try
            {
                multiPlayerWindow.Show();
                this.Close();

            }
            catch (Exception)
            {
                this.ShowMessage("we are sorry, there is a problem with the connection", "ERROE");
            }
        }
        public MessageBoxResult ShowMessage(string message, string title)
        {
            MessageBoxButton button = MessageBoxButton.OKCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            return MessageBox.Show(message, title, button, icon);

        }
        public static readonly DependencyProperty NotConnectD =
          DependencyProperty.Register("NotConnect", typeof(string), typeof(MainWindow),
              new PropertyMetadata(ConnectChanges));
        private static void ConnectChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainWindow m = (MainWindow)d;
            m.ShowMessage("we are sorry, there is a problem with the connection", "ERROE");
        }
    }
}
