using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.Models
{
    public class Rooms
    {

        public int IdHotels { get; set; }
        [Key]
        public int IdRoom { get; set; }
        public string TypeRoom { get; set; }
        public int CountGuests { get; set; }
    }
}
