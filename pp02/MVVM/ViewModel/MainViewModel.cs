using pp02.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pp02.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public DisplayViewModel DisplayVM { get; set; }
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged();}
        }
        public MainViewModel()
        {
            DisplayVM = new DisplayViewModel();
            CurrentView = DisplayVM;

            DisplayVM.CreatePageEventHandler += DisplayVM_CreatePageEventHandler;
            DisplayVM.CreatePageVM.DisplayPageEventHandler += CreatePageVM_DisplayPageEventHandler;
        }

        private void CreatePageVM_DisplayPageEventHandler(object sender, EventArgs e)
        {
            CurrentView = DisplayVM;
        }
        private void DisplayVM_CreatePageEventHandler(object sender, EventArgs e)
        {
            CurrentView = DisplayVM.CreatePageVM;
        }

        
    }
}
