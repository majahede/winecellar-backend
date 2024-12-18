using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winecellar.Domain.Models
{
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
        [Column("username", TypeName = "VARCHAR(100)")]
        public string Username { get; set; } = null!;

        [Required]
        [Column("password", TypeName = "VARCHAR(60)")]
        public string Password { get; set; } = null!;
    }
}
