using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string ContactNumber { get; set; }
        public string EmailId { get; set; }
        public string UserPassword { get; set; }
    }
}
