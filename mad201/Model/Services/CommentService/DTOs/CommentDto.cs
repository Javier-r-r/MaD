using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.CommentService.DTOs
{
    public class CommentDto
    {
        public long Id { get; set; }
        public string content { get; set; }
        public int rate { get; set; }
        public DateTime date { get; set; }

        public long clientId { get; set; }
        public long restaurantId { get; set; }
    }
}
