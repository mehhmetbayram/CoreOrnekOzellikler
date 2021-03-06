using ProjeCoreOrnekOzellikler.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCoreOrnekOzellikler.Models
{
    public class ItemModel
    {
        public ItemModel()
        {
            Categories = new List<Category>();
            CdItems = new List<cdItem>();
        }

        public int TotalCountProduct { get; set; }
        public int CurrentPage { get; set; }
        public int CurrentCategory { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public List<cdItem> CdItems { get; set; }
        public List<Category> Categories { get; set; }
    }
}
