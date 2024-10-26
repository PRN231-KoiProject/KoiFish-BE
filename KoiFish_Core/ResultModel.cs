using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KoiFish_Core
{
    public class ResultModel
    {
        public bool Success { get; set; }
        public int Status { get; set; }
        public object? Data { get; set; }
        public string? Message { get; set; }
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
