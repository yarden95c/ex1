using MazeLib;
using System.Threading;

namespace ClientWpf
{
    class SinglePlayerViewModel : ViewModel
    {
        private ISinglePlayerModel model;
        private static SinglePlayerViewModel instance;
        private static Mutex instanceMutex = new Mutex();
        private SinglePlayerViewModel(ISinglePlayerModel model)
        {
            this.model = model;

        }
        public static SinglePlayerViewModel Instance(ISinglePlayerModel model)
        {
            
                instanceMutex.WaitOne();
                if (instance == null)
                {
                    instance = new SinglePlayerViewModel(model);
                }
                instanceMutex.ReleaseMutex();
                return instance;
            
        }
        public string VM_NameOfMaze
        {
            get { return model.NameOfMaze; }
            set
            {
                model.NameOfMaze = value;
                NotifyPropertyChanged("NameOfMaze");
            }
        }
        public int VM_MazeRows
        {
            get { return model.MazeRows; }
            set
            {
                model.MazeRows = value;
                NotifyPropertyChanged("MazeRows");
            }
        }
        public int VM_MazeColums
        {
            get { return model.MazeColums; }
            set
            {
                model.MazeColums = value;
                NotifyPropertyChanged("MazeColums");
            }
        }
        public string VM_MazeString
        {
            get { return this.model.GenerateMaze(); }
            set
            {
                model.MazeString = value;
                NotifyPropertyChanged("MazeString");
            }
        }
        public Position VM_EndPoint
        {
            get
            {
                return this.model.EndPoint;
            }
            set
            {
                model.EndPoint = value;
                NotifyPropertyChanged("EndPoint");
            }
        }
        public Position VM_StartPoint
        {
            get
            {
                return this.model.StartPoint;
            }
            set
            {
                model.StartPoint = value;
                NotifyPropertyChanged("StartPoint");
            }
        }
        public void VM_GenerateMaze()
        {
            this.VM_MazeString = this.model.GenerateMaze();
        }
        public string VM_SolveMaze()
        {
            return this.model.SolveMaze();
        }
        public void VM_Delete()
        {
            this.model.DeleteSingleGame();
        }
    }
}