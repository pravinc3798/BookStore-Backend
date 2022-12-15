using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AddressBL : IAddressBL
    {
        private readonly IAddressRL addressRL;

        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }

        public bool AddAddress(int userId, AddressModel model)
        {
            try
            {
                return addressRL.AddAddress(userId, model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateAddress(int userId, AddressModel model)
        {
            try
            {
                return addressRL.UpdateAddress(userId, model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<AddressModel> GetAllAddresses(int userId)
        {
            try
            {
                return addressRL.GetAllAddresses(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveAddress(int userId, int addressId)
        {
            try
            {
                return addressRL.RemoveAddress(userId, addressId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
