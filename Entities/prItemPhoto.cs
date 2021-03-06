using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCoreOrnekOzellikler.Entities
{
    public class prItemPhoto
    {
        public int Id { get; set; }

        public string ImagePath { get; set; }

        public int cdItemId { get; set; }

        public virtual cdItem CdItem { get; set; }
    }
}
