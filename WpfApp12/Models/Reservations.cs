using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.Models
{
    public class Reservations
    {
        public int IdHotels { get; set; }
        public int IdRoom { get; set; }
        public int IdGuest { get; set; }
        [Key]
        public int IdReservation { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
