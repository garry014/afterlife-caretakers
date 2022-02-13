using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Models
{
    public class WitAppointment
    {
        public int Id { get; set; }
        public string ApptType { get; set; }
        public string CustName { get; set; }
        public int CustId { get; set; }
        public string ConsultName { get; set; }
        public int ConsultId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Date { get; set; }
        [Required (ErrorMessage ="Please choose your appointment time.")]
        public int StartTime { get; set; }
        [Required(ErrorMessage = "Please choose the duration of your appointment.")]
        public int Duration { get; set; }
        public Boolean ApptStatus { get; set; }
    } 
   
}
