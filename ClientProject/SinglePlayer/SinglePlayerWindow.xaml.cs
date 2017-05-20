using System.Windows;

namespace ClientWpf
{
    /// <summary>
    /// Interaction logic for SinglePlayerWindow.xaml
    /// </summary>
    public partial class SinglePlayerWindow : Window
    {
        private SinglePlayerViewModel vm;
        public SinglePlayerWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            vm = SinglePlayerViewModel.Instance(SinglePlayerModel.Instance);
            vm.VM_MazeRows = Properties.Settings.Default.MazeRows;
            vm.VM_MazeCols = Properties.Settings.Default.MazeCols;
            this.DataContext = vm;
        }


        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            SinglePlayerMazeWindow win = new SinglePlayerMazeWindow();
            win.Show();
            this.Close();
        }

        private void detailsControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
