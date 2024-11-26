using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Data
{
    public class ErrorResponse
    {
        public string Message { get; set; } = string.Empty;
        public string StackTrace { get; set; } = string.Empty;
    }

}