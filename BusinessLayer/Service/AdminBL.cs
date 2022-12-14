using BusinessLayer.Interfaces;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AdminBL : IAdminBL
    {
        private readonly IAdminRL adminRL;

        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public bool AddAdministrator(string administrator, string adminPassword)
        {
            try
            {
                return adminRL.AddAdministrator(administrator, adminPassword);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Login(string administrator, string adminPassword)
        {
            try
            {
                return adminRL.Login(administrator, adminPassword);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
