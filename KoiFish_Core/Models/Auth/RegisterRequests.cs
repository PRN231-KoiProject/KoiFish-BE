using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFish_Core.Models.Auth
{
    public class RegisterRequests
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public int BirthYear { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
    }
}
