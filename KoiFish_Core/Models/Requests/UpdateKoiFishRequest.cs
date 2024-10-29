using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoiFish_Core.Models.Requests
{
    public class UpdateKoiFishRequest
    {
        public string FishName { get; set; }
        public string FishElement { get; set; }
        public string Size { get; set; }
        public decimal PriceRange { get; set; }
        public string Lifespan { get; set; }

        public List<UpdateFishColorRequest> FishColors { get; set; }
        public List<UpdateImagesRequest> Images { get; set; }
    }
    public class UpdateFishColorRequest
    {
        public Guid ColorId { get; set; }
    }
    public class UpdateImagesRequest
    {
        public string ImageUrl { get; set; }
    }
}