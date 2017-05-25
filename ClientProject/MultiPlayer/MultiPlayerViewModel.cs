using MazeLib;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWpf.MultiPlayer
{

     class MultiPlayerViewModel : ViewModel
    {
        private MultiPlayerModel model;
        public MultiPlayerViewModel(MultiPlayerModel model)
        {
            this.model = model;
            this.model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public string VM_NotConnect
        {
            get
            {
                return this.model.NotConnect;
            }
            set
            {
                this.model.NotConnect = value;
               // NotifyPropertyChanged("NotConnect");

            }
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
        public string VM_MazeString
        {
            get { return this.model.MazeString; }
            set
            {
                model.MazeString = value;
                NotifyPropertyChanged("MazeString");
            }
        }
       
        public Position VM_CurrentPointNew
        {
            get
            {
                return this.model.CurrentPointNew;
            }
            set
            {
                this.model.CurrentPointNew = value;
                NotifyPropertyChanged("CurrentPointNew");

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
        public Position VM_StartPoint
        {
            get { return model.StartPoint; }
            set
            {
                model.StartPoint = value;
                NotifyPropertyChanged("StartPoint");
            }
        }
        public Position VM_EndPoint
        {
            get { return model.EndPoint; }
            set
            {
                model.EndPoint = value;
                NotifyPropertyChanged("EndPoint");
            }
        }
        public List<string> VM_listOfGames()
        {
            return this.model.GetList();

        }
        public void VM_start()
        {
            this.VM_MazeString = this.model.Start();
        }
        public void VM_join()
        {
            this.VM_MazeString = this.model.Join();
        }
        public void VM_Play(string move)
        {
            this.model.Play(move);
        }
        public void VM_Close()
        {
            this.model.Close();
        }
        public void VM_CheckIfClose()
        {
            this.model.CheckIfClose();
        }
    }
}
