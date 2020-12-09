using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MVVM_LightKit.Model;
using MVVM_LightKit.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_LightKit.ViewModel
{
    class GuestWindowViewModel : ViewModelBase
    {
        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; RaisePropertyChanged("Message"); }
        }

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
            Guest tmpGuest = Repositories.RGuests.FindAll(x => x.SName == currentGuest.SName && x.PName == currentGuest.PName).FirstOrDefault();
            if (tmpGuest == null)
            {
                Repositories.RGuests.Add(CurrentGuest);
                CurrentGuest = Repositories.RGuests.FindAll(x => x.SName == currentGuest.SName && x.PName == currentGuest.PName).FirstOrDefault();
            }
               
            System.Windows.Application.Current.Resources["SName"] = CurrentGuest.SName;
            System.Windows.Application.Current.Resources["PName"] = CurrentGuest.PName;
            System.Windows.Application.Current.Resources["PhoneNumber"] = CurrentGuest.PhoneNumber;
            IsWindowVisible = false;
            SelectExcursion selectExcursion = new SelectExcursion();
            selectExcursion.Show();
        }


        public bool CanExecuteLoginCommand()
        {
            return !string.IsNullOrEmpty(CurrentGuest.SName) && !string.IsNullOrEmpty(CurrentGuest.PName) && !string.IsNullOrEmpty(CurrentGuest.PhoneNumber);
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
