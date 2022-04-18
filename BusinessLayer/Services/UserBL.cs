using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public string ForgotPassword(string EmailId)
        {
            try
            {
                return userRL.ForgotPassword(EmailId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string LoginUser(string emailId, string password)
        {
            try
            {
                return userRL.LoginUser(emailId,password);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool RegisterUser(UserModel userModel)
        {
            try
            {
                return userRL.RegisterUser(userModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //reset pasword required email, password,confirmpassword
        public bool ResetPassword(string emailId, string password, string confirmPassword)
        {
            try
            {
                return this.userRL.ResetPassword(emailId, password, confirmPassword);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
