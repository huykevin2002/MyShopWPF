using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public long Year { get; set; }
        public long SellingPrice { get; set; }
        public long PurchasePrice { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public Classification Classification { get; set; }
    }
}
