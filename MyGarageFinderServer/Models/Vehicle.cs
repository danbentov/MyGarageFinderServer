using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyGarageFinderServer.Models;

[Table("Vehicle")]
public partial class Vehicle
{
    [Key]
    public int LicensePlate { get; set; }

    [StringLength(100)]
    public string? Model { get; set; }

    public int? VehicleYear { get; set; }

    [StringLength(50)]
    public string? FuelType { get; set; }

    [StringLength(20)]
    public string? Color { get; set; }

    [StringLength(50)]
    public string? Manufacturer { get; set; }

    public int? CurrentMileage { get; set; }

    [Column("ImageURL")]
    [StringLength(200)]
    public string? ImageUrl { get; set; }

    [InverseProperty("Vehicle")]
    public virtual ICollection<VehicleUser> VehicleUsers { get; set; } = new List<VehicleUser>();
}
