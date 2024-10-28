using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFish_Core.Models.Responses
{
    public class CategoryResponse
    {
        public Guid CategoryId { get; set; }
        public string Breeds { get; set; }
        public string Description { get; set; }
    }
}
