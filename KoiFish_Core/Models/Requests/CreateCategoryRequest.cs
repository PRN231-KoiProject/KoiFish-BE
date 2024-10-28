using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoiFish_Core.Models.Requests
{
    public class CreateCategoryRequest
    {
        public string Breeds { get; set; }
        public string Description { get; set; }
    }
}