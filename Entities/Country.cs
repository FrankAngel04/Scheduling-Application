using System;

namespace SchedulingApplication.Entities
{
    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }

        public Country(int countryId, string countryName, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy)
        {
            CountryId = countryId;
            CountryName = countryName;
            CreateDate = createDate;
            CreatedBy = createdBy;
            LastUpdate = lastUpdate;
            LastUpdateBy = lastUpdateBy;
        }
    }
}