using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GAPT.Models;

namespace GAPT.ViewModel
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public List<List<Item>> Items { get; set; }
        public List<List<int>> Ids { get; set; }
        public decimal CartTotal { get; set; }
    }
}
