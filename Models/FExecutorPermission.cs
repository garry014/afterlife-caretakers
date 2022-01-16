using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Models
{
    [Keyless]
    public class FExecutorPermission
    {
        public int executor_id { get; set; }
        public int funeral_id { get; set; }
    }
}
