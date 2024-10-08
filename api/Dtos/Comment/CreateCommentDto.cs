using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 charaters")]
        [MaxLength(280, ErrorMessage = "Title must be 280 charaters")]
        public string title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 charaters")]
        [MaxLength(280, ErrorMessage = "Content must be 280 charaters")]
        public string Content { get; set; } = string.Empty;
        public int StockId { get; set; }
    }
}