using pp02.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using pp02.MVVM.Model;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace pp02.MVVM.ViewModel
{
    internal class DisplayViewModel : ObservableObject
    {
        private string searchField;
        private ObservableCollection<Service> services;
        public ObservableCollection<Service> Service
        {
            get { return services; }
            set { services = value; OnPropertyChanged("Service"); }
        }

        public string SearchField
        {
            get { return searchField; }
            set { searchField = value; OnPropertyChanged("SearchField");}
        }
        public CreatePageViewModel CreatePageVM { get; set; }
        public RelayCommand CreatePageCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public event EventHandler CreatePageEventHandler;
        public DisplayViewModel()
        {
            Service = new ObservableCollection<Service>(TemplateContext.GetContext().Service.ToList());

            CreatePageVM = new CreatePageViewModel();

            CreatePageCommand = new RelayCommand(o =>
            {
                CreatePageVM.refreshView += CreatePageVM_refreshView;
                CreatePageEventHandler(this, new EventArgs());
            });

            EditCommand = new RelayCommand(o =>
            {
                CreatePageVM = new CreatePageViewModel(o as Service);
                CreatePageEventHandler(this, new EventArgs());
            });

            DeleteCommand = new RelayCommand(o =>
            {
                Service service = o as Service;
                MessageBoxResult result = MessageBox.Show("Вы уверены?!","Уверен?", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Service.Remove(service);
                    TemplateContext.GetContext().Service.Remove(service);
                    TemplateContext.GetContext().SaveChanges();
                }
            });

            SearchCommand = new RelayCommand(o =>
            {
                Service = new ObservableCollection<Service>(TemplateContext.GetContext().Service.Where(Service => Service.Title.Contains(SearchField)).ToList());
            });
        }

        private void CreatePageVM_refreshView(Service service)
        {
            Service.Add(service);
        }
    }
}
