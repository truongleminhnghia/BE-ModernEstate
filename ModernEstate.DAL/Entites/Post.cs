
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;

namespace ModernEstate.DAL.Entites
{
    [Table("post")]
    [Index(nameof(Code), IsUnique = true)]
    [Index(nameof(SourceStatus))]
    public class Post : BaseEntity
    {
        [Key]
        [Column("post_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("code", TypeName = "varchar(150)")]
        [Required]
        public string? Code { get; set; }

        [Column("approve_by", TypeName = "varchar(400)")]
        public string? AppRovedBy { get; set; }

        [Column("post_by", TypeName = "varchar(400)")]
        [Required]
        public string PostBy { get; set; } = string.Empty;

        [Column("demand", TypeName = "varchar(300)")]
        [Required]
        [Description("State of the post.")]
        [EnumDataType(typeof(EnumDemand))]
        public EnumDemand Demand { get; set; }

        [Column("source_status", TypeName = "varchar(300)")]
        [Required]
        [Description("Source status of the post.")]
        [EnumDataType(typeof(EnumSourceStatus))]
        public EnumSourceStatus SourceStatus { get; set; }

        [Column("rejection_reason", TypeName = "text")]
        [Description("Reason for rejection, if applicable.")]
        public string? RejectionReason { get; set; }

        [Column("status", TypeName = "varchar(200)")]
        [Required]
        [Description("Status of the post (ACTIVE, INACTIVE).")]
        [EnumDataType(typeof(EnumStatus))]
        public EnumStatus Status { get; set; }

        [Column("property_id")]
        [Required]
        [Description("ID of the property associated with the post.")]
        public Guid PropertyId { get; set; }

        [ForeignKey(nameof(PropertyId))]
        [Description("Navigation property for the associated property.")]
        public Property? Property { get; set; }

        [Column("contact_id")]
        [Required]
        [Description("ID of the contact associated with the post.")]
        public Guid ContactId { get; set; }

        [ForeignKey(nameof(ContactId))]
        [Description("Navigation property for the associated contact.")]
        public Contact? Contact { get; set; }

        public virtual ICollection<PostPackage>? PostPackages { get; set; }
        public virtual ICollection<History>? Histories { get; set; }
    }
}