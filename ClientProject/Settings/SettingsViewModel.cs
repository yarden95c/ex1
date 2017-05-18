namespace ClientWpf
{
    class SettingsViewModel : ViewModel
    {
        private ISettingsModel model;
        public SettingsViewModel(ISettingsModel model)
        {
            this.model = model;
        }
        public string VM_ServerIP
        {
            get { return model.ServerIP; }
            set
            {
                model.ServerIP = value;
                NotifyPropertyChanged("VM_ServerIP");
            }
        }
        public int VM_ServerPort
        {
            get { return model.ServerPort; }
            set
            {
                model.ServerPort = value;
                NotifyPropertyChanged("VM_ServerPort");
            }
        }
        public int VM_MazeRows
        {
            get { return model.MazeRows; }
            set
            {
                model.MazeRows = value;
                NotifyPropertyChanged("VM_MazeRows");
            }
        }
        public int VM_SearchAlgorithm
        {
            get { return model.SearchAlgorithm; }
            set
            {
                model.SearchAlgorithm = value;
                NotifyPropertyChanged("VM_SearchAlgorithm");
            }
        }
        public int VM_MazeCols
        {
            get { return model.MazeCols; }
            set
            {
                model.MazeCols = value;
                NotifyPropertyChanged("VM_MazeCols");
            }
        }
        public void VM_SaveSettings()
        {
            model.SaveSettings();
        }
        public void VM_Reset()
        {
            this.model.Reset();
        }
    }
}