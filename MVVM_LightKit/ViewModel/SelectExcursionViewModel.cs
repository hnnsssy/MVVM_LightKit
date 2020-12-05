using GalaSoft.MvvmLight;
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
    }
}
