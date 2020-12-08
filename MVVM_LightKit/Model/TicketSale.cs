using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_LightKit.Model
{
    public class TicketSale : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public int Price { get { return price; } set { price = value; OnPropertyChanged("Price"); } }
        [NotMapped]
        private int price;

        public int Amount { get { return amount; } set { amount = value; OnPropertyChanged("Amount"); } }
        [NotMapped]
        private int amount;

        public string Notes { get { return notes; } set { notes = value; OnPropertyChanged("Notes"); } }
        [NotMapped]
        private string notes;

        public virtual Excursion Excursion { get; set; }
        public virtual ICollection<TicketSaleClient> TicketSaleClients { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
