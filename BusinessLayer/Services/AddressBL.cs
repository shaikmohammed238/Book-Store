using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AddressBL:IAddressBL
    {
        public  IAddressRL addressRL;
        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }

        public string AddAddress(AddressModel address)
        {
            try
            {
                return this.addressRL.AddAddress(address);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IList<UpdateAddressModel> GetAddressesbyUserid(int ID)
        {
            try
            {
               return this.addressRL.GetAddressesbyUserid(ID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IList<UpdateAddressModel> GetAllAddresses()
        {
            try
            {
                return this.addressRL.GetAllAddresses();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string RemoveAddress(int iD)
        {
            try
            {
                return this.addressRL.RemoveAddress(iD);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string UpdateAddress(UpdateAddressModel address)
        {
            try
            {
                return this.addressRL.UpdateAddress(address);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
