using KoiFish_Core.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFish_Core.Domain.Content
{
    public class KoiFish
    {
        [Key]
        public Guid KoiFishId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
        public string FishName { get; set; }
        public string FishElement { get; set; }
        public string Size { get; set; }
        public decimal PriceRange { get; set; }
        public string Lifespan { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }

        public ICollection<FishPond> FishPonds { get; set; }
        public ICollection<FishColor> FishColors { get; set; }
        public ICollection<Image>Images{get;set;}
    }
}
