using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyGarageFinderServer.Models;

[Table("AppointmentStatus")]
public partial class AppointmentStatus
{
    [Key]
    [Column("StatusID")]
    public int StatusId { get; set; }

    [StringLength(20)]
    public string? StatusName { get; set; }

    [InverseProperty("Status")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
