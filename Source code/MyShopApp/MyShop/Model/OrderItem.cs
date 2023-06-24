using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public Order Order { get; set; }
        public long Price { get; set; }
        public int Quantity { get; set; }

        public long TotalPrice {
            get => Price * Quantity;  
        }
    }
}
