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
        private int count;
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerMazeWindow"/> class.
        /// </summary>
        /// <param name="s">The s.</param>
        public MultiPlayerMazeWindow(string s)
        {
            InitializeComponent();
            vm = new MultiPlayerViewModel(MultiPlayerModel.Instance);
            this.DataContext = vm;
            this.KeyDown += new KeyEventHandler(this.GridKeyDown);
            this.PreviewKeyDown += new KeyEventHandler(this.Grid_PreviewKeyDown);
            count = 0;
            Task task = new Task(() =>
            {
                this.vm.VM_CheckIfClose();
                Message.ShowOKMessage("The other player left", "Multy Player Game" + s);
                this.Dispatcher.Invoke(() =>
                {
                    MainWindow win = new MainWindow();
                    win.Show();
                    this.Close();
                });
            });
            task.Start();
        }
        /// <summary>
        /// Handles the Click event of the mainMenuButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.vm.VM_Close();
            MainWindow win = new MainWindow();
            win.Show();
            this.Close();
        }
        /// <summary>
        /// Grids the key down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Ends the game.
        /// </summary>
        public void EndGame()
        {
            this.vm.VM_Close();
            WinWindow winWindow = new WinWindow();
            winWindow.ShowDialog();
            this.Close();
        }
        /// <summary>
        /// Handles the PreviewKeyDown event of the Grid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
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
    }
}
