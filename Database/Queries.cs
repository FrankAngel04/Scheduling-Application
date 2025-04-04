﻿namespace SchedulingApplication.Database
{
    public class Queries
    {
        // Table Queries
        public static string CustomerTable => @"
            SELECT c.customerId, c.customerName, a.address, a.addressId, a.postalCode, a.phone, 
                   ci.city, ci.cityId, co.country, co.countryId 
            FROM customer c 
            JOIN address a ON c.addressId = a.addressId 
            JOIN city ci ON a.cityId = ci.cityId 
            JOIN country co ON ci.countryId = co.countryId";

        // Country Queries
        public static string CountryIndex => @"
            SELECT countryId FROM country ORDER BY countryId DESC LIMIT 1";
        public static string AddCountry => @"
            INSERT INTO country (countryId, country, createDate, createdBy, lastUpdate, lastUpdateBy) 
            VALUES (@CountryId, @Country, NOW(), @CreatedBy, NOW(), @LastUpdateBy)";
        public static string UpdateCountry => @"
            UPDATE country SET country = @Country, lastUpdate = NOW(), lastUpdateBy = @LastUpdateBy 
            WHERE countryId = @CountryId";

        // City Queries
        public static string CityIndex => @"
            SELECT cityId FROM city ORDER BY cityId DESC LIMIT 1";
        public static string AddCity => @"
            INSERT INTO city (cityId, city, countryId, createDate, createdBy, lastUpdate, lastUpdateBy) 
            VALUES (@CityId, @City, @CountryId, NOW(), @CreatedBy, NOW(), @LastUpdateBy)";
        public static string UpdateCity => @"
            UPDATE city SET city = @City, countryId = @CountryId, lastUpdate = NOW(), lastUpdateBy = @LastUpdateBy 
            WHERE cityId = @CityId";

        // Address Queries
        public static string AddressIndex => @"
            SELECT addressId FROM address ORDER BY addressId DESC LIMIT 1";
        public static string AddAddress => @"
            INSERT INTO address (addressId, address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdate, lastUpdateBy) 
            VALUES (@AddressId, @Address, '', @CityId, @PostalCode, @PhoneNumber, NOW(), @CreatedBy, NOW(), @LastUpdateBy)";
        public static string UpdateAddress => @"
            UPDATE address SET address = @Address, address2 = '', cityId = @CityId, postalCode = @PostalCode, phone = @PhoneNumber, lastUpdate = NOW(), lastUpdateBy = @LastUpdateBy 
            WHERE addressId = @AddressId";

        // Customer Queries
        public static string CustomerIndex => @"
            SELECT customerId FROM customer ORDER BY customerId DESC LIMIT 1";
        public static string AddCustomer => @"
            INSERT INTO customer (customerId, customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy) 
            VALUES (@CustomerId, @CustomerName, @AddressId, @Active, NOW(), @CreatedBy, NOW(), @LastUpdateBy)";
        public static string UpdateCustomer => @"
            UPDATE customer SET customerName = @CustomerName, addressId = @AddressId, active = @Active, lastUpdate = NOW(), lastUpdateBy = @LastUpdateBy 
            WHERE customerId = @CustomerId";
        public static string DeleteCustomerAppointments => @"
            DELETE FROM appointment WHERE customerId = @CustomerId";
        public static string DeleteCustomer => @"
            DELETE FROM customer WHERE customerId = @CustomerId";
        
        public static string GetCustomers => @"
            SELECT customerId, customerName FROM customer";
        
        public static string DeleteAddress => @"
            DELETE FROM address WHERE addressId = @AddressId";

            public static string DeleteCityIfUnused => @"
                DELETE FROM city WHERE cityId = @CityId AND 
                NOT EXISTS (SELECT 1 FROM address WHERE cityId = @CityId)";

            public static string DeleteCountryIfUnused => @"
                DELETE FROM country WHERE countryId = @CountryId AND 
                NOT EXISTS (SELECT 1 FROM city WHERE countryId = @CountryId)";

       // Appointment Queries
        public static string AddAppointment => @"
            INSERT INTO appointment (appointmentId, customerId, userId, title, description, location, contact, type, url, start, end, createDate, createdBy, lastUpdate, lastUpdateBy) 
            VALUES (@AppointmentId, @CustomerId, @UserId, @Title, @Description, @Location, @Contact, @Type, @URL, @Start, @End, NOW(), @CreatedBy, NOW(), @LastUpdateBy)";
        
        public static string UpdateAppointment => @"
            UPDATE appointment SET customerId = @CustomerId, userId = @UserId, title = @Title, description = @Description, location = @Location, contact = @Contact, type = @Type, url = @URL, start = @Start, end = @End, lastUpdate = NOW(), lastUpdateBy = @LastUpdateBy 
            WHERE appointmentId = @AppointmentId";
        
        public static string GetAppointmentTable => @"
            SELECT ap.appointmentId, ap.customerId, ap.userId, ap.description, ap.location, ap.type, ap.url, ap.start, ap.end, u.userName, c.customerName, a.phone, a.addressId, a.cityId, ci.countryId 
            FROM appointment ap 
            JOIN customer c ON ap.customerId = c.customerId 
            JOIN address a ON c.addressId = a.addressId 
            JOIN city ci ON a.cityId = ci.cityId 
            JOIN country co ON ci.countryId = co.countryId 
            JOIN user u ON ap.userId = u.userId 
            ORDER BY ap.start";
        
        public static string GetAppointmentTimes => @"
            SELECT start, end FROM appointment WHERE DATE(start) = @Date";
        
        public static string FilteredAppointments => @"
            SELECT ap.appointmentId, ap.customerId, ap.userId, ap.description, ap.location, ap.type, ap.url, ap.start, ap.end, u.userName, c.customerName, a.phone, a.addressId, a.cityId, ci.countryId 
            FROM appointment ap 
            JOIN customer c ON ap.customerId = c.customerId 
            JOIN address a ON c.addressId = a.addressId 
            JOIN city ci ON a.cityId = ci.cityId 
            JOIN country co ON ci.countryId = co.countryId 
            JOIN user u ON ap.userId = u.userId 
            WHERE start BETWEEN @StartDate AND @EndDate 
            ORDER BY ap.start";
        
        public static string AppointmentIndex => @"
            SELECT appointmentId FROM appointment ORDER BY appointmentId DESC LIMIT 1";
        public static string DeleteAppointment => @"
            DELETE FROM appointment WHERE appointmentId = @AppointmentId";
        
        public static string UpcomingAppointmentsDetails => @"
            SELECT a.start, c.customerName 
            FROM appointment a 
            JOIN customer c ON a.customerId = c.customerId 
            WHERE a.start BETWEEN @currentTime AND DATE_ADD(@currentTime, INTERVAL 15 MINUTE) 
            AND a.userId = @userId
            ORDER BY a.start ASC";
        
       // User Queries
        public static string GetUsers => @"
            SELECT userId, userName FROM user";
        public static string AuthenticateUser => @"
            SELECT * FROM user WHERE userName=@username AND password=@password";

        // Report Queries
        public static string GetAppointmentTypesByMonth => @"
            SELECT type AS 'Appointment Type', COUNT(type) AS 'Number of Appointments' 
            FROM appointment 
            WHERE MONTH(start) = @month AND YEAR(start) = @year 
            GROUP BY type 
            HAVING COUNT(type) > 0";
        
        public static string GetUserSchedule => @"
            SELECT ap.appointmentId, ap.customerId, ap.userId, ap.description, ap.location, ap.type, ap.url, ap.start, ap.end, u.userName, c.customerName, a.phone, a.addressId, a.cityId, ci.countryId 
            FROM appointment ap 
            JOIN customer c ON ap.customerId = c.customerId 
            JOIN address a ON c.addressId = a.addressId 
            JOIN city ci ON a.cityId = ci.cityId 
            JOIN country co ON ci.countryId = co.countryId 
            JOIN user u ON ap.userId = u.userId 
            WHERE u.userName = @UserName AND u.userId = @UserId 
            ORDER BY ap.start";
        
        public static string GetAppointmentCountByLocation => @"
            SELECT location, COUNT(*) AS appointment_count 
            FROM appointment 
            GROUP BY location";


        public static string SetupDatabaseQuery => @"
            USE `client_schedule`;

            --
            -- populate table `country`
            --
            INSERT INTO `country` VALUES 
            (1,'US','2019-01-01 00:00:00','test','2019-01-01 00:00:00','test'),
            (2,'Canada','2019-01-01 00:00:00','test','2019-01-01 00:00:00','test'),
            (3,'Norway','2019-01-01 00:00:00','test','2019-01-01 00:00:00','test');
            --
            -- populate table `city`
            --
            INSERT INTO `city` VALUES 
            (1,'New York',1,'2019-01-01 00:00:00','test','2019-01-01 00:00:00','test'),
            (2,'Los Angeles',1,'2019-01-01 00:00:00','test','2019-01-01 00:00:00','test'),
            (3,'Toronto',2,'2019-01-01 00:00:00','test','2019-01-01 00:00:00','test'),
            (4,'Vancouver',2,'2019-01-01 00:00:00','test','2019-01-01 00:00:00','test'),
            (5,'Oslo',3,'2019-01-01 00:00:00','test','2019-01-01 00:00:00','test');
            --
            -- populate table `address`
            --
            INSERT INTO `address` VALUES 
            (1,'123 Main','',1,'11111','555-1212','2019-01-01 00:00:00','test','2019-01-01 00:00:00','test'),
            (2,'123 Elm','',3,'11112','555-1213','2019-01-01 00:00:00','test','2019-01-01 00:00:00','test'),
            (3,'123 Oak','',5,'11113','555-1214','2019-01-01 00:00:00','test','2019-01-01 00:00:00','test');
            --
            -- populate table `customer`
            --
            INSERT INTO `customer` VALUES 
            (1,'John Doe',1,1,'2019-01-01 00:00:00','test','2019-01-01 00:00:00','test'),
            (2,'Alfred E Newman',2,1,'2019-01-01 00:00:00','test','2019-01-01 00:00:00','test'),
            (3,'Ina Prufung',3,1,'2019-01-01 00:00:00','test','2019-01-01 00:00:00','test');
            --
            -- populate table `user`
            --
            INSERT INTO `user` VALUES 
            (1,'test','test',1,'2019-01-01 00:00:00','test','2019-01-01 00:00:00','test'),
            (2,'test2','test2',1,'2019-01-01 00:00:00','test',2019-01-01 00:00:00','test2');
            --
            -- populate table `appointment`
            --
            INSERT INTO `appointment` VALUES 
            (1,1,1,'not needed','not needed','not needed','not needed','Presentation','not needed','2019-01-01 00:00:00','2019-01-01 00:00:00','2019-01-01 00:00:00','test','2019-01-01 00:00:00','test'),
            (2,2,1,'not needed','not needed','not needed','not needed','Scrum','not needed','2019-01-01 00:00:00','2019-01-01 00:00:00','2019-01-01 00:00:00','test','2019-01-01 00:00:00','test')";
        
        public static string CheckTablesExistence => @"
            SELECT COUNT(*) 
            FROM information_schema.tables 
            WHERE table_schema = 'client_schedule' 
              AND table_name IN ('country', 'city', 'address', 'customer', 'user', 'appointment')";
        public static string GetTotalUsers => @"
            SELECT COUNT(*) FROM user";
    }
}