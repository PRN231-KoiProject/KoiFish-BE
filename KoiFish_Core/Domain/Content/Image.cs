using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFish_Core.Domain.Content
{
    public class Image
    {
        public Guid ImageId { get; set; }
        public Guid KoiFishId { get; set; }
        public string ImageUrl { get; set; }

        [ForeignKey(nameof(KoiFishId))]
        public KoiFish KoiFish { get; set; }
    }
}
