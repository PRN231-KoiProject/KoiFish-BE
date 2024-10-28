using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoiFish_Core.Models.Requests
{
    public class CreateKoiFishRequest
    {
        public string FishName { get; set; }
        public string FishElement { get; set; }
        public string Size { get; set; }
        public decimal PriceRange { get; set; }
        public string Lifespan { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
        public List<CreateFishColorRequest> FishColors { get; set; }
        public List<CreateImagesRequest> Images { get; set; }
    }

    public class CreateFishColorRequest
    {
        public Guid ColorId { get; set; }
    }
    public class CreateImagesRequest
    {
        public string ImageUrl { get; set; }
    }
}