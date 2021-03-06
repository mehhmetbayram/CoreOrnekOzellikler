using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCoreOrnekOzellikler.Models
{
    public class Product
    {
        public string UrunKodu { get; set; }
        public string UrunAdi { get; set; }
        public decimal Fiyat { get; set; }
        public string Sezon { get; set; }
        public string Tedarikci { get; set; }
        public string Topuk { get; set; }
        public int Stok { get; set; }
        public string AltGrup { get; set; }
    }
}
