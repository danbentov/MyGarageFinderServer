using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyGarageFinderServer.Models;

[Table("Garage")]
public partial class Garage
{
    [Key]
    [Column("GarageID")]
    public int GarageId { get; set; }

    public int? GarageNumber { get; set; }

    [StringLength(100)]
    public string? GarageName { get; set; }

    [StringLength(50)]
    public string? TypeCode { get; set; }

    [StringLength(50)]
    public string? GarageType { get; set; }

    [StringLength(70)]
    public string? GarageAddress { get; set; }

    [StringLength(30)]
    public string? City { get; set; }

    [StringLength(50)]
    public string? Phone { get; set; }

    public int? ZipCode { get; set; }

    public int? SpecializationCode { get; set; }

    [StringLength(50)]
    public string? Specialization { get; set; }

    [StringLength(50)]
    public string? GarageManager { get; set; }

    [StringLength(70)]
    public string GarageLicense { get; set; } = null!;

    public DateOnly? TestTime { get; set; }

    [StringLength(50)]
    public string? WorkingHours { get; set; }

    [InverseProperty("Garage")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [InverseProperty("Garage")]
    public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

    [InverseProperty("Garage")]
    public virtual ICollection<GarageImage> GarageImages { get; set; } = new List<GarageImage>();

    [InverseProperty("Garage")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
