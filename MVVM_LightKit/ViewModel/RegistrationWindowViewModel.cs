using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MVVM_LightKit.Model;
using MVVM_LightKit.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_LightKit.ViewModel
{
    class RegistrationWindowViewModel : ViewModelBase
    {

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

        RelayCommand registerCommand;
        public RelayCommand RegisterCommand
        {
            get
            {
                if (registerCommand == null)
                    registerCommand = new RelayCommand(ExecuteRegisterCommand, true);
                return registerCommand;
            }          
        }

        public void ExecuteRegisterCommand()
        {
            Repositories.RClients.Add(CurrentClient);
            System.Windows.Application.Current.Resources["Login"] = CurrentClient.Login;
            System.Windows.Application.Current.Resources["Password"] = CurrentClient.Password;
            IsWindowVisible = false;
            SelectExcursion selectExcursion = new SelectExcursion();
            selectExcursion.Show();
        }

        public bool CanExecuteRegisterCommand()
        {
            return !string.IsNullOrEmpty(CurrentClient.SName) && !string.IsNullOrEmpty(CurrentClient.PName);
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
            IsWindowVisible = false;
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

        /* public bool CanExecuteRegisterCommand()
         {
             if (string.IsNullOrEmpty(CurrentClient.SName) ||
                 string.IsNullOrEmpty(CurrentClient.PName) ||
                 string.IsNullOrEmpty(CurrentClient.PhoneNumber) ||
                 string.IsNullOrEmpty(CurrentClient.Email) ||
                 CurrentClient.Birthday == null)
                 return false;
             return true;
         }*/

        /*string _textBoxLogin_Text = "Логин";
        public string TextBoxLogin_Text
        {
            get
            {
                return _textBoxLogin_Text;
            }
            set
            {
                _textBoxLogin_Text = value;
                RaisePropertyChanged("TextBoxLogin_Text");
            }
        }

        //OPEN REG WINDOW COMMAND
        RelayCommand _textBox_Login_GotFocus;
        public RelayCommand TextBox_Login_GotFocus
        {
            get
            {
                if (_textBox_Login_GotFocus == null)
                    _textBox_Login_GotFocus = new RelayCommand(textBox_Login_GotFocus, true);
                return _textBox_Login_GotFocus;
            }
        }

        private void textBox_Login_GotFocus()
        {
            if (_textBoxLogin_Text == "Логин")
                _textBoxLogin_Text = "";
        }
        ////////////////////////////////////////////////////////



        RelayCommand _textBox_Login_LostFocus;
        public RelayCommand TextBox_Login_LostFocus
        {
            get
            {
                if (_textBox_Login_LostFocus == null)
                    _textBox_Login_LostFocus = new RelayCommand(textBox_Login_LostFocus, true);
                return _textBox_Login_GotFocus;
            }
        }

        private void textBox_Login_LostFocus()
        {
            if (_textBoxLogin_Text == "")
                _textBoxLogin_Text = "Логин";
        }*/
    }
}
