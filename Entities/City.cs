using System;

namespace SchedulingApplication.Entities
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int CountryId { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }

        public City(int cityId, string cityName, int countryId, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy)
        {
            CityId = cityId;
            CityName = cityName;
            CountryId = countryId;
            CreateDate = createDate;
            CreatedBy = createdBy;
            LastUpdate = lastUpdate;
            LastUpdateBy = lastUpdateBy;
        }
    }
}