using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BM.DataAccess.Entities
{
    public class Slot
    {
        [Key]
        public Guid SlotId { get; set; }

        [Required]
        public DateTime DateStart { get; set; }

        [Required]
        public DateTime DateEnd { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
