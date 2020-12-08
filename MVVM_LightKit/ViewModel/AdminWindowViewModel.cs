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



        private int? selectedIndex;
        public int? SelectedIndex
        {
            get { return selectedIndex; }
            set { 
                selectedIndex = value; 
                CurrentExcursion = Repositories.RExcursions.FindById(ExcursionList[(int)value].Id);
                CurrentTicket = Repositories.RTicketSales.FindAll(x => x.Excursion.Id == CurrentExcursion.Id).FirstOrDefault();
                RaisePropertyChanged("SelectedIndex"); 
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged("Name"); }
        }

        private int? duration;
        public int? Duration
        {
            get { return duration; }
            set { duration = value; RaisePropertyChanged("Duration"); }
        }

        private int? price;
        public int? Price
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

        private int? ticketAmount;
        public int? TicketAmount
        {
            get { return ticketAmount; }
            set { ticketAmount = value; RaisePropertyChanged("TicketAmount"); }
        }

        private int? ticketPrice;
        public int? TicketPrice
        {
            get { return ticketPrice; }
            set { ticketPrice = value; RaisePropertyChanged("TicketPrice"); }
        }

        private string ticketNotes;
        public string TicketNotes
        {
            get { return ticketNotes; }
            set { ticketNotes = value; RaisePropertyChanged("TicketNotes"); }
        }

        private Excursion currentExcursion;
        public Excursion CurrentExcursion
        {
            get { return currentExcursion; }
            set { currentExcursion = value; RaisePropertyChanged("CurrentExcursion"); }
        }

        private TicketSale currentTicket;
        public TicketSale CurrentTicket
        {
            get { return currentTicket; }
            set { currentTicket = value; RaisePropertyChanged("CurrentExcursion"); }
        }

        private string buttonAddFunction = "Добавить";
        public string ButtonAddFunction
        {
            get { return buttonAddFunction; }
            set { buttonAddFunction = value; RaisePropertyChanged("ButtonAddFunction"); }
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
            if (buttonAddFunction == "Добавить")
            {
                Excursion exc = new Excursion() { Name = Name, Price = (int)Price, ExDate = ExDate, Type = Type, Duration = (int)Duration, Description = Description };
                Repositories.RExcursions.Add(exc);
                Repositories.RTicketSales.Add(new TicketSale() { Excursion = exc, Price = (int)TicketPrice, Amount = (int)TicketAmount, Notes = TicketNotes });
                ClearFields();
            }
            else
            {
                CurrentExcursion.Name = Name;
                CurrentExcursion.Price = (int)Price;
                CurrentExcursion.ExDate = ExDate;
                CurrentExcursion.Type = Type;
                CurrentExcursion.Duration = (int)Duration;
                CurrentExcursion.Description = Description;
                Repositories.RExcursions.Update(CurrentExcursion);

                CurrentTicket.Amount = (int)TicketAmount;
                CurrentTicket.Price = (int)TicketPrice;
                CurrentTicket.Notes = TicketNotes;
                Repositories.RTicketSales.Update(CurrentTicket);

                ClearFields();
                ButtonAddFunction = "Добавить";
            }
        }

        public void ClearFields()
        {
            ExcursionList = null;
            Name = "";
            Type = "";
            ExDate = "";
            Description = "";
            Duration = null;
            Price = null;
            TicketAmount = null;
            TicketPrice = null;
            TicketNotes = "";
        }

        public bool CanExecuteAddCommand()
        {
            return !string.IsNullOrEmpty(Name) && Duration > 0 && Price > 0 && !string.IsNullOrEmpty(ExDate) && !string.IsNullOrEmpty(Type) && !string.IsNullOrEmpty(Description) && TicketAmount > 0;
        }

        RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                    deleteCommand = new RelayCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
                return deleteCommand;
            }
        }

        public void ExecuteDeleteCommand()
        {
            Repositories.RExcursions.Remove(ExcursionList[(int)SelectedIndex]);
            Repositories.RTicketSales.Remove(CurrentTicket);
            ExcursionList = null;
        }

        public bool CanExecuteDeleteCommand()
        {
            return SelectedIndex >= 0;
        }

        RelayCommand changeCommand;
        public RelayCommand ChangeCommand
        {
            get
            {
                if (changeCommand == null)
                    changeCommand = new RelayCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
                return changeCommand;
            }
        }

        public void ExecuteChangeCommand()
        {
            Name = CurrentExcursion.Name;
            Description = CurrentExcursion.Description;
            Duration = CurrentExcursion.Duration;
            Price = CurrentExcursion.Price;
            ExDate = CurrentExcursion.ExDate;
            Type = CurrentExcursion.Type;
            Description = CurrentExcursion.Description;

            TicketAmount = CurrentTicket.Amount;
            TicketPrice = CurrentTicket.Price;
            TicketNotes = CurrentTicket.Notes;
            ButtonAddFunction = "Cохранить";
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
