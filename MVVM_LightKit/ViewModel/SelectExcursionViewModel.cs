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

        RelayCommand _setCurrentClient;
        public RelayCommand SetCurrentClient
        { //CurrentClient = Repositories.RClients.FindAll(x => x.Login = System.Windows.Application.Current.Resources["Login"]);
            get
            {
                if (_setCurrentClient == null)
                    _setCurrentClient = new RelayCommand(new Action(() => 
                    {
                        string login = (string)System.Windows.Application.Current.Resources["Login"];
                        string pass = (string)System.Windows.Application.Current.Resources["Password"];
                        CurrentClient = Repositories.RClients.FindAll(x => x.Login == login && x.Password == pass).First(); 
                    }), true);
                return _setCurrentClient;
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

            //set { excursionList = value; }
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
