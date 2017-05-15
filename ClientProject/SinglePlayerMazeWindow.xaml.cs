using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Shapes;

namespace ClientWpf
{
    /// <summary>
    /// Interaction logic for SinglePlayerMazeWindow.xaml
    /// </summary>
    public partial class SinglePlayerMazeWindow : Window
    {
        private SinglePlayerViewModel vm;
        public SinglePlayerMazeWindow()
        {
            InitializeComponent();
            this.vm = new SinglePlayerViewModel(new SinglePlayerModel());
            this.DataContext = vm;
        }

        private void RestartGame_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = new MainWindow();
            win.Show();
            this.Close();
        }

        private void SolveMaze_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MazeBoard_Loaded(object sender, RoutedEventArgs e)
        {

        }
        
    }
}
