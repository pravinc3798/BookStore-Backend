using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IAddressRL
    {
        public bool AddAddress(int userId, AddressModel model);
        public bool UpdateAddress(int userId, AddressModel model);
        public List<AddressModel> GetAllAddresses(int userId);
        public bool RemoveAddress(int userId, int addressId);
    }
}
