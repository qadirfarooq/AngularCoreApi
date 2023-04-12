using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.DTO;
namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsynch();
        Task<AppUser> GetUserByIsAsynch( int id); 
        Task<AppUser> GetUserByUsernameAsync(String username);

        Task <IEnumerable<MemberDto>> GetMembersAsync();
        Task <MemberDto> GetMemberByUserNameAsync(String username);
    }
}