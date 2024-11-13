using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace MyGarageFinderServer.Models;

public partial class MyGarageFinderDbContext : DbContext
{
    public User? GetUser(string licenseNumber)
    {
        return this.Users.Where(u => u.LicenseNumber == licenseNumber)
                            .FirstOrDefault();
    }

    public Vehicle? GetVehicle(string licenseNumber)
    {
        return this.Vehicles.Where(v => v.LicensePlate ==  licenseNumber)
            .FirstOrDefault();
    }

    public Collection<Vehicle> GetVehicles(User modelUser)
    {
        Collection<Vehicle> result = new Collection<Vehicle>();
        foreach (VehicleUser v in this.VehicleUsers)
        {
            if (v.UserId == modelUser.UserId)
            {
                Vehicle? vh = GetVehicle(v.VehicleId);
                result.Add(vh);
            }
        }
        return result;
    }
}
