using System.ComponentModel.DataAnnotations.Schema;

namespace donutrun.Models
{
    public class OrderedProduct
    {
        public int OrderedProductId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public int QuantityOrdered { get; set; }


    }
}