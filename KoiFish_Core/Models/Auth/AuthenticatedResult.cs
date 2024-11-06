using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFish_Core.Models.Auth
{
    public class AuthenticatedResult
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public required DateTime? ExpiredAt { get; set; }
    }
}
