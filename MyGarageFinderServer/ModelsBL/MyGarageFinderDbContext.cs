using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyGarageFinderServer.Models;

public partial class MyGarageFinderDbContext : DbContext
{
    public User? GetUser(string licenseNumber)
    {
        return this.Users.Where(u => u.LicenseNumber == licenseNumber)
                            .FirstOrDefault();
    }
}
