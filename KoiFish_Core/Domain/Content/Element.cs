using KoiFish_Core.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFish_Core.Domain.Content
{
    public class Element
    {
        [Key]
        public int BirthYear { get; set; }
        public string ElementName { get; set; }
        public AppUser User { get; set; }
    }
}
