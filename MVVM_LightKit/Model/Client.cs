using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_LightKit.Model
{
    public class Client : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public string SName { get { return sName; } set { sName = value; OnPropertyChanged("SName"); } }
        [NotMapped]
        private string sName;

        public string PName { get { return pName; } set { pName = value; OnPropertyChanged("PName"); } }
        [NotMapped]
        private string pName;

        public string PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; OnPropertyChanged("PhoneNumber"); } }
        [NotMapped]
        private string phoneNumber;

        public string Birthday { get { return birthday; } set { birthday = value; OnPropertyChanged("Birthday"); } }
        [NotMapped]
        private string birthday;

        public string Email { get { return email; } set { email = value; OnPropertyChanged("Email"); } }
        [NotMapped]
        private string email;

        public string Login { get { return login; } set { login = value; OnPropertyChanged("Login"); } }
        [NotMapped]
        private string login;

        public string Password { get { return password; } set { password = value; OnPropertyChanged("Password"); } }
        [NotMapped]
        private string password;

        public virtual ICollection<TicketSaleClient> TicketSaleClients { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return Login;
        }
    }
}
