using System;
using System.ComponentModel.DataAnnotations;

namespace BM.Domain.Models
{
    public class UserModel
    {
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public Guid RoleId { get; set; }
    }
}
