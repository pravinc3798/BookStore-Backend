using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class CartModel
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int CartQty { get; set; }
    }
}
