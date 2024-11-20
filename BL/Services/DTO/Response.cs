using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.DTO
{
    public class Response<T>
    {
        public bool? success { get; set; }
        public string? error { get; set; }
        public string? response { get; set; }
        public T? data { get; set; }
        public int? totalDataSize { get; set; }
    }
}
