using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyGarageFinderServer.Models;

[Table("GarageImage")]
public partial class GarageImage
{
    [Key]
    [Column("GarageImageID")]
    public int GarageImageId { get; set; }

    [Column("GarageID")]
    public int? GarageId { get; set; }

    [Column("ImageURL")]
    [StringLength(200)]
    public string ImageUrl { get; set; } = null!;

    [ForeignKey("GarageId")]
    [InverseProperty("GarageImages")]
    public virtual Garage? Garage { get; set; }
}
