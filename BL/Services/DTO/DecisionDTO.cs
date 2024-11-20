using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.DTO
{
    public class DecisionDTO
    {
        public string DecisionId { get; set; }
        public string CaseId { get; set; }
        public string DecisionText { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
