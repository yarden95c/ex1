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
            this.KeyDown += new System.Windows.Input.KeyEventHandler(mazeControl.Grid_KeyDown);
        }

        private void RestartGame_Click(object sender, RoutedEventArgs e)
        {
            if (this.ShowMessage() == MessageBoxResult.OK)
            {
                this.mazeControl.InitializeCurrPoint(this.mazeControl.StartPoint);
            }
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            if (this.ShowMessage() == MessageBoxResult.OK)
            {
                MainWindow win = new MainWindow();
                win.Show();
                this.Close();
            }

        }

        private void SolveMaze_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MazeBoard_Loaded(object sender, RoutedEventArgs e)
        {
        }
        private MessageBoxResult ShowMessage()
        {
            string messageBoxText = "Are you sure?!?";
            string caption = "Exit to Main menu";
            MessageBoxButton button = MessageBoxButton.OKCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            return System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);

        }
    }
}
