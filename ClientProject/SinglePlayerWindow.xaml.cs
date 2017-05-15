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
            vm = new SinglePlayerViewModel(new SinglePlayerModel());
            this.DataContext = vm;
        }


        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            SinglePlayerMazeWindow win = new SinglePlayerMazeWindow();
            win.Show();
            this.Close();
        }
    }
}
