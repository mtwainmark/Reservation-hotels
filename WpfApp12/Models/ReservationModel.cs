using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.Models
{
    public class ReservationModel
    {
        [Key]
        public int IdReservation { get; set; }
        public string City { get; set; }
        public string Hotel { get; set; }
        public string Room { get; set; }
        public string Client { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
