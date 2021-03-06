using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCoreOrnekOzellikler.Entities
{
    public class Category
    {
        public Category()
        {
            this.CdItems = new HashSet<cdItem>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string CategoryName { get; set; }

        public ICollection<cdItem> CdItems { get; set; }
    }
}
