using AutoMapper;
using DatingApp.Data;
using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Controllers
{
    //[Authorize]
    public class accountController : baseApiController
    {
        public readonly dataContext _context;
        private readonly iTokenService _iTokenService;
        private readonly IMapper _iMapper;

        public accountController(dataContext context, iTokenService iTokenService, IMapper iMapper)
        {
            _context = context;
            _iTokenService = iTokenService;
            _iMapper = iMapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<userDTO>> register(registerDTO registerDTO)
        {
            if (await userExist(registerDTO.username.ToLower())) return BadRequest("UserName already exist !");

            var user = _iMapper.Map<appUser>(registerDTO);

            using var hmac = new HMACSHA512();

            user.username = registerDTO.username;
            user.password = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.password));
            user.passwordSalt = hmac.Key;

            _context.appUsers.Add(user);
            await _context.SaveChangesAsync();
            return new userDTO
            {
                username = user.username,
                token = _iTokenService.createToken(user),
                knownAs = user.knownAs
            };
        }

        public async Task<bool> userExist(string username)
        {
            return await _context.appUsers.AnyAsync(x => x.username.ToLower() == username.ToLower());
        }

        [HttpPost("login")]
        public async Task<ActionResult<userDTO>> login(loginDTO loginDTO)
        {
            var user = await _context.appUsers
                .Include(p => p.photos)
                .SingleOrDefaultAsync(x => x.username.ToLower() == loginDTO.username.ToLower());

            if (user == null) return Unauthorized("Invalid Username");

            using var hmac = new HMACSHA512(user.passwordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.password));

            //for (int i = 0; i < computedHash.Length; i++)
            //    if (computedHash[i] != user.password[i])
            //        return Unauthorized("Invalid password !");

            return new userDTO
            {
                username = user.username,
                token = _iTokenService.createToken(user),
                photoUrl = user.photos.FirstOrDefault(x => x.isMain)?.url,
                knownAs=user.knownAs
            };
        }
    }
}
