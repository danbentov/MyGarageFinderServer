using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyGarageFinderServer.Models;

[Table("Review")]
public partial class Review
{
    [Key]
    [Column("ReviewID")]
    public int ReviewId { get; set; }

    public int Rating { get; set; }

    [Column(TypeName = "text")]
    public string ReviewDescription { get; set; } = null!;

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column("GarageID")]
    public int? GarageId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReviewTimestamp { get; set; }

    [ForeignKey("GarageId")]
    [InverseProperty("Reviews")]
    public virtual Garage? Garage { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Reviews")]
    public virtual User? User { get; set; }
}
