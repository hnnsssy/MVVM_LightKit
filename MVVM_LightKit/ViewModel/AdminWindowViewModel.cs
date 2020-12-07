using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MVVM_LightKit.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_LightKit.ViewModel
{
    public class AdminWindowViewModel : ViewModelBase
    {
        ObservableCollection<Excursion> excursionList;
        public ObservableCollection<Excursion> ExcursionList
        {
            get
            {
                if (excursionList == null)
                    excursionList = new ObservableCollection<Excursion>(Repositories.RExcursions.GetAll());
                return excursionList;
            }
            set { excursionList = value; RaisePropertyChanged("ExcursionList"); }
        }

        private int selectedIndex;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; CurrentExcursion = Repositories.RExcursions.FindById(ExcursionList[value].Id); RaisePropertyChanged("SelectedIndex"); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged("Name"); }
        }

        private int duration;
        public int Duration
        {
            get { return duration; }
            set { duration = value; RaisePropertyChanged("Duration"); }
        }

        private int price;
        public int Price
        {
            get { return price; }
            set { price = value; RaisePropertyChanged("Price"); }
        }

        private string exDate;
        public string ExDate
        {
            get { return exDate; }
            set { exDate = value; RaisePropertyChanged("ExDate"); }
        }

        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; RaisePropertyChanged("Type"); }
        }


        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; RaisePropertyChanged("Description"); }
        }


        private Excursion currentExcursion;
        public Excursion CurrentExcursion
        {
            get
            {
                if (currentExcursion == null)
                    currentExcursion = new Excursion();
                return currentExcursion;
            }
            set { currentExcursion = value; RaisePropertyChanged("CurrentExcursion"); }
        }

        RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                if (addCommand == null)
                    addCommand = new RelayCommand(ExecuteAddCommand, CanExecuteAddCommand);
                return addCommand;
            }
        }

        public void ExecuteAddCommand()
        {
            Repositories.RExcursions.Add(new Excursion() { Name = Name, Price = Price, ExDate = ExDate, Type = Type, Duration = Duration, Description = Description });
            //ExcursionList = null;
        }

        public bool CanExecuteAddCommand()
        {
            return !string.IsNullOrEmpty(Name) && Duration > 0 && Price > 0 && !string.IsNullOrEmpty(ExDate) && !string.IsNullOrEmpty(Type) && !string.IsNullOrEmpty(Description);
        }

        RelayCommand changeCommand;
        public RelayCommand ChangeCommand
        {
            get
            {
                if (changeCommand == null)
                    changeCommand = new RelayCommand(ExecuteAddCommand, CanExecuteAddCommand);
                return changeCommand;
            }
        }

        public void ExecuteChangeCommand()
        {
            Repositories.RExcursions.Add(new Excursion() { Name = Name, Price = Price, ExDate = ExDate, Type = Type, Duration = Duration, Description = Description });
            //ExcursionList = null;
        }

        public bool CanExecuteChangeCommand()
        {
            return SelectedIndex >= 0;
        }





        RelayCommand applicationExit;
        public RelayCommand ApplicationExit
        {
            get
            {
                if (applicationExit == null)
                    applicationExit = new RelayCommand(ExecuteAppExitCommand, true);
                return applicationExit;
            }
        }

        public void ExecuteAppExitCommand()
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
