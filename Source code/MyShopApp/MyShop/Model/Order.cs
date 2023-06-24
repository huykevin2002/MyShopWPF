using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public ObservableCollection<OrderItem> OrderItems { get; set; }
    }
}
