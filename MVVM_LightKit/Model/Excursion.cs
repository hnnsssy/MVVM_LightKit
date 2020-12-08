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
    public class Excursion : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }
        [NotMapped]
        private string name;

        public int Duration { get { return duration; } set { duration = value; OnPropertyChanged("Duration"); } }
        [NotMapped]
        private int duration;

        public int Price { get { return price; } set { price = value; OnPropertyChanged("Description"); } }
        [NotMapped]
        private int price;

        public string ExDate { get { return exDate; } set { exDate = value; OnPropertyChanged("ExDate"); } }
        [NotMapped]
        private string exDate;

        public string Type { get { return type; } set { type = value; OnPropertyChanged("Type"); } }
        [NotMapped]
        private string type;

        public string Description { get { return description; } set { description = value; OnPropertyChanged("Description"); } }
        [NotMapped]
        private string description;


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
