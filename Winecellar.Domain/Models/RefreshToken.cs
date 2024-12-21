using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Winecellar.Domain.Models
{
    [Table("refresh_tokens")]
    public class RefreshToken
    {
        [Key]
        [Column("id", TypeName = "UUID", Order = 1)]
        public Guid Id { get; set; }

        [Column("user_id", TypeName = "UUID")]
        [Required]
        public Guid UserId { get; private set; }

        [Column("token", TypeName = "TEXT")]
        [Required]
        public string Token { get; set; } = null!;

        [Column("set_at")]
        public DateTime? SetAt { get; set; } = DateTime.UtcNow;

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshToken>()
                .HasIndex(rf => rf.UserId);

            modelBuilder.Entity<RefreshToken>()
            .Property(u => u.SetAt)
            .HasDefaultValueSql("NOW()");

            modelBuilder.Entity<User>()
                .HasMany<RefreshToken>();
        }
    }
}
