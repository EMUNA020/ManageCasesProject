using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.DTO
{
    public class EmailDTO
    {

        public string? From { get; set; }
        public List<string>? To { get; set; } = new List<string>();
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public List<string>? Cc { get; set; } = new List<string>();
        public List<string>? Bcc { get; set; } = new List<string>();
        public string? Attachment { get; set; }
        public List<FileDTO>? Attachments { get; set; } = new List<FileDTO>();


    }
}
