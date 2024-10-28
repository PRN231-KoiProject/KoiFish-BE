using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFish_Core.Domain.Content
{
    public class Color
    {
        [Key]
        public Guid ColorId { get; set; }
        public string ColorName { get; set; }
        public string Description { get; set; }
        public ICollection<FishColor> FishColors { get; set; }
    }
}
