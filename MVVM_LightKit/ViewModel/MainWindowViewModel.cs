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
        ////////////////////////////////////////////////////////




        //OPEN LOGIN WINDOW COMMAND
        RelayCommand openAdminWindow;
        public RelayCommand OpenAdminWindow
        {
            get
            {
                if (openAdminWindow == null)
                    openAdminWindow = new RelayCommand(button_Admin_Click, true);
                return openAdminWindow;
            }
        }

        private void button_Admin_Click()
        {
            IsWindowVisible = false;
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
        }
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

