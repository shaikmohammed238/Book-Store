using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Book_Store.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserBL userBL;

        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost("Register")]
        public IActionResult RegisterUser (UserModel userModel)
        {
            try
            {
                var Result = userBL.RegisterUser(userModel);
                if (Result)
                {
                    return this.Ok(new { success = true, message = "Registration Successful", data = Result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Registration Unsuccessful" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost("Login")]
        public IActionResult LoginUser(string EmailId,string Password)
        {
            try
            {
                var login = this.userBL.LoginUser(EmailId, Password);
                return this.Ok(new { status = 200, isSuccess = true, Message = "Logged in", data = login });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { status = 400, isSuccess = false, Message = e.Message });
            }
        }
        [HttpPost("Forgot")]
        public IActionResult ForgotPassword(string EmailId)
        {
            try
            {
                var forgotPassword = this.userBL.ForgotPassword(EmailId);
                if (forgotPassword != null)
                {
                    return this.Ok(new { Success = true, message = " Token Sent to your email to reset password", Response = forgotPassword });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Check email id is register or not" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpPut("Reset")]

        public IActionResult ResetPassword(string password, string confirmPassword)
        {
            try
            {
                var emailId = User.Claims.FirstOrDefault(e => e.Type == "EmailId").Value.ToString();
                if (this.userBL.ResetPassword(emailId, password, confirmPassword))
                {
                    return this.Ok(new { Success = true, message = " Reset password sucessfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = " check your email" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
