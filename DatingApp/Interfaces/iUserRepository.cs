using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Interfaces
{
    public interface iUserRepository
    {
        void update(appUser appUser);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<appUser>> getUsersAsync();
        Task<appUser> getUserByIdAsync(int id);
        Task<appUser> getUserByUserNameAsync(string userName);
        Task<pageList<memberDTO>> getMemberAsync(userParams userParams);
        Task<memberDTO> getMemberDTOAsync(string userName);
    }
}
