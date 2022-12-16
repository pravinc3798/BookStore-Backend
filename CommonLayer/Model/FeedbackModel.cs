using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class FeedbackModel
    {
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public float Ratings { get; set; }
        public string Comments { get; set; }
    }
}
