using DBTask.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBTask.Entities
{
    public class ProductBasket: BaseEntity
    {
        [ForeignKey("Product")]
        public long ProductId { get; set; }
        public Product Product { get; set; }
        [ForeignKey("Basket")]
        public long BasketId { get; set; }
        public Basket Basket { get; set; }

    }
}
