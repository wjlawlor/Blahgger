using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blahgger.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int BlahgPostId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}