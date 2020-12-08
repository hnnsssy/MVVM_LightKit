using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_LightKit.Model
{
    public class TicketSaleClient : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int TicketAmount { get { return ticketAmount; } set { ticketAmount = value; OnPropertyChanged("TicketAmount"); } }
        [NotMapped]
        private int ticketAmount;
        public Client Client { get; set; }
        public virtual TicketSale TicketSale { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
