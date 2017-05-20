using MazeLib;
using Newtonsoft.Json.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using System;
using System.Threading;
using System.Threading.Tasks;

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
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.vm = SinglePlayerViewModel.Instance(SinglePlayerModel.Instance);
            this.DataContext = vm;
            this.KeyDown += new KeyEventHandler(this.GridKeyDown);
        }

        public void GridKeyDown(object sender, KeyEventArgs e)
        {
            int row = this.mazeControl.CurrPoint.Row, col = this.mazeControl.CurrPoint.Col;
            Position newPosition = new Position();

            switch (e.Key)
            {
                case Key.Down:
                    row = row + 1;
                    break;
                case Key.Up:
                    row = row - 1;
                    break;
                case Key.Left:
                    col = col - 1;
                    break;
                case Key.Right:
                    col = col + 1;
                    break;
                default:
                    break;
            }
            newPosition.Row = row;
            newPosition.Col = col;

            if (this.mazeControl.AreEqualPositions(newPosition, this.mazeControl.EndPoint))
            {
                this.EndGame();
                return;
            }
            this.mazeControl.SetCurrPoint(newPosition);
        }
        public void EndGame()
        {
            WinWindow winWindow = new WinWindow();
            winWindow.ShowDialog();
            MainWindow win = new MainWindow();
            win.Show();
            this.Close();
            vm.VM_Delete();
        }
        private void RestartGame_Click(object sender, RoutedEventArgs e)
        {
            if (this.ShowMessage("Are you sure?!", "Restart Game") == MessageBoxResult.OK)
            {
                this.mazeControl.SetCurrPoint(this.mazeControl.StartPoint);
            }
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            if (this.ShowMessage("Are you sure?!?", "Exit to Main menu") == MessageBoxResult.OK)
            {
                //  MainWindow win = (MainWindow)Application.Current.MainWindow;
                MainWindow win = new MainWindow();
                win.Show();
                this.Close();
                vm.VM_Delete();
            }

        }

        private void SolveMaze_Click(object sender, RoutedEventArgs e)
        {
            this.mazeControl.SetCurrPoint(this.mazeControl.StartPoint);
            string solution = this.vm.VM_SolveMaze();
            JObject jObject = JObject.Parse(solution);
            JToken jSolution = jObject["Solution"];
            solution = (string)jSolution;
            Task task = new Task(() =>
            {
                this.StartSolve(solution);
            });
            task.Start();
           // this.EndGame();
        }
        private MessageBoxResult ShowMessage(string message, string title)
        {
            MessageBoxButton button = MessageBoxButton.OKCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            return MessageBox.Show(message, title, button, icon);

        }
        public void StartSolve(string solution)
        {
            for (int i = 0; i < solution.Length; i++)
            {
                int direction = int.Parse(solution[i].ToString());
                Position newPosition = this.GetNewPoint(direction);
                if (this.mazeControl.AreEqualPositions(newPosition, this.mazeControl.EndPoint))
                {
                    return;
                }
                Thread.Sleep(500);
                this.mazeControl.SetCurrPoint(newPosition);
             
            }
        }


        public Position GetNewPoint(int direction)
        {
            int row = this.mazeControl.CurrPoint.Row, col = this.mazeControl.CurrPoint.Col;
            Position newPosition = new Position();
            newPosition.Row = row;
            newPosition.Col = col;

            switch (direction)
            {
                case 3:
                    newPosition.Row = row + 1;
                    break;
                case 2:
                    newPosition.Row = row - 1;
                    break;
                case 0:
                    newPosition.Col = col - 1;
                    break;
                case 1:
                    newPosition.Col = col + 1;
                    break;
                default:
                    break;
            }
            return newPosition;
        }

    }

}
