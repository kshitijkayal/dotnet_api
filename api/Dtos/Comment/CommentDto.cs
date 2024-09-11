using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CommentDto
    {
         public int CommentId { get; set; } 
        public string title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = string.Empty;
        public int? StockId { get; set; }
    }
}