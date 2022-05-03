using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IAddressRL
    {
        public string AddAddress(AddressModel address);
        public string UpdateAddress(UpdateAddressModel address);
        public string RemoveAddress(int iD);
        public IList<UpdateAddressModel> GetAllAddresses();
        public IList<UpdateAddressModel> GetAddressesbyUserid(int iD);
    }
}
