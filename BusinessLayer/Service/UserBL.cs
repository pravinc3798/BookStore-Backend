using BusinessLayer.Interface;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRl;

        public UserBL(IUserRL userRl)
        {
            this.userRl = userRl;
        }
    }
}
