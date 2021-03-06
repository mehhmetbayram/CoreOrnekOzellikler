using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCoreOrnekOzellikler.Entities
{
    public class CartLine
    {
        public cdItem cdItem { get; set; }

        public int Quantity { get; set; }
    }
}
