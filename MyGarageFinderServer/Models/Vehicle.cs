﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyGarageFinderServer.Models;

[Table("Vehicle")]
public partial class Vehicle
{
    [Key]
    [StringLength(50)]
    public string LicensePlate { get; set; } = null!;

    [StringLength(100)]
    public string Model { get; set; } = null!;

    [StringLength(6)]
    public string VehicleYear { get; set; } = null!;

    [StringLength(50)]
    public string FuelType { get; set; } = null!;

    [StringLength(20)]
    public string? Color { get; set; }

    [StringLength(50)]
    public string Manufacturer { get; set; } = null!;

    public int CurrentMileage { get; set; }

    [Column("ImageURL")]
    [StringLength(200)]
    public string? ImageUrl { get; set; }

    [InverseProperty("Vehicle")]
    public virtual ICollection<VehicleUser> VehicleUsers { get; set; } = new List<VehicleUser>();
}
