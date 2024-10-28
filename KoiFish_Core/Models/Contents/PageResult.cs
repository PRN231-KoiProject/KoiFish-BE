using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoiFish_Core.Models.Contents
{
    public class PageResult<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}