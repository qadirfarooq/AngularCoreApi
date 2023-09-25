using System.Reflection.Metadata.Ecma335;
using System.Net.Http.Headers;
using System.Security.Claims;
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
using API.Services;
using API.Extensions;
namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
        {
        public IUserRepository UserRepository { get; }
        public IPhotoService _photoService { get; }
        // Instead of adding db contet like this we are going to use IuserREpository see example below how to inject it

        // public DataContext _context;
        // public UsersController(DataContext context)
        // {
        //     this._context = context;
        // }
        private readonly IMapper mapper;

        public UsersController(IUserRepository  userRepository, IMapper mapper, IPhotoService  photoService)
        {
            this.mapper = mapper;
            UserRepository = userRepository;
            _photoService =  photoService;
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


        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateUser (MemberUpdateDto memberUpdateDto)
        {
            var username = User.GetUsername();
            var user = await UserRepository.GetUserByUsernameAsync(username);
            if(user ==null) return NotFound();

            mapper.Map(memberUpdateDto, user);

            if( await UserRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to Update");

        }
        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto (IFormFile file)
        {
            var user = await UserRepository.GetUserByUsernameAsync(User.GetUsername());
            if(user == null) return NotFound();
            var result = await _photoService.AddPhotoAsync(file);
            if(result.Error != null) return BadRequest(result.Error.Message);
            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };
            if(user.Photos.Count == 0) photo.IsMain = true;
            user.Photos.Add(photo);
            if(await UserRepository.SaveAllAsync()) 
            {
                // this will return 200 OK  below is how 201 got genrated and sent back return mapper.Map<PhotoDto>(photo);
                return CreatedAtAction(nameof(GetUser),new {username =  user.UserName}, mapper.Map<PhotoDto>(photo));
            }
            return  BadRequest("Problem adding Photo");

        }

        // [Authorize]
        [HttpPut("set-main-photo/{photoId}")]

        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var user = await UserRepository.GetUserByUsernameAsync(User.GetUsername());
            if(user == null) return NotFound();
            var photo  = user.Photos.FirstOrDefault(x=> x.Id == photoId);
            if(photo == null) return NotFound();
            if(photo.IsMain) return BadRequest("this is already your main photo");
            var currentmain = user.Photos.FirstOrDefault( x=> x.IsMain);
            if(currentmain != null) currentmain.IsMain = false;
            photo.IsMain =  true;
            if(await UserRepository.SaveAllAsync()) return NoContent();
            return BadRequest ("Problem Setting the main Phto");

        }
        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto (int photoId)
        {
            var user = await UserRepository.GetUserByUsernameAsync(User.GetUsername());
            if(user == null) return NotFound();
            var photo  = user.Photos.FirstOrDefault(x=> x.Id == photoId);
            if(photo == null) return NotFound();
            if(photo.IsMain) return BadRequest("this is your main photo please choose another photo");
            if(photo.PublicId == null) return BadRequest("photo has no public id");
            if(photo.PublicId != null)
            {
                var result  =  await _photoService.DeletePhotoAsync(photo.PublicId);
                if(result.Error != null ) return BadRequest(result.Error.Message);

            }
            user.Photos.Remove(photo);

             
            if(await UserRepository.SaveAllAsync()) return Ok();

            return BadRequest("problem deleteing phto");
        }


    }
}