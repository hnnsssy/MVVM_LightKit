using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MVVM_LightKit.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_LightKit.ViewModel
{
    class AdminLoginWindowViewModel : ViewModelBase
    {
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

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; RaisePropertyChanged("Message"); }
        }

        RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                if (loginCommand == null)
                    loginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
                return loginCommand;
            }
        }

        public void ExecuteLoginCommand()
        {
            if(Login == "admin" && Password == "1")
            {
                IsWindowVisible = false;
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Show();
            }
            else
            {
                Message = "Неверный логин или пароль";
            }
        }

        public bool CanExecuteLoginCommand()
        {
            return !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password);
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
    }
}
