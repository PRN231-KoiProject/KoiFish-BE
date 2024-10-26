using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFish_Core.Domain.Content
{
    public class PondFeature
    {
        [Key]
        public Guid PondId { get; set; }
        public string Shape { get; set; }
        public string Position { get; set; }
        public string Direction { get; set;}
        public string Element { get; set; }
        public double Size { get; set; }
        public string PondMaterial { get; set; }
        public string WaterSource { get; set; }
        public string FilterType { get; set;}
        public string CompatibleFish { get; set;}
        public string MaintenanceLevel { get; set;}

        public ICollection<FishPond> FishPonds { get; set; }
    }
}
