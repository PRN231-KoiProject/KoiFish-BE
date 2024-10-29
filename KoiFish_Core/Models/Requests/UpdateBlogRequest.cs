using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoiFish_Core.Models.Requests
{
    public class UpdateBlogRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}