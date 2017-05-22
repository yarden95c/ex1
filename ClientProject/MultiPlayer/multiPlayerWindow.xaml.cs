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
            vm = new MultiPlayerViewModel(SinglePlayerModel.Instance);
            vm.VM_MazeRows = Properties.Settings.Default.MazeRows;
            vm.VM_MazeCols = Properties.Settings.Default.MazeCols;
            this.DataContext = vm;
            comboBox.ItemsSource = games;
            List<string> list = this.vm.VM_listOfGames();
            this.GiveMeTheGames(list);
        }
        private void GiveMeTheGames(List<string> list)
        {
            this.games.Clear();
            foreach (string s in list)
            {
                this.games.Add(s);
            }
        }

        private void comboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.vm.VM_NameOfMaze = (string)comboBox.SelectedItem;
            this.vm.VM_join();
            MultiPlayerMazeWindow win = new MultiPlayerMazeWindow();
            win.Show();
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            WaitingWindow win = new WaitingWindow();
            win.Show();
            win.WaitForAnotherPlayer();
            this.Close();
        }
    }
}
