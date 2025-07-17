
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;

namespace ModernEstate.DAL.Entites
{
    [Table("service")]
    [Index(nameof(Title), Name = "IX_Service_Title")]
    [Index(nameof(ProvideId), Name = "IX_Service_ProvideId")]
    [Index(nameof(TypeService), Name = "IX_Service_TypeService")]
    [Index(nameof(Status), Name = "IX_Service_Status")]
    [Index(nameof(Id), Name = "IX_Service_Id")]
    public class Service : BaseEntity
    {
        [Key]
        [Column("service_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("title", TypeName = "varchar(50)")]
        [Required]
        public string? Title { get; set; }

        [Column("price", TypeName = "double")]
        [Required]
        public double Price { get; set; }

        [Column("duration_day", TypeName = "int")]
        [Required]
        public int DurationDay { get; set; }

        [Column("is_system", TypeName = "bit")]
        [Required]
        public bool IsSystem { get; set; }

        [Column("type_service", TypeName = "varchar(300)")]
        [Required]
        [EnumDataType(typeof(EnumTypeService))]
        public EnumTypeService TypeService { get; set; }

        [Column("staus", TypeName = "NVARCHAR(200)")]
        [Required]
        [EnumDataType(typeof(EnumStatus))]
        public EnumStatus Status { get; set; }

        [Column("provide_id")]
        // [Required]
        public Guid? ProvideId { get; set; }
        [ForeignKey(nameof(ProvideId))]
        public Provide? Provide { get; set; }

        public virtual ICollection<AccountBuyService>? AccountServices { get; set; }

    }
}