using DatingApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data
{
    public class dataContext : DbContext
    {
        public dataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<appUser> appUsers { get; set; }
    }
}
