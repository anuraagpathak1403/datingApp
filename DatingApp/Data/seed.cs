using DatingApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DatingApp.Data
{
    public class seed
    {
        public static async Task seedUsers(dataContext context)
        {
            if (await context.appUsers.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/userSeedData.json");
            var users = JsonSerializer.Deserialize<List<appUser>>(userData);
            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();

                user.username = user.username.ToLower();
                user.password = hmac.ComputeHash(Encoding.UTF8.GetBytes("password"));
                user.passwordSalt = hmac.Key;

                context.appUsers.Add(user);
            }
            await context.SaveChangesAsync();
        }
    }
}
