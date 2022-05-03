using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AddressRL:IAddressRL
    {
        public IConfiguration Configuration { get; }
        private SqlConnection sqlConnection;

        public AddressRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string AddAddress(AddressModel address)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BOOKSTORE_db"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlcmd = new SqlCommand("Sp_AddAddress", sqlConnection);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@UserId", address.ID);
                    sqlcmd.Parameters.AddWithValue("@Address", address.Address);
                    sqlcmd.Parameters.AddWithValue("@City", address.City);
                    sqlcmd.Parameters.AddWithValue("@State", address.State);
                    sqlcmd.Parameters.AddWithValue("@AddressTypeId", address.AddressTypeId);
                    sqlConnection.Open();
                    int value = sqlcmd.ExecuteNonQuery();
                    if (value > 0)
                    {
                        return "Address Added successfully";
                    }
                    else
                    {
                        return "Check again address";
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

        public string UpdateAddress(UpdateAddressModel address)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BOOKSTORE_db"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlcmd = new SqlCommand("sp_UpdateAddress", sqlConnection);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@AddressId", address.AddressId);
                    sqlcmd.Parameters.AddWithValue("@Address", address.Address);
                    sqlcmd.Parameters.AddWithValue("@City", address.City);
                    sqlcmd.Parameters.AddWithValue("@State", address.State);
                    sqlcmd.Parameters.AddWithValue("@AddressTypeId", address.AddressTypeId);
                    sqlConnection.Open();
                    int value = sqlcmd.ExecuteNonQuery();
                    if (value > 0)
                    {
                        return "Address updated successfully";
                    }
                    else
                    {
                        return "Check userId address";
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

        public string RemoveAddress(int iD)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BOOKSTORE_db"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlcmd = new SqlCommand("sp_RemoveAddress", sqlConnection);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@UserId", iD);
                    sqlConnection.Open();
                    int value = sqlcmd.ExecuteNonQuery();
                    if (value > 0)
                    {
                        return "Address Removed successfully";
                    }
                    else
                    {
                        return "Check userId address";
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
        public IList<UpdateAddressModel> GetAllAddresses()
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BOOKSTORE_db"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlcmd = new SqlCommand("sp_GetAllAddresses", sqlConnection);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    SqlDataReader sqlData = sqlcmd.ExecuteReader();
                    if (sqlData.HasRows)
                    {
                        List<UpdateAddressModel> allAddress = new List<UpdateAddressModel>();
                        while (sqlData.Read())
                        {
                            UpdateAddressModel address = new UpdateAddressModel();
                            address.AddressId = Convert.ToInt32(sqlData["AddressId"]);
                            address.Address = sqlData["Address"].ToString();
                            address.City = sqlData["City"].ToString();
                            address.State = sqlData["State"].ToString();
                            address.AddressTypeId = Convert.ToInt32(sqlData["AddressTypeId"]);
                            allAddress.Add(address);
                        }
                        return allAddress;
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

        public IList<UpdateAddressModel> GetAddressesbyUserid(int iD)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BOOKSTORE_db"]);

            try
            {
                using (sqlConnection)
                {
                    List<UpdateAddressModel> address = new List<UpdateAddressModel>();
                    SqlCommand sqlCommand = new SqlCommand("sp_GetAddressbyUserid", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserId", iD);
                    sqlConnection.Open();
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    if (sqlData.HasRows)
                    {
                        while (sqlData.Read())
                        {
                            UpdateAddressModel model = new UpdateAddressModel();
                            model.AddressId = Convert.ToInt32(sqlData["AddressId"]);
                            model.Address = sqlData["Address"].ToString();
                            model.City = sqlData["City"].ToString();
                            model.State = sqlData["State"].ToString();
                            model.AddressTypeId = Convert.ToInt32(sqlData["AddressTypeId"]);
                            address.Add(model);
                        }
                        return address;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    
    }

}
