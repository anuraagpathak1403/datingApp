using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Helpers;
using DatingApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data
{
    public class userRepository : iUserRepository
    {
        public readonly dataContext _context;
        private readonly IMapper _mapper;

        public userRepository(dataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<pageList<memberDTO>> getMemberAsync(userParams userParms)
        {
            var query = _context.appUsers.AsQueryable();

            query = query.Where(u => u.username != userParms.CurrentUsername);
            query = query.Where(u => u.gender == userParms.Gender);

            return await pageList<memberDTO>.createAsync(query.ProjectTo<memberDTO>(_mapper
                .ConfigurationProvider).AsNoTracking(), userParms.pageNumber, userParms.pageSize);
        }

        public async Task<memberDTO> getMemberDTOAsync(string userName)
        {
            return await _context.appUsers
                .Where(x => x.username == userName)
                .ProjectTo<memberDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<appUser> getUserByIdAsync(int id)
        {
            return await _context.appUsers.FindAsync(id);
        }

        public async Task<appUser> getUserByUserNameAsync(string username)
        {
            return await _context.appUsers
                .Include(p => p.photos)
                .SingleOrDefaultAsync(x => x.username == username);
        }

        public async Task<IEnumerable<appUser>> getUsersAsync()
        {
            return await _context.appUsers
                .Include(p => p.photos)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void update(appUser appUser)
        {
            _context.Entry(appUser).State = EntityState.Modified;
        }
    }
}
