using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly IConfiguration configurations;

        public UserRL(IConfiguration configurations)
        {
            this.configurations = configurations;
        }
    }
}
