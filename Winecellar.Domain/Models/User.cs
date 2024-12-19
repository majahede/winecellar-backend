using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace Winecellar.Domain.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id", TypeName = "UUID", Order = 1)]
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        [Column("email", TypeName = "VARCHAR(100)")]
        public string Email { get; set; } = null!;

        [Required]
        [Column("username", TypeName = "VARCHAR(50)")]
        public string Username { get; set; } = null!;

        [Required]
        [Column("password", TypeName = "VARCHAR(60)")]
        public string Password { get; set; } = null!;

        public static void Configure(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        }
    }
}