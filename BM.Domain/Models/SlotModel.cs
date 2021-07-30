using System;
using System.ComponentModel.DataAnnotations;

namespace BM.Domain.Models
{
    public class SlotModel
    {
        public Guid SlotId { get; set; }

        [Required]
        public DateTime DateStart { get; set; }

        [Required]
        public DateTime DateEnd { get; set; }

        public UserModel User { get; set; }
    }
}
