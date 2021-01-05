using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MindForkBlog.Models
{
    public class Dislike
    {
        public int Id { get; set; }
        public int DisLikeCount { get; set; }
        public string UserId { get; set; }

        [Column(TypeName = "date")]
        public DateTime DislikeDate { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
