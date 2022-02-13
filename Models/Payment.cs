using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public double Price { get; set; }
        public int UserID { get; set; }
        public DateTime TimeStamp { get; set; }
        public int FormId { get; set; }
    }
}
