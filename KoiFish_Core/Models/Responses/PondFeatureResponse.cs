using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoiFish_Core.Models.Responses
{
    public class PondFeatureResponse
    {
        public Guid PondId { get; set; }
        public string Shape { get; set; }
        public string Position { get; set; }
        public string Direction { get; set; }
        public string Element { get; set; }
        public double Size { get; set; }
        public string PondMaterial { get; set; }
        public string WaterSource { get; set; }
        public string FilterType { get; set; }
        public string CompatibleFish { get; set; }
        public string MaintenanceLevel { get; set; }
    }
}