using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class WishlistModel
    {
        public int WishlistId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
    }
}
