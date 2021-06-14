using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.DTOs
{
    public class memberDTO
    {
        public int Id { get; set; }
        public string username { get; set; }
        public string photoUrl { get; set; }
        public int age { get; set; }
        public string knownAs { get; set; }
        public DateTime created { get; set; }
        public DateTime lastActive { get; set; }
        public string gender { get; set; }
        public string introduction { get; set; }
        public string lookingFor { get; set; }
        public string intrests { get; set; }
        public string city { get; set; }
        public string country { get; set; }

        public ICollection<photoDTO> photos { get; set; }
    }
}
