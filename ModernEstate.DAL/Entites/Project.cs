
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModernEstate.Common.Enums;

namespace ModernEstate.DAL.Entites
{
    [Table("project")]
    public class Project : BaseEntity
    {
        [Key]
        [Column("project_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("name", TypeName = "varchar(100)")]
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        [DataType(DataType.Text)]
        [StringLength(100)]
        public string? Name { get; set; }

        [Column("investor", TypeName = "varchar(100)")]
        [MaxLength(100)]
        [MinLength(1)]
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Required]
        public string? Investor { get; set; }

        [Column("description", TypeName = "varchar(500)")]
        [MaxLength(500)]
        [MinLength(1)]
        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        public string? Description { get; set; }

        [Column("progress", TypeName = "varchar(100)")]
        [MaxLength(100)]
        [MinLength(1)]
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Required]
        [EnumDataType(typeof(EnumProject))]
        public EnumProject? Progress { get; set; }

        [Column("location_id")]
        [Required]
        public Guid LocationId { get; set; }

        [ForeignKey("LocationId")]
        [InverseProperty("Projects")]
        public Location? Location { get; set; }
        public ICollection<Property>? Properties { get; set; }
        public ICollection<Image>? Images { get; set; }
    }
}