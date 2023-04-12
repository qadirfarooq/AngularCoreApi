using System.Net.Mime;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Entities;
using System.Security.Cryptography;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if(await context.Users.AnyAsync()) return;
            var UserData = await File.ReadAllTextAsync("Data/UserSeedData.json");

            var option = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
            try{
            var userobj =  JsonSerializer.Deserialize<List<AppUser>>(UserData);
            foreach (var (user, hmac) in from user in userobj
                                         let hmac = new HMACSHA512()
                                         select (user, hmac))
            {
                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Password"));
                user.PasswordSalt = hmac.Key;
                context.Users.Add(user);
            }

            await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
        }
    }
}