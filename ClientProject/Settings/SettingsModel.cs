using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace ClientWpf
{
    class SettingsModel : ISettingsModel
    {
        private string serverIp;
        private int serverPort;
        private int mazeCols;

        private int mazeRows;

        private int searchAlgorithm;

        public SettingsModel()
        {
            serverIp = Properties.Settings.Default.ServerIP;
            serverPort=Properties.Settings.Default.ServerPort;
            mazeCols = Properties.Settings.Default.MazeCols;
            mazeRows = Properties.Settings.Default.MazeRows;
            searchAlgorithm = Properties.Settings.Default.SearchAlgorithm;
        }
        public string ServerIP
        {
            get { return Properties.Settings.Default.ServerIP; }
            set { Properties.Settings.Default.ServerIP = value; }
        }
        public int ServerPort
        {
            get { return Properties.Settings.Default.ServerPort; }
            set { Properties.Settings.Default.ServerPort = value; }
        }
        public int MazeRows
        {
            get { return Properties.Settings.Default.MazeRows; }
            set { Properties.Settings.Default.MazeRows = value; }
        }
        public int MazeCols
        {
            get { return Properties.Settings.Default.MazeCols; }
            set { Properties.Settings.Default.MazeCols = value; }
        }
        public int SearchAlgorithm
        {
            get { return Properties.Settings.Default.SearchAlgorithm; }
            set { Properties.Settings.Default.SearchAlgorithm = value; }
        }
        public void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }
        public void Reset()
        {
            ServerIP = serverIp;
            ServerPort = serverPort;
            MazeCols = mazeCols;
            MazeRows =mazeRows;
            SearchAlgorithm = searchAlgorithm;
        }
    }
}
