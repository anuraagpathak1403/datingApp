﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Helpers
{
    public class paginationHeader
    {
        public paginationHeader(int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            this.currentPage = currentPage;
            this.itemsPerPage = itemsPerPage;
            this.totalItems = totalItems;
            this.totalPages = totalPages;
        }

        public int currentPage { get; set; }
        public int itemsPerPage { get; set; }
        public int totalItems { get; set; }
        public int totalPages { get; set; }
    }
}
