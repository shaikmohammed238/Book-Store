using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        public bool RegisterUser(UserModel userModel);
        public string LoginUser(string emailId, string password);
        public string ForgotPassword(string EmailId);
        public bool ResetPassword(string emailId, string password, string confirmPassword);
    }
}
