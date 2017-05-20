using MazeLib;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ClientWpf
{
    /// <summary>
    /// Interaction logic for MazeControl.xaml
    /// </summary>
    public partial class MazeControl : UserControl
    {
        private Grid grid;
        private double hight;
        private double width;
        private string mazeString;
        private Maze mazeInfo;
        private Position startPoint;
        private Position endPoint;
        private Position currPoint;


        public MazeControl()
        {
            InitializeComponent();
            grid = new Grid();
            width = myCanvas.Width;
            hight = myCanvas.Height;
            Content = grid; // the content is grid.
            grid.ShowGridLines = true;
        }
        public Position StartPoint
        {
            get
            {
                return this.startPoint;
            }
            set
            {
                this.startPoint = value;
            }
        }
        public Position EndPoint
        {
            get
            {
                return this.endPoint;
            }
            set
            {
                this.endPoint = value;
                this.grid.Children.Add(this.GetRectForGrid(EndPoint.Row, EndPoint.Col, endImage));
            }
        }
        public Position CurrPoint
        {
            get
            {
                return this.currPoint;
            }
            set
            {
                this.currPoint = value;
                this.Dispatcher.Invoke(() =>
                {
                    this.grid.Children.Add(this.GetRectForGrid(CurrPoint.Row, CurrPoint.Col, startImage));
                });
            }
        }
        public string NameOfMaze
        {
            get;
            set;
        }
        public int Cols
        {
            get;
            set;
        }
        public int Rows
        {
            get;
            set;
        }
        public string MazeString
        {
            get
            {
                return this.mazeString;
            }
            set
            {
                this.MazeInfo = Maze.FromJSON(value);
                this.mazeString = this.mazeInfo.Name;
                this.DrawMaze();
            }
        }
        public Maze MazeInfo
        {
            set
            {
                this.mazeInfo = value;
                this.StartPoint = this.mazeInfo.InitialPos;
                this.EndPoint = this.mazeInfo.GoalPos;
                this.CurrPoint = this.StartPoint;
            }
        }

        // Using a DependencyProperty as the backing store for StringOfMaze.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeStringD =
            DependencyProperty.Register("MazeString", typeof(string), typeof(MazeControl),
                new PropertyMetadata(MazeChanges));

        public static readonly DependencyProperty ColsD =
            DependencyProperty.Register("Cols", typeof(int), typeof(MazeControl),
                new PropertyMetadata(ColsChanges));

        public static readonly DependencyProperty RowsD =
            DependencyProperty.Register("Rows", typeof(int), typeof(MazeControl),
                new PropertyMetadata(RowsChanges));

        public static readonly DependencyProperty StartPointD =
           DependencyProperty.Register("StartPoint", typeof(Position), typeof(MazeControl),
               new PropertyMetadata(StartPointChanges));

        public static readonly DependencyProperty EndPointD =
           DependencyProperty.Register("EndPoint", typeof(Position), typeof(MazeControl),
               new PropertyMetadata(EndPointChanges));

        public static readonly DependencyProperty NameD =
         DependencyProperty.Register("NameOfMaze", typeof(string), typeof(MazeControl),
             new PropertyMetadata(NameChanges));

        private static void MazeChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeControl mc = (MazeControl)d;
            mc.MazeString = (string)e.NewValue;
        }

        private static void ColsChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeControl mc = (MazeControl)d;
            mc.Cols = (int)e.NewValue;
        }
        private static void RowsChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeControl mc = (MazeControl)d;
            mc.Rows = (int)e.NewValue;
        }
        private static void StartPointChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeControl mc = (MazeControl)d;
            mc.StartPoint = (Position)e.NewValue;
        }
        private static void EndPointChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeControl mc = (MazeControl)d;
            mc.EndPoint = (Position)e.NewValue;
        }

        private static void NameChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeControl mc = (MazeControl)d;
            mc.NameOfMaze = (string)e.NewValue;
        }

        public void DrawMaze()
        {
            SetRowsOfGrid();
            SetColsOfGrid();
            Brush black = new SolidColorBrush(Colors.Black);

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (this.mazeInfo[i, j] == CellType.Wall)
                    {
                        grid.Children.Add(this.GetRectForGrid(i, j, black)); // its a wall
                    }
                }
            }
        }

        private void SetRowsOfGrid()
        {
            for (int i = 0; i < Rows; i++)
            {
                RowDefinition row = new RowDefinition();
                grid.RowDefinitions.Add(row);
            }
        }

        private void SetColsOfGrid()
        {
            for (int i = 0; i < Cols; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                grid.ColumnDefinitions.Add(col);
            }
        }
        public Rectangle GetRectForGrid(int i, int j, Brush fill)
        {
            Rectangle rect = new Rectangle();
            rect.Height = this.hight / Rows;
            rect.Width = this.width / Cols; 
            Grid.SetRow(rect, i);
            Grid.SetColumn(rect, j);
            rect.Fill = fill;
            return rect;
        }

        public void SetCurrPoint(Position newPoint)
        {
            int row = newPoint.Row;
            int col = newPoint.Col;

            if (row >= 0 && row < Rows && col >= 0 && col < Cols)
            {
                int i = CurrPoint.Row, j = CurrPoint.Col;
                if (this.mazeInfo[row, col] == CellType.Free)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        grid.Children.Add(this.GetRectForGrid(i, j, new SolidColorBrush(Colors.White))); // its not a wall
                    });
                    CurrPoint = newPoint;
                }
            }
        }
        public bool AreEqualPositions(Position p1, Position p2)
        {
            return (p1.Row == p2.Row) && (p1.Col == p2.Col);
        }
    }

}