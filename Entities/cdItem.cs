using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCoreOrnekOzellikler.Entities
{
    public class cdItem
    {
        public cdItem()
        {
            this.PrItemPhotos = new HashSet<prItemPhoto>();
        }

        public int Id { get; set; }
        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string Description { get; set; }
        public DateTime  CreateDate { get; set; }

        public TimeSpan CreateTime { get; set; }

        public bool IsActive { get; set; }

        public short Qty { get; set; }

        public decimal Price { get; set; }

        public string ShowImage { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public ICollection<prItemPhoto> PrItemPhotos { get; set; }
    }
}
