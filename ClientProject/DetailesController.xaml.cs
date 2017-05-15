﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientWpf
{
    /// <summary>
    /// Interaction logic for DetailesComtroller.xaml
    /// </summary>
    public partial class DetailesController : UserControl
    {
        public DetailesController()
        {
            InitializeComponent();
            txtRows.Text = Properties.Settings.Default.MazeRows.ToString();
            txtCols.Text = Properties.Settings.Default.MazeCols.ToString();
        }
    }
}
