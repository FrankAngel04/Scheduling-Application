using System;

namespace SchedulingApplication.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int AddressId { get; set; }
        public byte Active { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }

        public Customer(int customerId, string customerName, int addressId, byte active, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            AddressId = addressId;
            Active = active;
            CreateDate = createDate;
            CreatedBy = createdBy;
            LastUpdate = lastUpdate;
            LastUpdateBy = lastUpdateBy;
        }

        public Customer(string customerName, int addressId, byte active, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy)
        {
            CustomerName = customerName;
            AddressId = addressId;
            Active = active;
            CreateDate = createDate;
            CreatedBy = createdBy;
            LastUpdate = lastUpdate;
            LastUpdateBy = lastUpdateBy;
        }
    }
}