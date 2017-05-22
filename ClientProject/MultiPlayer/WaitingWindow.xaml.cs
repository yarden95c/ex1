using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClientWpf.MultiPlayer
{
    /// <summary>
    /// Interaction logic for WaitingWindow.xaml
    /// </summary>
    public partial class WaitingWindow : Window
    {
        private MultiPlayerViewModel vm;
        public WaitingWindow()
        {
            InitializeComponent();
            vm = new MultiPlayerViewModel(SinglePlayerModel.Instance);
            this.DataContext = vm;
        }
        public void WaitForAnotherPlayer()
        {
            this.vm.VM_start(); 
            MultiPlayerMazeWindow win = new MultiPlayerMazeWindow();
            win.Show();
            this.Close();
        }
    }
}
