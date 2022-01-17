using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Models
{
    public class FExecutorPermission
    {
        public int Id { get; set; }
        public int executor_id { get; set; }
        public int funeral_id { get; set; }
    }
}
