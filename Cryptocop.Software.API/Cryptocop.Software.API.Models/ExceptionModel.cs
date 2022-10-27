using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cryptocop.Software.API.Models
{
    public class ExceptionModel
    {
        public int StatusCode { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}