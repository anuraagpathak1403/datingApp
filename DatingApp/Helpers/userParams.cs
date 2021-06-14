using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Helpers
{
    public class userParams
    {
        private const int maxPageSize = 50;
        public int pageNumber { get; set; }
        private int _pageSize = 10;
        public int pageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
        public string CurrentUsername { get; set; }
        public string Gender { get; set; }
    }
}
