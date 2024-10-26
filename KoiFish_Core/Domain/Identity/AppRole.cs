using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFish_Core.Domain.Identity
{
    public class AppRole : IdentityRole<Guid>
    {
        [MaxLength(100)]
        public string DisplayName { get; set; }
    }
}
