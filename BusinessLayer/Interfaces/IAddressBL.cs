using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IAddressBL
    {
        public string AddAddress(AddressModel address);
        public string UpdateAddress(UpdateAddressModel address);
        public IList<UpdateAddressModel> GetAllAddresses();
        public string RemoveAddress(int iD);
        public IList<UpdateAddressModel> GetAddressesbyUserid(int ID);
    }
}
