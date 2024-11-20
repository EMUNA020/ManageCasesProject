using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.DTO
{
    public class FileDTO
    {
        public int? Id { get; set; }
        public string? FileName { get; set; }
        public DateTime? DateUploaded { get; set; }
        public string? OrigFileName { get; set; }
        public int? TypeId { get; set; }
        public int? Type { get; set; }

        //public virtual List<PickOrderDTO> PickOrders { get; set; } = new List<PickOrderDTO>();
    }
}
