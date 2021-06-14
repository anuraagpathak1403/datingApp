﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Helpers
{
    public class pageList<T> : List<T>
    {
        public pageList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            currentPage = pageNumber;
            totalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.pageSize = pageSize;
            totalCount = count;
            AddRange(items);
        }

        public int currentPage { get; set; }
        public int totalPages { get; set; }
        public int pageSize { get; set; }
        public int totalCount { get; set; }
        public static async Task<pageList<T>> createAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new pageList<T>(items, count, pageNumber, pageSize);
        }
    }
}
