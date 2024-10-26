using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFish_Core.Domain.Content
{
    public class FishPond
    {
        [Key]
        public Guid FishPondId { get; set; }
        public Guid KoiFishId { get; set; }
        public Guid PondId { get; set; }


        [ForeignKey(nameof(KoiFishId))]
        public KoiFish KoiFish { get; set; }
        [ForeignKey(nameof(PondId))]
        public PondFeature PondFeature { get; set; }
    }
}
