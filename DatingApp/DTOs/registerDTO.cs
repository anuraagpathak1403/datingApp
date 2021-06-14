using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.DTOs
{
    public class registerDTO
    {
        [Required] public string username { get; set; }
        [Required] public DateTime DateOfBirth { get; set; }
        [Required] public string knownAs { get; set; }
        [Required] public string gender { get; set; }
        [Required] public string city { get; set; }
        [Required] public string country { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string password { get; set; }
    }
}
