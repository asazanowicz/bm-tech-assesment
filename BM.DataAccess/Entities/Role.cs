using System;
using System.ComponentModel.DataAnnotations;

namespace BM.DataAccess.Entities
{
    public class Role
    {
        [Key]
        public Guid RoleId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
