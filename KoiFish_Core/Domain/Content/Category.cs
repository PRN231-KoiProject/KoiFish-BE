using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFish_Core.Domain.Content
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }
        public string Breeds { get; set; }
        public string Description { get; set; }
    }
}
