using System;

namespace SchedulingApplication.Entities
{
    public class User
    {
        public int UserId { get; }
        public string UserName { get; }
        public string Password { get; }
        public byte Active { get; }
        public DateTime CreateDate { get; }
        public string CreatedBy { get; }
        public DateTime LastUpdate { get; }
        public string LastUpdateBy { get; }

        public User(int userId, string userName, string password, byte active, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy)
        {
            UserId = userId;
            UserName = userName;
            Password = password;
            Active = active;
            CreateDate = createDate;
            CreatedBy = createdBy;
            LastUpdate = lastUpdate;
            LastUpdateBy = lastUpdateBy;
        }
        
        public static User CurrentUser { get; set; }
    }
}