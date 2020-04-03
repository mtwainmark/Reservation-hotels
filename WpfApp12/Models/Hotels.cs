using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.Models
{
    public class Hotels
    {
        [Key]
        public int IdHotels { get; set; }
        public int Stars { get; set; }
        public string City { get; set; }
        public string Title { get; set; }
    }
}
