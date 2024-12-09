using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Winecellar.Domain.Models
{
    [Table("wines")]
    public class Wine
    {
        [Key]
        [Column("id", TypeName = "UUID", Order = 1)]
        public Guid Id { get; set; }

        [Required]
        [Column("name", TypeName = "VARCHAR(36)")]
        public string Name { get; set; } = null!;

        public static void Configure(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Wine>()
                .HasIndex(x => x.Name);
        }
    }
}
