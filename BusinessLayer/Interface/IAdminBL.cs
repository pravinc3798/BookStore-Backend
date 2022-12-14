using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IAdminBL
    {
        public bool AddAdministrator(string administrator, string adminPassword);
        public string Login(string administrator, string adminPassword);
    }
}
