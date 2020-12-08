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


        ObservableCollection<Client> clientsList;
        public ObservableCollection<Client> ClientsList
        {
            get
            {
                if (clientsList == null)
                    clientsList = new ObservableCollection<Client>(Repositories.RClients.GetAll());
                return clientsList;
            }
            set { clientsList = value; RaisePropertyChanged("ClientsList"); }
        }

        ObservableCollection<TicketSaleClient> ticketSaleClients;
        public ObservableCollection<TicketSaleClient> TicketSaleClients
        {
            get
            {
                if (ticketSaleClients == null)
                    ticketSaleClients = new ObservableCollection<TicketSaleClient>(Repositories.RTicketSaleClients.GetAll());
                return ticketSaleClients;
            }
            set { ticketSaleClients = value; RaisePropertyChanged("TicketSaleClients"); }
        }

        ObservableCollection<TicketSaleGuest> ticketSaleGuests;
        public ObservableCollection<TicketSaleGuest> TicketSaleGuests
        {
            get
            {
                if (ticketSaleGuests == null)
                    ticketSaleGuests = new ObservableCollection<TicketSaleGuest>(Repositories.RTicketSaleGuests.GetAll());
                return ticketSaleGuests;
            }
            set { ticketSaleGuests = value; RaisePropertyChanged("TicketSaleGuests"); }
        }

        ObservableCollection<Client> clients;
        public ObservableCollection<Client> Clients
        {
            get
            {
                if (clients == null)
                    clients = new ObservableCollection<Client>(Repositories.RClients.GetAll());
                return clients;
            }
            set { clients = value; RaisePropertyChanged("Clients"); }
        }

        ObservableCollection<Guest> guests;
        public ObservableCollection<Guest> Guests
        {
            get
            {
                if (guests == null)
                    guests = new ObservableCollection<Guest>(Repositories.RGuests.GetAll());
                return guests;
            }
            set { guests = value; RaisePropertyChanged("Guest"); }
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


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private Client currentClient;
        public Client CurrentClient
        {
            get { return currentClient; }
            set { currentClient = value; RaisePropertyChanged("CurrentClient"); }
        }

        private int? selectedIndexClient;
        public int? SelectedIndexClient
        {
            get { return selectedIndexClient; }
            set
            {
                selectedIndexClient = value;
                CurrentClient = Repositories.RClients.FindById(ClientsList[(int)value].Id);
                RaisePropertyChanged("SelectedIndexClient");
            }
        }

        private string sname;
        public string SName
        {
            get { return sname; }
            set { sname = value; RaisePropertyChanged("SName"); }
        }

        private string pname;
        public string PName
        {
            get { return pname; }
            set { pname = value; RaisePropertyChanged("PName"); }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; RaisePropertyChanged("PhoneNumber"); }
        }

        private string birthday;
        public string Birthday
        {
            get { return birthday; }
            set { birthday = value; RaisePropertyChanged("Birthday"); }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; RaisePropertyChanged("Email"); }
        }

        private string login;
        public string Login
        {
            get { return login; }
            set { login = value; RaisePropertyChanged("Login"); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; RaisePropertyChanged("Password"); }
        }

        RelayCommand changeClientCommand;
        public RelayCommand ChangeClientCommand
        {
            get
            {
                if (changeClientCommand == null)
                    changeClientCommand = new RelayCommand(ExecuteChangeClientCommand, CanExecuteChangeClientCommand);
                return changeClientCommand;
            }
        }

        public void ExecuteChangeClientCommand()
        {
            SName = CurrentClient.SName;
            PName = CurrentClient.PName;
            PhoneNumber = CurrentClient.PhoneNumber;
            Birthday = CurrentClient.Birthday;
            Email = CurrentClient.Email;
            Login = CurrentClient.Login;
            Password = CurrentClient.Password;

            ButtonClientAddFunction = "Cохранить";
        }

        public bool CanExecuteChangeClientCommand()
        {
            return SelectedIndexClient >= 0;
        }


        private string buttonClientAddFunction = "Добавить";
        public string ButtonClientAddFunction
        {
            get { return buttonClientAddFunction; }
            set { buttonClientAddFunction = value; RaisePropertyChanged("ButtonClientAddFunction"); }
        }



        RelayCommand addClientCommand;
        public RelayCommand AddClientCommand
        {
            get
            {
                if (addClientCommand == null)
                    addClientCommand = new RelayCommand(ExecuteAddClientCommandd, CanExecuteAddClientCommand);
                return addClientCommand;
            }
        }

        public void ExecuteAddClientCommandd()
        {
            if (ButtonClientAddFunction == "Добавить")
            {
                Client exc = new Client() { SName = SName, PName = PName, PhoneNumber = PhoneNumber, Birthday = Birthday, Email = Email, Login = Login, Password = Password };
                Repositories.RClients.Add(exc);
                ClearClientFields();
            }
            else
            {
                CurrentClient.SName = SName;
                CurrentClient.PName = PName;
                CurrentClient.PhoneNumber = PhoneNumber;
                CurrentClient.Birthday = Birthday;
                CurrentClient.Email = Email;
                CurrentClient.Login = Login;
                CurrentClient.Password = Password;
                Repositories.RClients.Update(CurrentClient);

                ClearClientFields();
                ButtonClientAddFunction = "Добавить";
            }
        }

        public bool CanExecuteAddClientCommand()
        {
            return !string.IsNullOrEmpty(SName) 
                && !string.IsNullOrEmpty(PName) 
                && !string.IsNullOrEmpty(PhoneNumber)
                && !string.IsNullOrEmpty(Birthday) 
                && !string.IsNullOrEmpty(Email) 
                && !string.IsNullOrEmpty(Login) 
                && !string.IsNullOrEmpty(Password);
        }

        RelayCommand deleteClientCommand;
        public RelayCommand DeleteClientCommand
        {
            get
            {
                if (deleteClientCommand == null)
                    deleteClientCommand = new RelayCommand(ExecuteDeleteClientCommand, CanExecuteDeleteClientCommand);
                return deleteClientCommand;
            }
        }

        public void ExecuteDeleteClientCommand()
        {
            Repositories.RClients.Remove(ClientsList[(int)SelectedIndexClient]);
            ClientsList = null;
        }

        public bool CanExecuteDeleteClientCommand()
        {
            return SelectedIndexClient >= 0;
        }


        public void ClearClientFields()
        {
            ClientsList = null;
            SName = "";
            PName = "";
            PhoneNumber = "";
            Birthday = "";
            Email = "";
            Login = "";
            Password = "";
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private Client currentClientReservation; //ReservationExcursionName
        public Client CurrentClientReservation
        {
            get { return currentClientReservation; }
            set { currentClientReservation = value; RaisePropertyChanged("CurrentClientReservation"); }
        }

        private Excursion currentExcursionReservation;
        public Excursion CurrentExcursionReservation
        {
            get { return currentExcursionReservation; }
            set { currentExcursionReservation = value; RaisePropertyChanged("CurrentExcursionReservation"); }
        }

        private TicketSaleClient currentTicketSaleClient;
        public TicketSaleClient CurrentTicketSaleClient
        {
            get { return currentTicketSaleClient; }
            set { currentTicketSaleClient = value; RaisePropertyChanged("CurrentTicketSaleClient"); }
        }

        private int? selectedIndexTicketSaleClients;
        public int? SelectedIndexTicketSaleClients
        {
            get { return selectedIndexTicketSaleClients; }
            set
            {
                selectedIndexTicketSaleClients = value;
                CurrentTicketSaleClient = TicketSaleClients[(int)value];
                RaisePropertyChanged("SelectedIndexTicketSaleClients");
            }
        }

        private int? ticketSaleClientsTicketAmount;
        public int? TicketSaleClientsTicketAmount
        {
            get { return ticketSaleClientsTicketAmount; }
            set
            {
                ticketSaleClientsTicketAmount = value;
                RaisePropertyChanged("TicketSaleClientsTicketAmount");
            }
        }

        RelayCommand changeClientReservationCommand;
        public RelayCommand ChangeClientReservationCommand
        {
            get
            {
                if (changeClientReservationCommand == null)
                    changeClientReservationCommand = new RelayCommand(ExecuteChangeClientReservationCommand, CanExecuteChangeClientReservationCommand);
                return changeClientReservationCommand;
            }
        }

        public void ExecuteChangeClientReservationCommand()
        {
            CurrentClientReservation = TicketSaleClients[(int)SelectedIndexTicketSaleClients].Client;
            CurrentExcursionReservation = TicketSaleClients[(int)SelectedIndexTicketSaleClients].TicketSale.Excursion;
            TicketSaleClientsTicketAmount = TicketSaleClients[(int)SelectedIndexTicketSaleClients].TicketAmount;
            ButtonClientAddFunction = "Cохранить";
        }

        public bool CanExecuteChangeClientReservationCommand()
        {
            return SelectedIndexTicketSaleClients >= 0;
        }


        RelayCommand addTicketSaleClientCommand;
        public RelayCommand AddTicketSaleClientCommand
        {
            get
            {
                if (addTicketSaleClientCommand == null)
                    addTicketSaleClientCommand = new RelayCommand(ExecuteAddTicketSaleClientCommand, CanExecuteAddTicketSaleClientCommand);
                return addTicketSaleClientCommand;
            }
        }

        public void ExecuteAddTicketSaleClientCommand()
        {
            if (ButtonClientAddFunction == "Добавить")
            {
                /*Client exc = new Client() { SName = SName, PName = PName, PhoneNumber = PhoneNumber, Birthday = Birthday, Email = Email, Login = Login, Password = Password };*/
                Repositories.RTicketSaleClients.Add(new TicketSaleClient() { Client = CurrentClientReservation, TicketSale = Repositories.RTicketSales.FindAll(x=>x.Excursion.Id == CurrentExcursionReservation.Id).FirstOrDefault(), TicketAmount = (int)TicketSaleClientsTicketAmount });
                ClearTicketSaleClientFields();
            }
            else
            {
                CurrentTicketSaleClient.Client = CurrentClientReservation;
                CurrentTicketSaleClient.TicketSale = Repositories.RTicketSales.FindAll(x => x.Excursion.Id == CurrentExcursionReservation.Id).FirstOrDefault();
                CurrentTicketSaleClient.TicketAmount = (int)TicketSaleClientsTicketAmount;
                Repositories.RTicketSaleClients.Update(CurrentTicketSaleClient);

                ClearTicketSaleClientFields();
                ButtonClientAddFunction = "Добавить";
            }
        }

        public bool CanExecuteAddTicketSaleClientCommand()
        {
            return TicketSaleClientsTicketAmount != null;
        }

        RelayCommand deleteTicketSaleClientCommand;
        public RelayCommand DeleteTicketSaleClientCommand
        {
            get
            {
                if (deleteTicketSaleClientCommand == null)
                    deleteTicketSaleClientCommand = new RelayCommand(ExecuteDeleteTicketSaleClientCommand, CanExecuteDeleteTicketSaleClientCommand);
                return deleteTicketSaleClientCommand;
            }
        }

        public void ExecuteDeleteTicketSaleClientCommand()
        {
            Repositories.RTicketSaleClients.Remove(TicketSaleClients[(int)SelectedIndexTicketSaleClients]);
            TicketSaleClients = null;
        }

        public bool CanExecuteDeleteTicketSaleClientCommand()
        {
            return SelectedIndexTicketSaleClients >= 0;
        }

        public void ClearTicketSaleClientFields()
        {
            TicketSaleClients = null;
            CurrentClientReservation = null;
            CurrentExcursionReservation = null;
            TicketSaleClientsTicketAmount = null;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////





        private Guest currentGuestReservation; //ReservationExcursionName
        public Guest CurrentGuestReservation
        {
            get { return currentGuestReservation; }
            set { currentGuestReservation = value; RaisePropertyChanged("CurrentGuestReservation"); }
        }

        private Excursion currentExcursionReservationGuest;
        public Excursion CurrentExcursionReservationGuest
        {
            get { return currentExcursionReservationGuest; }
            set { currentExcursionReservationGuest = value; RaisePropertyChanged("CurrentExcursionReservationGuest"); }
        }

        private TicketSaleGuest currentTicketSaleGuest;
        public TicketSaleGuest CurrentTicketSaleGuest
        {
            get { return currentTicketSaleGuest; }
            set { currentTicketSaleGuest = value; RaisePropertyChanged("CurrentTicketSaleGuest"); }
        }

        private int? selectedIndexTicketSaleGuests;
        public int? SelectedIndexTicketSaleGuests
        {
            get { return selectedIndexTicketSaleGuests; }
            set
            {
                selectedIndexTicketSaleGuests = value;
                CurrentTicketSaleGuest = TicketSaleGuests[(int)value];
                RaisePropertyChanged("SelectedIndexTicketSaleGuests");
            }
        }

        private int? ticketSaleGuestsTicketAmount;
        public int? TicketSaleGuestsTicketAmount
        {
            get { return ticketSaleGuestsTicketAmount; }
            set
            {
                ticketSaleGuestsTicketAmount = value;
                RaisePropertyChanged("TicketSaleGuestsTicketAmount");
            }
        }

        RelayCommand changeGuestReservationCommand;
        public RelayCommand ChangeGuestReservationCommand
        {
            get
            {
                if (changeGuestReservationCommand == null)
                    changeGuestReservationCommand = new RelayCommand(ExecuteChangeGuestReservationCommand, CanExecuteChangeGuestReservationCommand);
                return changeGuestReservationCommand;
            }
        }

        public void ExecuteChangeGuestReservationCommand()
        {
            CurrentGuestReservation = TicketSaleGuests[(int)SelectedIndexTicketSaleGuests].Guest;
            CurrentExcursionReservationGuest = TicketSaleGuests[(int)SelectedIndexTicketSaleGuests].TicketSale.Excursion;
            TicketSaleGuestsTicketAmount = TicketSaleGuests[(int)SelectedIndexTicketSaleGuests].TicketAmount;
            ButtonClientAddFunction = "Cохранить";
        }

        public bool CanExecuteChangeGuestReservationCommand()
        {
            return SelectedIndexTicketSaleGuests >= 0;
        }


        RelayCommand addTicketSaleGuestCommand;
        public RelayCommand AddTicketSaleGuestCommand
        {
            get
            {
                if (addTicketSaleGuestCommand == null)
                    addTicketSaleGuestCommand = new RelayCommand(ExecuteAddTicketSaleGuestCommand, CanExecuteAddTicketSaleGuestCommand);
                return addTicketSaleGuestCommand;
            }
        }

        public void ExecuteAddTicketSaleGuestCommand()
        {
            if (ButtonClientAddFunction == "Добавить")
            {
                /*Client exc = new Client() { SName = SName, PName = PName, PhoneNumber = PhoneNumber, Birthday = Birthday, Email = Email, Login = Login, Password = Password };*/
                Repositories.RTicketSaleGuests.Add(new TicketSaleGuest() { 
                    Guest = CurrentGuestReservation, 
                    TicketSale = Repositories.RTicketSales.FindAll(x => x.Excursion.Id == CurrentExcursionReservationGuest.Id).FirstOrDefault(), 
                    TicketAmount = (int)TicketSaleGuestsTicketAmount });
                ClearTicketSaleGuestFields();
            }
            else
            {
                CurrentTicketSaleGuest.Guest = CurrentGuestReservation;
                CurrentTicketSaleGuest.TicketSale = Repositories.RTicketSales.FindAll(x => x.Excursion.Id == CurrentExcursionReservationGuest.Id).FirstOrDefault();
                CurrentTicketSaleGuest.TicketAmount = (int)TicketSaleGuestsTicketAmount;
                Repositories.RTicketSaleGuests.Update(CurrentTicketSaleGuest);

                ClearTicketSaleGuestFields();
                ButtonClientAddFunction = "Добавить";
            }
        }

        public bool CanExecuteAddTicketSaleGuestCommand()
        {
            return TicketSaleGuestsTicketAmount != null;
        }

        RelayCommand deleteTicketSaleGuestCommand;
        public RelayCommand DeleteTicketSaleGuestCommand
        {
            get
            {
                if (deleteTicketSaleGuestCommand == null)
                    deleteTicketSaleGuestCommand = new RelayCommand(ExecuteDeleteGuestSaleClientCommand, CanExecuteDeleteGuestSaleClientCommand);
                return deleteTicketSaleGuestCommand;
            }
        }

        public void ExecuteDeleteGuestSaleClientCommand()
        {
            Repositories.RTicketSaleGuests.Remove(TicketSaleGuests[(int)SelectedIndexTicketSaleGuests]);
            TicketSaleClients = null;
        }

        public bool CanExecuteDeleteGuestSaleClientCommand()
        {
            return SelectedIndexTicketSaleClients >= 0;
        }

        public void ClearTicketSaleGuestFields()
        {
            TicketSaleGuests = null;
            CurrentGuestReservation = null;
            CurrentExcursionReservationGuest = null;
            TicketSaleGuestsTicketAmount = null;
        }
    }
}

