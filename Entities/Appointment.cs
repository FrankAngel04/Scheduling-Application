﻿using System;

namespace SchedulingApplication.Entities
{
  public class Appointment
    {
        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }

        public Appointment(int appointmentId, int customerId, int userId, string title, string description, string location, string contact, string type, string url, DateTime start, DateTime end, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy)
        {
            AppointmentId = appointmentId;
            CustomerId = customerId;
            UserId = userId;
            Title = title;
            Description = description;
            Location = location;
            Contact = contact;
            Type = type;
            Url = url;
            Start = start;
            End = end;
            CreateDate = createDate;
            CreatedBy = createdBy;
            LastUpdate = lastUpdate;
            LastUpdateBy = lastUpdateBy;
        }

        public Appointment(int customerId, int userId, string title, string description, string location, string contact, string type, string url, DateTime start, DateTime end, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy)
        {
            CustomerId = customerId;
            UserId = userId;
            Title = title;
            Description = description;
            Location = location;
            Contact = contact;
            Type = type;
            Url = url;
            Start = start;
            End = end;
            CreateDate = createDate;
            CreatedBy = createdBy;
            LastUpdate = lastUpdate;
            LastUpdateBy = lastUpdateBy;
        }
    }
}