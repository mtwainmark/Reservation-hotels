using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp12.Helpers;

namespace WpfApp12.Models
{
    public class Guests : BaseModel
    {
        [Key]
        public int IdGuest { get; set; }
        private string surname;
        private string name;
        private string phoneNumber;
        private string pasport;

        

        public string Surname
        {
            get => surname; set
            {
                surname = value;
                OnPropertyChanged("Surname");
            }
        }
        public string Name
        {
            get => name; set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string PhoneNumber
        {
            get => phoneNumber; set
            {
                phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }
        public string Pasport
        {
            get => pasport;
            set
            {
                pasport = value;
                
                /*
                if (!String.IsNullOrEmpty(pasport))
                {
                    var OldUser = db.Guests.FirstOrDefault(p => p.Pasport == pasport);

                    Surname = OldUser.Surname;
                    Name = OldUser.Name;
                    PhoneNumber = OldUser.PhoneNumber;
                }
                */

                OnPropertyChanged("Pasport");
            }
        }

      
    }
}
