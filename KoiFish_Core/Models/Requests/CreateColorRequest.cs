using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoiFish_Core.Models.Requests
{
    public class CreateColorRequest
    {
        public string ColorName { get; set; }
        public string Description { get; set; }
    }
}