using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class AddressModel
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public int AddressTypeId { get; set; }
        public string MainAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
