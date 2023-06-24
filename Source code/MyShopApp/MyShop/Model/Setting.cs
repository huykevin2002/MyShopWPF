using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    class Setting
    {
        private static int _itemsPerPage = 10;

        public static int GetItemsPerPage() { return _itemsPerPage; }

        public static void SetItemsPerPage(int itemsPerPage) { _itemsPerPage = itemsPerPage; }

    }
}
