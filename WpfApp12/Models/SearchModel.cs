using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.Models
{
    public class SearchModel
    { 
        public int IdHotels { get; set; }
        public int IdRoom { get; set; }
        public string Title { get; set; }
        public string City { get; set; }
        public int Stars { get; set; }
        public string TypeRoom { get; set; }
        public int CountGuests { get; set; }
    }
}
