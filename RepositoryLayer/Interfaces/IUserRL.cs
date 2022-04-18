using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public bool RegisterUser(UserModel userModel);
        public string LoginUser(string emailId, string password);
        public string ForgotPassword(string emailId);
        //using for reset password
        public bool ResetPassword(string emailId, string password, string confirmPassword);

    }
}
