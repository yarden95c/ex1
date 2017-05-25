using Server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientWpf.MultiPlayer
{
    /// <summary>
    /// Interaction logic for multiPlayerWindow.xaml
    /// </summary>
    public partial class multiPlayerWindow : Window
    {
        private MultiPlayerViewModel vm;
        private ObservableCollection<string> games = new ObservableCollection<string>();
        public multiPlayerWindow()
        {
            InitializeComponent();
            vm = new MultiPlayerViewModel(MultiPlayerModel.Instance);
            vm.VM_MazeRows = Properties.Settings.Default.MazeRows;
            vm.VM_MazeCols = Properties.Settings.Default.MazeCols;
            this.DataContext = vm;
            comboBox.ItemsSource = games;
            this.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.comboBox_DropDownOpened);
        }
        private void GiveMeTheGames(List<string> list)
        {
            this.games.Clear();
            if (list != null)
            {
                foreach (string s in list)
                {
                    this.games.Add(s);
                }
            }
        }
        private void JoinGameButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.vm.VM_NameOfMaze = (string)comboBox.SelectedItem;
                this.vm.VM_join();
                MultiPlayerMazeWindow win = new MultiPlayerMazeWindow(" Join");
                win.Show();
                this.Close();
            }
            catch (Exception)
            {
                Message.ShowOKMessage("we are sorry, there is a problem with the connection, please try again later..", "ERROE");
            }
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WaitingWindow win = new WaitingWindow();
                win.Show();
                win.WaitForAnotherPlayer();
                this.Close();
            }
            catch (Exception)
            {
                Message.ShowOKMessage("we are sorry, there is a problem with the connection, please try again later..", "ERROE");
            }
        }
        private void comboBox_DropDownOpened(object sender, EventArgs e)
        {
            List<string> list = this.vm.VM_listOfGames();
            this.GiveMeTheGames(list);
        }


    }
}
