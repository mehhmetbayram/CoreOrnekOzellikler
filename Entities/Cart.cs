using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCoreOrnekOzellikler.Entities
{
    public class Cart
    {
        public Cart()
        {
            this.CartLines = new List<CartLine>();
        }
        public List<CartLine> CartLines { get; set; }

        public decimal Amount => CartLines.Sum(x => x.cdItem.Price * x.Quantity);
    }
}
