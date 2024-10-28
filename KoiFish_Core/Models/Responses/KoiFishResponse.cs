using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoiFish_Core.Models.Responses
{
    public class KoiFishResponse
    {
        public Guid KoiFishId { get; set; }
        public string Category { get; set; }
        public string User { get; set; }
        public string FishName { get; set; }
        public string FishElement { get; set; }
        public string Size { get; set; }
        public decimal PriceRange { get; set; }
        public string Lifespan { get; set; }
        public List<ImageResponses> Images { get; set; }
        public List<ColorResponses> Colors { get; set; }
    }
    public class ImageResponses
    {
        public string ImageUrl { get; set; }
    }
    public class ColorResponses
    {
        public string ColorName { get; set; }
    }
}