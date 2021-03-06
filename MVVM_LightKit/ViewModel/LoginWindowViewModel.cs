﻿using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_LightKit.Model;
using GalaSoft.MvvmLight.CommandWpf;
using MVVM_LightKit.View;

namespace MVVM_LightKit.ViewModel
{
    public class LoginWindowViewModel: ViewModelBase
    {

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; RaisePropertyChanged("Message"); }
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

        /// <summary>
        /// В методе выполняеться проверка на правильность введённых данных. Если проверка прошла успешно, то откроеться окно выбора экскурсий
        /// </summary>
        public void ExecuteLoginCommand()
        {
            try { CurrentClient = Repositories.RClients.FindAll(x => x.Login == currentClient.Login && x.Password == currentClient.Password).First(); }
            catch (Exception) { Message = "Неверный логин или пароль"; return; }
            System.Windows.Application.Current.Resources["Login"] = CurrentClient.Login;
            System.Windows.Application.Current.Resources["Password"] = CurrentClient.Password;
            IsWindowVisible = false;
            SelectExcursion selectExcursion = new SelectExcursion();
            selectExcursion.Show();
        }

        public bool CanExecuteLoginCommand()
        {
            return !string.IsNullOrEmpty(CurrentClient.Login) && !string.IsNullOrEmpty(CurrentClient.Password);
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
