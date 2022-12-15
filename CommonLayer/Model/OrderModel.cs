using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int CartId { get; set; }
        public int AddressId { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
    }
}
