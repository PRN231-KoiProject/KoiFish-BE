using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFish_Core.Models.Auth
{
    public class LoginRequests
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
