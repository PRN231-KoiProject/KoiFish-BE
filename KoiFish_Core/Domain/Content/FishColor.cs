using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KoiFish_Core.Domain.Content
{
    public class FishColor
    {
        public Guid FishColorId { get; set; }
        public Guid ColorId { get; set; }
        public Guid KoiFishId { get; set; }
        [ForeignKey(nameof(KoiFishId))]
        public KoiFish KoiFish { get; set; }
        [ForeignKey(nameof(ColorId))]
        public Color Color { get; set; }
    }
}