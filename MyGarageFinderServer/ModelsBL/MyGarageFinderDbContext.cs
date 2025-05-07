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
    

    public ObservableCollection<Vehicle> GetVehicles(User modelUser)
    {
        ObservableCollection<Vehicle> result = new ObservableCollection<Vehicle>();
        List<string> list = new List<string>();
        foreach (VehicleUser v in this.VehicleUsers)
        {
            if (v.UserId == modelUser.UserId)
            {
                list.Add(v.VehicleId);
            }
        }
        foreach (string v in list)
        {
            result.Add(GetVehicle(v));
        }
        return result;
    }

    public ObservableCollection<VehicleUser> GetUserVehicles(User modelUser)
    {
        ObservableCollection<VehicleUser> result = new ObservableCollection<VehicleUser>();
        foreach (VehicleUser v in this.VehicleUsers)
        {
            if (v.User.UserId == modelUser.UserId)
            {
                result.Add(v);
            }
        }
        return result;
    }

}
