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
using AutoMapper;
//using API.Interfaces;
namespace API.Controllers
{
    
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        public ITokenService _tokenService ;
        private readonly IMapper _mapper;
    
        public AccountController(DataContext context, ITokenService tokenService , IMapper mapper)
        {
            this._mapper = mapper;
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
            var user = await _context.Users.Include(p=> p.Photos).SingleOrDefaultAsync(x => x.UserName == login.username);
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
                Token = _tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
                PhotoUrl = user.Photos.FirstOrDefault( x=> x.IsMain)?.Url
            };
 
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDtos>> Register(RegisterDto Reg)
        {
            if (await UserExist(Reg.username))
            {
                return BadRequest("User Exist");
            }

            var user  =  _mapper.Map<AppUser>(Reg);
            using var hmac = new HMACSHA512();
            user.UserName = Reg.username.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Reg.password));
            user.PasswordSalt = hmac.Key;
             
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
           
            return new UserDtos {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user),
                KnownAs = user.KnownAs
               // PhotoUrl = user.Photos.FirstOrDefault( x=> x.IsMain)?.Url
            };
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}