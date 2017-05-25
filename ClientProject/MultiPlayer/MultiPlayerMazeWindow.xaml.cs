using MazeLib;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace ClientWpf.MultiPlayer
{
    /// <summary>
    /// Interaction logic for MultiPlayerMazeWindow.xaml
    /// </summary>
    public partial class MultiPlayerMazeWindow : Window
    {
        private MultiPlayerViewModel vm;
        private bool isClosed;
        private int count;
        public MultiPlayerMazeWindow(string s)
        {
            InitializeComponent();
            vm = new MultiPlayerViewModel(MultiPlayerModel.Instance);
            this.DataContext = vm;
            this.KeyDown += new KeyEventHandler(this.GridKeyDown);
            this.PreviewKeyDown += new KeyEventHandler(this.Grid_PreviewKeyDown);
            count = 0;
            isClosed = false;
            Task task = new Task(() =>
            {
                this.vm.VM_CheckIfClose();
                if (!this.isClosed)
                {
                    Message.ShowOKMessage("The other player left", "Multy Player Game" + s);
                    this.Dispatcher.Invoke(() =>
                    {
                        //MainWindow win = new MainWindow();
                        //win.Show();
                        this.Close();
                    });
                }
            });
            task.Start();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            this.isClosed = true;
            this.vm.VM_Close();
            MainWindow win = new MainWindow();
            win.Show();
        }
        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            //this.vm.VM_Close();
            //MainWindow win = new MainWindow();
            //win.Show();
            this.Close();
        }
        public void GridKeyDown(object sender, KeyEventArgs e)
        {
            int row = this.mazeControlMy.CurrPoint.Row, col = this.mazeControlMy.CurrPoint.Col;
            Position newPosition = new Position();
            string move = "its not a key";
            switch (e.Key)
            {
                case Key.Down:
                    row = row + 1;
                    move = "down";
                    break;
                case Key.Up:
                    row = row - 1;
                    move = "up";
                    break;
                case Key.Left:
                    col = col - 1;
                    move = "left";
                    break;
                case Key.Right:
                    col = col + 1;
                    move = "right";
                    break;
                default:
                    break;
            }
            newPosition.Row = row;
            newPosition.Col = col;

            if (this.mazeControlMy.AreEqualPositions(newPosition, this.mazeControlMy.EndPoint))
            {
                this.EndGame();
                return;
            }
            this.mazeControlMy.SetCurrPoint(newPosition);
            if (mazeControlMy.CurrPoint.Equals(newPosition))
            {
                if (!move.Equals("its not a key"))
                    vm.VM_Play(move);
            }
        }

        public void EndGame()
        {
            this.vm.VM_Close();
            WinWindow winWindow = new WinWindow();
            winWindow.ShowDialog();
            this.Close();
        }
        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.IsRepeat)
            {
                count++;
                if (count >= 5)
                {
                    count = 0;
                }
            }
            else
            {
                count = 0;

            }
            e.Handled = true;
            GridKeyDown(sender, e);
        }
        //private MessageBoxResult ShowMessage(string message, string title)
        //{
        //    MessageBoxButton button = MessageBoxButton.OK;
        //    MessageBoxImage icon = MessageBoxImage.Warning;
        //    return MessageBox.Show(message, title, button, icon);
        //}
    }
}
