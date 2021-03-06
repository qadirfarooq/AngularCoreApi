using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using API.DTO;
using System.Security.Cryptography;
using System.Text;
using API.Interfaces;
//using API.Interfaces;
namespace API.Controllers
{
    
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        public ITokenService _tokenService ;
    
        public AccountController(DataContext context, ITokenService tokenService )
        {
           _tokenService = tokenService;
            _context = context;
        }
        private async Task<bool> UserExist(string username)
        {
            return await _context.Users.AnyAsync(a => a.UserName == username.ToLower());

        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDtos>> Login(LoginDto login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == login.username);
            if (user == null) return Unauthorized("invalid User");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.password));
            for (int i = 0; i < ComputeHash.Length; i++)
            {
                if (ComputeHash[i] != user.PasswordHash[i])
                    return Unauthorized("Invalid Password");
            }
            return new UserDtos {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
 
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDtos>> Register(RegisterDto Reg)
        {
            if (await UserExist(Reg.username))
            {
                return BadRequest("User Exist");
            }

            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = Reg.username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Reg.password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            //return user;
            return new UserDtos {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}