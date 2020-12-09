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
    class SelectExcursionViewModel : ViewModelBase
    {
        private Guest currentGuest;
        public Guest CurrentGuest
        {
            get
            {
                if (currentGuest == null)
                    currentGuest = new Guest();
                return currentGuest;
            }
            set { currentGuest = value; RaisePropertyChanged("CurrentGuest"); }
        }

        private Client currentClient;
        public Client CurrentClient
        {
            get
            {
                if (currentClient == null)
                    currentClient = new Client();
                return currentClient;
            }
            set { currentClient = value; RaisePropertyChanged("CurrentClient"); }
        }

        RelayCommand _setCurrentUser;
        public RelayCommand SetCurrentUser
        {
            get
            {
                if (_setCurrentUser == null)
                    _setCurrentUser = new RelayCommand(new Action(() => 
                    {
                        if(!string.IsNullOrEmpty((string)System.Windows.Application.Current.Resources["Login"]))
                        {
                            string login = (string)System.Windows.Application.Current.Resources["Login"];
                            string pass = (string)System.Windows.Application.Current.Resources["Password"];
                            CurrentClient = Repositories.RClients.FindAll(x => x.Login == login && x.Password == pass).FirstOrDefault();
                        }
                        else
                        {
                            string sname = (string)System.Windows.Application.Current.Resources["SName"];
                            string pname = (string)System.Windows.Application.Current.Resources["PName"];
                            string phonenumber = (string)System.Windows.Application.Current.Resources["PhoneNumber"];
                            currentGuest = Repositories.RGuests.FindAll(x => x.SName == sname && x.PName == pname && x.PhoneNumber == phonenumber).FirstOrDefault();
                        }
                    }), true);
                return _setCurrentUser;
            }
        }

        ObservableCollection<Excursion> excursionList;
        public ObservableCollection<Excursion> ExcursionList
        {
            get
            {
                if (excursionList == null)
                    excursionList = new ObservableCollection <Excursion>(Repositories.RExcursions.GetAll());
                return excursionList;
            }
            set { excursionList = value; RaisePropertyChanged("ExcursionList"); }
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

        RelayCommand reservationCommand;
        public RelayCommand ReservationCommand
        {
            get
            {
                if (reservationCommand == null)
                    reservationCommand = new RelayCommand(ExecuteReservationCommand, CanExecuteReservationCommand);
                return reservationCommand;
            }
        }

        public void ExecuteReservationCommand()
        {
            Excursion excursion = Repositories.RExcursions.FindById(ExcursionList[SelectedIndex].Id);
            TicketSale ticketSale = Repositories.RTicketSales.FindAll(x => x.Excursion.Id == excursion.Id).FirstOrDefault();
            if(CurrentClient.Login != null)
                Repositories.RTicketSaleClients.Add(new TicketSaleClient() { Client = CurrentClient, TicketSale = ticketSale, TicketAmount = (int)TicketAmount });
            else Repositories.RTicketSaleGuests.Add(new TicketSaleGuest() { Guest = CurrentGuest, TicketSale = ticketSale, TicketAmount = (int)TicketAmount });
        }

        public bool CanExecuteReservationCommand()
        {
            return SelectedIndex >= 0 && TicketAmount > 0;
        }

        private string searchParamType;
        public string SearchParamType
        {
            get { return searchParamType; }
            set { searchParamType = value; RaisePropertyChanged("SearchParamType"); }
        }

        private DateTime searchParamDateTime;
        public DateTime SearchParamDateTime
        {
            get
            {
                if (searchParamDateTime == new DateTime())
                    searchParamDateTime = DateTime.Now.AddDays(5);
                return searchParamDateTime;
            }
            set { searchParamDateTime = value; RaisePropertyChanged("SearchParamDateTime"); }
        }

        private string searchParamFrom;
        public string SearchParamFrom
        {
            get { return searchParamFrom; }
            set { searchParamFrom = value; RaisePropertyChanged("SearchParamFrom"); }
        }

        private string searchParamTo;
        public string SearchParamTo
        {
            get { return searchParamTo; }
            set { searchParamTo = value; RaisePropertyChanged("SearchParamTo"); }
        }

        RelayCommand searchCommand;
        public RelayCommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                    searchCommand = new RelayCommand(ExecuteSearchExcursion, CanExecuteSearchExcursion);
                return searchCommand;
            }
        }

        public void ExecuteSearchExcursion()
        {
            int from;
            int to;
            int.TryParse(searchParamFrom.Replace("от ", ""), out from);
            int.TryParse(SearchParamTo.Replace("до ", ""), out to);
            string excursionDate = searchParamDateTime.Date.ToString("dd.MM.yyyy");

            List<Excursion> tmp_excursions = Repositories.RExcursions.FindAll(x => x.Type == searchParamType && x.ExDate == excursionDate && x.Price >= from && x.Price <= to).ToList();

            ExcursionList.Clear();
            foreach (Excursion item in tmp_excursions)
            {
                if (Repositories.RTicketSales.FindAll(x => x.Excursion.Id == item.Id && x.Amount >= TicketAmount).Count() > 0)
                    ExcursionList.Add(item);
            }
            //List<TicketSale> ticketSales = Repositories.RTicketSales.FindAll(y => y.Excursion.Id == x.Id).FirstOrDefault().Excursion.Id))
            //ExcursionList = new ObservableCollection<Excursion>(Repositories.RExcursions.FindAll(x => x.Type == searchParamType && x.ExDate == excursionDate && x.Price >= from && x.Price <= to)); // && x.Id == Repositories.RTicketSales.FindAll(y=>y.Excursion.Id == x.Id).FirstOrDefault().Excursion.Id))
        }

        public bool CanExecuteSearchExcursion()
        {
            return !string.IsNullOrEmpty(SearchParamType) && !string.IsNullOrEmpty(SearchParamFrom) && !string.IsNullOrEmpty(SearchParamTo) && !string.IsNullOrEmpty(SearchParamType) && TicketAmount > 0; 
        }

        private int? ticketAmount;
        public int? TicketAmount
        {
            get { return ticketAmount; }
            set { ticketAmount = value; RaisePropertyChanged("TicketAmount"); }
        }

        private int selectedIndex;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; RaisePropertyChanged("SelectedIndex"); }
        }
    }
}
