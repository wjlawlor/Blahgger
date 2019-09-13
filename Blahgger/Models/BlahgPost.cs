using System;

namespace Blahgger.Models
{
    public class BlahgPost
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}
