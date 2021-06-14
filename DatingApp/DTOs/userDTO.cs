using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.DTOs
{
    public class userDTO
    {
        public string username { get; set; }
        public string token { get; set; }
        public string photoUrl { get; set; }
        public string knownAs { get; set; }
    }
}
