using System.Windows;
using System;
namespace ClientWpf
{
    /// <summary>
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private SettingsViewModel vm;
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingWindow"/> class.
        /// </summary>
        public SettingsWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            vm = new SettingsViewModel(new SettingsModel());
            this.DataContext = vm;
        }
        /// <summary>
        /// Handles the Click event of the btnOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            vm.VM_SaveSettings();
            //MainWindow win = (MainWindow)Application.Current.MainWindow;
            MainWindow win = new MainWindow();

            win.Show();
            this.Close();
        }
        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            vm.VM_Reset();
            //  MainWindow win = (MainWindow)Application.Current.MainWindow;
            MainWindow win = new MainWindow();
            win.Show();
            this.Close();
        }

        private void txtRows_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
