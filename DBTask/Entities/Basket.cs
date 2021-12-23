using DBTask.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBTask.Entities
{
    public class Basket: BaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public string Description { get; set; }
    }
}
