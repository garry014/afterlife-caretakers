using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string videoLink { get; set; }
        [Required]
        public Int16 releasePeriod { get; set; }
        public string writtenMemo { get; set; }
        public int willMakerID { get; set; }
    }
}
