using DatingApp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Entities
{
    public class appUser
    {
        public int Id { get; set; }
        public string username { get; set; }
        public byte[] password { get; set; }
        public byte[] passwordSalt { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string knownAs { get; set; }
        public DateTime created { get; set; } = DateTime.Now;
        public DateTime lastActive { get; set; } = DateTime.Now;
        public string gender { get; set; }
        public string introduction { get; set; }
        public string lookingFor { get; set; }
        public string intrests { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public ICollection<photo> photos { get; set; }
    }
}
