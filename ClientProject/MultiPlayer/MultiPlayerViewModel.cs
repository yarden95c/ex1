using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWpf.MultiPlayer
{

    public class MultiPlayerViewModel : ViewModel
    {
        private ISinglePlayerModel model;
        public MultiPlayerViewModel(ISinglePlayerModel model)
        {
            this.model = model;
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
        public int VM_MazeCols
        {
            get { return model.MazeCols; }
            set
            {
                model.MazeCols = value;
                NotifyPropertyChanged("MazeCols");
            }
        }
        public List<string> VM_listOfGames()
        {
            return this.model.GetList();

        }
        public void VM_start()
        {
            this.model.Start();
        }
        public void VM_join()
        {
            this.model.Join();
        }
    }
}
