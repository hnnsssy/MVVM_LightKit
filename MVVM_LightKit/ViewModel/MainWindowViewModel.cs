using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MVVM_LightKit.Model;
using MVVM_LightKit.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace MVVM_LightKit.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        //LOAD DB CONTEXT
        RelayCommand _loadDataBaseContex;
        public RelayCommand LoadDataBaseContex
        {
            get
            {
                if (_loadDataBaseContex == null)
                    _loadDataBaseContex = new RelayCommand(new Action(()=> { Repositories.InitializeGenericRepositories(); Repositories.RClients.GetAll(); }), true);
                return _loadDataBaseContex;
            }
        }
        ////////////////////////////////////////////////////////


        //OPEN REG WINDOW COMMAND
        RelayCommand _openRegistrationWindow;
        public RelayCommand OpenRegistrationWindow
        {
            get
            {
                if (_openRegistrationWindow == null)
                    _openRegistrationWindow = new RelayCommand(button_Registration_Click, true);
                return _openRegistrationWindow;
            }
        }

        private void button_Registration_Click()
        {
            IsWindowVisible = false;
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
        }
        ////////////////////////////////////////////////////////


        //OPEN LOGIN WINDOW COMMAND
        RelayCommand _openLoginWindow;
        public RelayCommand OpenLoginWindow
        {
            get
            {
                if (_openLoginWindow == null)
                    _openLoginWindow = new RelayCommand(button_Login_Click, true);
                return _openLoginWindow;
            }
        }

        private void button_Login_Click()
        {
            IsWindowVisible = false;
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }
        ////////////////////////////////////////////////////////


        //OPEN LOGIN WINDOW COMMAND
        RelayCommand openGuestWindow;
        public RelayCommand OpenGuestWindow
        {
            get
            {
                if (openGuestWindow == null)
                    openGuestWindow = new RelayCommand(button_Guest_Click, true);
                return openGuestWindow;
            }
        }

        private void button_Guest_Click()
        {
            IsWindowVisible = false;
            GuestWindow guestWindow = new GuestWindow();
            guestWindow.Show();
        }

        /*RelayCommand _openSelectExcursionWindow;
        public RelayCommand SelectExcursionWindow
        {
            get
            {
                if (_openSelectExcursionWindow == null)
                    _openSelectExcursionWindow = new RelayCommand(button_SelectExcursion_Click, true);
                return _openSelectExcursionWindow;
            }
        }

        private void button_SelectExcursion_Click()
        {
            IsWindowVisible = false;
            SelectExcursion selectExcursion = new SelectExcursion();
            selectExcursion.Show();
        }*/
        ////////////////////////////////////////////////////////


        //WINDOW VISIBILITY
        bool _isWindowVisible = true;
        public bool IsWindowVisible
        {
            get { return _isWindowVisible; }
            set
            {
                _isWindowVisible = value;
                RaisePropertyChanged("IsWindowVisible");
            }
        }
        ////////////////////////////////////////////////////////











        /*public RelayCommand OpenRegistrationWindow { get; set; }
        public MainWindowViewModel()
        {
            OpenRegistrationWindow = new RelayCommand(button_Registration_Click, true);
        }*/



        /*Client _currentClient;
        public Client CurrentClient
        {
            get
            {
                if (_currentClient == null)
                    _currentClient = new Client();
                return _currentClient;
            }
            set
            {
                _currentClient = value;


                RaisePropertyChanged("CurrentClient");
            }
        }


        ObservableCollection<Client> _clients;
        public ObservableCollection<Client> Clients
        {
            get
            {
                if (_clients == null)
                    _clients = ClientRepository.AllClients;
                return _clients;
            }
        }*/


        /*RelayCommand _addClientCommand;
        public RelayCommand AddClient
        {
            get
            {
                if (_addClientCommand == null)
                    _addClientCommand = new RelayCommand(ExecuteAddClientCommand, CanExecuteAddClientCommand);
                return _addClientCommand;
            }
        }


        public void ExecuteAddClientCommand()
        {
            Clients.Add(_currentClient);
            CurrentClient = null;
        }


        public bool CanExecuteAddClientCommand()
        {
            if (string.IsNullOrEmpty(CurrentClient.FirstName) ||
                string.IsNullOrEmpty(CurrentClient.LastName))
                return false;
            return true;
        }*/
    }
}

