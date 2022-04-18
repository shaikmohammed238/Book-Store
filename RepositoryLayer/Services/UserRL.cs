using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        public IConfiguration Configuration { get; }
        private SqlConnection sqlConnection;

        public UserRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        /// <summary>
        /// login user
        /// </summary>
        /// <param name="emailId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string LoginUser(string emailId, string password)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BOOKSTORE_db"]);
            try
            {
                using (sqlConnection)
                {
                    UserModel model = new UserModel();

                    SqlCommand command = new SqlCommand("sp_LoginUser", sqlConnection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmailId", emailId);
                    command.Parameters.AddWithValue("@Password", password);
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.EmailId = Convert.ToString(reader["EmailId"] == DBNull.Value ? default : reader["EmailId"]);
                            model.Password = Convert.ToString(reader["Password"] == DBNull.Value ? default : reader["Password"]);
                        }
                        string token = GenerateToken(emailId);
                        return token;
                    }
                    else
                    {
                        return null;
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        private string GenerateToken(string emailId)
        {
            {
                // header
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["Jwt:SecretKey"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                // payload
                var claims = new[]
                {
                new Claim("EmailId", emailId)
                };

                // signature
                var token = new JwtSecurityToken(
                    this.Configuration["Jwt:Issuer"],
                    this.Configuration["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }

        public bool RegisterUser(UserModel userModel)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BOOKSTORE_db"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlcmd = new SqlCommand("sp_RegUser", sqlConnection);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@FullName", userModel.FullName);
                    sqlcmd.Parameters.AddWithValue("@EmailId", userModel.EmailId);
                    sqlcmd.Parameters.AddWithValue("@Password", userModel.Password);
                    sqlcmd.Parameters.AddWithValue("@PhoneNumber", userModel.PhoneNumber);

                    sqlConnection.Open();
                    int value = sqlcmd.ExecuteNonQuery();
                    if (value > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public string ForgotPassword(string emailId)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BOOKSTORE_db"]);
            try
            {
                using (sqlConnection)
                {

                    UserModel model = new UserModel();

                    SqlCommand command = new SqlCommand("sp_ForgotPassword", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmailId", emailId);

                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            emailId = Convert.ToString(reader["EmailId"] == DBNull.Value ? default : reader["EmailId"]);
                        }
                        sqlConnection.Close();
                        var token = GenerateToken(emailId);
                        new MsMq().MSMQSender(token);
                        return token;
                    }
                    else
                    {
                        sqlConnection.Close();
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }

        }
        //for reset password required dummy gmail
        public bool ResetPassword(string emailId, string password, string confirmPassword)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BOOKSTORE_db"]);
            try
            {
                if (password == confirmPassword)
                {
                    using (sqlConnection)
                    {
                        SqlCommand command = new SqlCommand("sp_ResetPassword", sqlConnection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@EmailId", emailId);
                        command.Parameters.AddWithValue("@Password", confirmPassword);
                        sqlConnection.Open();
                        int i = command.ExecuteNonQuery();
                        sqlConnection.Close();
                        if (i >= 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
