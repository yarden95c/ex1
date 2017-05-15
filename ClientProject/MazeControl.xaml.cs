using MazeLib;
using System.Windows;
using System.Windows.Controls;
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
     //   private Position currPoint;
        
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
            }
    }
    public string PlayerImageFile
        {
            get;
            set;
        }
        public string Name2
        {
            get;
            set;
        }
        public string ExitImageFile
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
                string[] v = value.Split(',');
                string[] m = v[1].Split(':');
                this.mazeString = m[1];
                this.DrawMaze(this.mazeString);
            }
        }
        public Maze MazeInfo
        {
            set
            {
                this.mazeInfo = value;
                this.StartPoint = this.mazeInfo.InitialPos;
                this.endPoint = this.mazeInfo.GoalPos;
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

        public static readonly DependencyProperty PlayerImageFiles =
           DependencyProperty.Register("PlayerImageFile", typeof(string), typeof(MazeControl),
               new PropertyMetadata(PlayerImageFileChanges));

        public static readonly DependencyProperty ExitImageFiled =
           DependencyProperty.Register("ExitImageFile", typeof(string), typeof(MazeControl),
               new PropertyMetadata(ExitImageFileChanges));

        public static readonly DependencyProperty NameD =
         DependencyProperty.Register("Name", typeof(string), typeof(MazeControl),
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
        private static void PlayerImageFileChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeControl mc = (MazeControl)d;
            mc.PlayerImageFile = (string)e.NewValue;
        }
        private static void ExitImageFileChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeControl mc = (MazeControl)d;
            mc.ExitImageFile = (string)e.NewValue;
        }
        private static void NameChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeControl mc = (MazeControl)d;
            mc.Name = (string)e.NewValue;
        }

        public void DrawMaze(string mazeStringRepresinatation)
        {
            SetRowsOfGrid();
            SetColsOfGrid();
            int k = 0;
            while (mazeStringRepresinatation[k] != '0' && mazeStringRepresinatation[k] != '1')
            {
                k++;
            }
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    Brush white = new SolidColorBrush(Colors.White);
                    Brush black = new SolidColorBrush(Colors.Black);
                    if (mazeStringRepresinatation[k++] == '0')
                    {
                        grid.Children.Add(this.GetRectForGrid(i, j, white)); // its not a wall
                    }
                    else
                    {
                        grid.Children.Add(this.GetRectForGrid(i, j, black)); // its a wall
                    }
                 }
            }
            grid.Children.Add(this.GetRectForGrid(StartPoint.Row, StartPoint.Col, startImage));
            grid.Children.Add(this.GetRectForGrid(EndPoint.Row, EndPoint.Col, endImage));
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
            double hightRect = this.hight / Rows;
            double widthRect = this.width / Cols;
            Rectangle rect = new Rectangle();
            rect.Height = hightRect;
            rect.Width = widthRect;
            Grid.SetRow(rect, i);
            Grid.SetColumn(rect, j);
            rect.Fill = fill;
            return rect;
        }
    }
    
}