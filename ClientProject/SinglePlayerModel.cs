
using MazeLib;

namespace ClientWpf
{
    class SinglePlayerModel : ISinglePlayerModel
    {
        Client client = null;
        private Position endPoint;
        private Position startPoint;
        public SinglePlayerModel()
        {
            this.client = new Client();
        }
        public string NameOfMaze
        {
            get { return Properties.Settings.Default.NameOfMaze; }
            set { Properties.Settings.Default.NameOfMaze = value; }
        }
        public int MazeRows
        {
            get { return Properties.Settings.Default.MazeRowsSinglePlayer; }
            set { Properties.Settings.Default.MazeRowsSinglePlayer = value; }
        }
        public int MazeColums
        {
            get { return Properties.Settings.Default.MazeColsSinglePlayer; }
            set { Properties.Settings.Default.MazeColsSinglePlayer = value; }
        }
        public string MazeString
        {
            get { return Properties.Settings.Default.MazeString; }
            set { Properties.Settings.Default.MazeString = value; }
        }
        public Position EndPoint
        {
            get { return this.endPoint ; }
            set { this.endPoint = value; }
        }
        public Position StartPoint
        {
            get { return this.startPoint; }
            set { this.startPoint = value; }
        }
        public string GenerateMaze()
        {
            string command = "generate" + " " + this.NameOfMaze + " "
                + this.MazeRows + " " + this.MazeColums;
            this.client.AddCommand(command);
            this.client.Connect();
            string maze = this.client.GetAnswer();
            this.StartPoint =Maze.FromJSON(maze).InitialPos;
            this.EndPoint = Maze.FromJSON(maze).GoalPos;
            return maze;
        }
        
    }
}
