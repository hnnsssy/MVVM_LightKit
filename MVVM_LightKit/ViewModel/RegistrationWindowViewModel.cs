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
            if(!string.IsNullOrEmpty(CurrentClient.SName) && !string.IsNullOrEmpty(CurrentClient.PName) && !string.IsNullOrEmpty(CurrentClient.PhoneNumber) && !string.IsNullOrEmpty(CurrentClient.Login) && !string.IsNullOrEmpty(CurrentClient.Password))
            {
                Repositories.RClients.Add(CurrentClient);
                System.Windows.Application.Current.Resources["Login"] = CurrentClient.Login;
                System.Windows.Application.Current.Resources["Password"] = CurrentClient.Password;
                IsWindowVisible = false;
                SelectExcursion selectExcursion = new SelectExcursion();
                selectExcursion.Show();
            }
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
    }
}
