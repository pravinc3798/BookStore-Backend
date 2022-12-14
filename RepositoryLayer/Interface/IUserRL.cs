using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UserModel AddUser(UserModel userModel);
        public string Login(string emailId, string userPassword);
        public string ForgetPassword(string emailId);
        public bool ResetPassword(string emailId, string userPassword, string confirmPassword);
    }
}
