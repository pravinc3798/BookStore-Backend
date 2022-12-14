using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;

        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public UserModel AddUser(UserModel userModel)
        {
            try
            {
                return userRL.AddUser(userModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Login(string emailId, string userPassword)
        {
            try
            {
                return userRL.Login(emailId, userPassword);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ForgetPassword(string emailId)
        {
            try
            {
                return userRL.ForgetPassword(emailId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ResetPassword(string emailId, string userPassword, string confirmPassword)
        {
            try
            {
                return userRL.ResetPassword(emailId, userPassword, confirmPassword);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
