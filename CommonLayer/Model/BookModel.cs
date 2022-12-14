using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class BookModel
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public float Ratings { get; set; }
        public int Reviews { get; set; }
        public float DiscountedPrice { get; set; }
        public float OriginalPrice { get; set; }
        public int Quantity { get; set; }

    }

}
