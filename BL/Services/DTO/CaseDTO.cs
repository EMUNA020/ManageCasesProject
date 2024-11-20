using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.DTO
{
    public class CaseDTO
    {
        //generate the properties
        public int? Id { get; set; }
        public string? CaseNumber { get; set; }
        public string? CaseName { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Priority { get; set; }
        public string? Severity { get; set; }
        public string? Type { get; set; }
        public List<string> Parties { get; set; }
        public string CaseTitle { get; set; }

    }
}
