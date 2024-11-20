using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.DTO
{
    public class DiscussionDTO
    {
        public string DiscussionId { get; set; }
        public string CaseId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
