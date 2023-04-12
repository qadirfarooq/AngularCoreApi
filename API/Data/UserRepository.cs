using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public IMapper Mapper { get; }

        public UserRepository( DataContext context, IMapper mapper)
        {
            this.Mapper = mapper;
            _context = context;

        }
        public async Task<AppUser> GetUserByIsAsynch(int id)
        {
            return await _context.Users.Where(a=> a.Id==id).FirstAsync();
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.Where(x => x.UserName == username).Include(p => p.Photos).FirstAsync();
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsynch()
        {
             return await _context.Users.Include(p => p.Photos).ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() >0 ;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
               return await _context.Users.ProjectTo<MemberDto>(Mapper.ConfigurationProvider).ToListAsync();
               // throw new NotFiniteNumberException() ;
        }

        public async Task<MemberDto> GetMemberByUserNameAsync(string username)
        {
            return await _context.Users.Where(u=> u.UserName == username).
            ProjectTo<MemberDto>(Mapper.ConfigurationProvider).SingleAsync();
            
            
        }
    }
}