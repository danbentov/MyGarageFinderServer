using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyGarageFinderServer.Models;

[Table("Appointment")]
public partial class Appointment
{
    [Key]
    [Column("AppointmentID")]
    public int AppointmentId { get; set; }

    [Column("GarageID")]
    public int? GarageId { get; set; }

    [Column("VehicleUserID")]
    public int? VehicleUserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AppointmentDate { get; set; }

    [Column("StatusID")]
    public int? StatusId { get; set; }

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [ForeignKey("GarageId")]
    [InverseProperty("Appointments")]
    public virtual Garage? Garage { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("Appointments")]
    public virtual AppointmentStatus? Status { get; set; }

    [ForeignKey("VehicleUserId")]
    [InverseProperty("Appointments")]
    public virtual VehicleUser? VehicleUser { get; set; }
}
