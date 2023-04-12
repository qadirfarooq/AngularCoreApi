using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;
using AutoMapper;
using API.DTO;
namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
        {
        public IUserRepository UserRepository { get; }

        // Instead of adding db contet like this we are going to use IuserREpository see example below how to inject it

        // public DataContext _context;
        // public UsersController(DataContext context)
        // {
        //     this._context = context;
        // }
        private readonly IMapper mapper;

        public UsersController(IUserRepository  userRepository, IMapper mapper)
        {
            this.mapper = mapper;
            UserRepository = userRepository;
        }


        //[AllowAnonymous]
        [HttpGet]
       
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
        //    var users = await UserRepository.GetUsersAsynch();

        //    var usersToReturn = mapper.Map<IEnumerable<MemberDto>>(users);

        //    return Ok(usersToReturn); 

                var users = await UserRepository.GetMembersAsync();
                return Ok(users);
        }
         [Authorize]
        //api/Users/id
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            //var user =  await UserRepository.GetUserByUsernameAsync(username);
            //return Ok(mapper.Map<MemberDto>(user));
            return  await UserRepository.GetMemberByUserNameAsync(username);
        }
    }
}