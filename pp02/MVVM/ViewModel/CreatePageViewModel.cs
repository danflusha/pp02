using pp02.Core;
using System;
using pp02.MVVM.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace pp02.MVVM.ViewModel
{
    internal class CreatePageViewModel : ObservableObject
    {
        public event EventHandler DisplayPageEventHandler;
        private string titleName;
        private decimal costPredmeta;
        private int timePredmeta;
        private string descriptionPredmeta;
        private double? discountPredmeta;
        private byte[] photoPuth;

        private string viewName;

        public RelayCommand DisplayPageViewCommand { get; set; }
        public RelayCommand AddCommand { get; set; }

        public delegate void ServiceHandler(Service service);
        public event ServiceHandler refreshView;

        public string TitleName
        {
            get { return titleName; }
            set { titleName = value; OnPropertyChanged("TitleName"); } 
        }

        public decimal CostPredmeta
        {
            get { return costPredmeta; }
            set { costPredmeta = value; OnPropertyChanged("CostPredmeta"); }
        }
        public int TimePredmeta
        {
            get { return timePredmeta; }
            set { timePredmeta = value; OnPropertyChanged("TimePredmeta"); }
        }
        public string DescriptionPredmeta
        {
            get { return descriptionPredmeta; }
            set { descriptionPredmeta = value; OnPropertyChanged("DescriptionPredmeta"); }
        }
        public double? DiscountPredmeta
        {
            get { return discountPredmeta; }
            set { discountPredmeta = value; OnPropertyChanged("DiscountPredmeta"); }
        }
        public byte[] PhotoPuth
        {
            get { return photoPuth; }  
            set { photoPuth = value; OnPropertyChanged("PhotoPuth"); }
        }
        public string ViewName
        {
            get { return viewName; }
            set { viewName = value; OnPropertyChanged("ViewName"); }
        }
        public CreatePageViewModel()
        {
            ViewName = "Добавление услуги";

            DisplayPageViewCommand = new RelayCommand(o =>
            {
                DisplayPageEventHandler(this, new EventArgs()); 
            });

            AddCommand = new RelayCommand(o =>
            {
                try
                {
                    Service service1 = TemplateContext.GetContext().Service.Where(ser => ser.Title.Contains(TitleName)).FirstOrDefault();
                    if (service1 == null)
                    {
                        Service service = new Service()
                        {
                            Title = TitleName,
                            Cost = CostPredmeta,
                            DurationInSeconds = TimePredmeta,
                            Description = DescriptionPredmeta,
                            Discount = DiscountPredmeta,
                    
                        };
                        TemplateContext.GetContext().Service.Add(service);
                        TemplateContext.GetContext().SaveChanges();
                        MessageBox.Show("Запись добавлена");
                        refreshView?.Invoke(service);
                    }
                    else
                    {
                        MessageBox.Show("Данная запись уже существует");
                    }
                }
                catch (Exception eee)
                {
                    MessageBox.Show(eee.Message);
                }
            });
        }
        public CreatePageViewModel(Service servicee)
        {
            DisplayPageViewCommand = new RelayCommand(o =>
            {
                DisplayPageEventHandler(this, new EventArgs());
            });

            ViewName = "Редактирование услуг";
            
            TitleName = servicee.Title;
            CostPredmeta = servicee.Cost;
            TimePredmeta = servicee.DurationInSeconds;
            DescriptionPredmeta = servicee.Description;
            DiscountPredmeta = servicee.Discount;
            PhotoPuth = servicee.MainImagePath;

            AddCommand = new RelayCommand(o =>
            {
                try
                {
                    Service service = TemplateContext.GetContext().Service.Find(servicee.ID);
                    service.Title = TitleName;
                    service.Cost = CostPredmeta;
                    service.DurationInSeconds = TimePredmeta;
                    service.Description = DescriptionPredmeta;
                    service.Discount = DiscountPredmeta;
                    service.MainImagePath = PhotoPuth;
                    TemplateContext.GetContext().SaveChanges();
                    MessageBox.Show("Запись изменена");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            });

        }
    }
}
