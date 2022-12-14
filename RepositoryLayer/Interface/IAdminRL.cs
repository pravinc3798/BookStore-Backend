using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IAdminRL
    {
        public bool AddAdministrator(string administrator, string adminPassword);
        public string Login(string administrator, string adminPassword);
    }
}
