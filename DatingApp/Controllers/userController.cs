using AutoMapper;
using DatingApp.Data;
using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Extensions;
using DatingApp.Helpers;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DatingApp.Controllers
{
    public class userController : baseApiController
    {
        public readonly iUserRepository _iUserRepository;
        private readonly IMapper _mapper;
        private readonly iPhotoService _iPhotoService;

        public userController(iUserRepository iUserRepository, IMapper mapper, iPhotoService iPhotoService)
        {
            _iUserRepository = iUserRepository;
            _mapper = mapper;
            _iPhotoService = iPhotoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<memberDTO>>> getUser([FromQuery]userParams userParms)
        {
            var user = await _iUserRepository.getUserByUserNameAsync(User.getUserName());
            userParms.CurrentUsername = user.username;

            if (string.IsNullOrEmpty(userParms.Gender))
                userParms.Gender = user.gender == "male" ? "female" : "male";

            if (userParms.pageNumber == 0)
                userParms.pageNumber = 1;
            var users = await _iUserRepository.getMemberAsync(userParms);

            Response.AddPaginationHeader(users.currentPage, users.pageSize, users.totalCount, users.totalPages);

            return Ok(users);
        }

        [HttpGet("{username}", Name = "getUser")]
        public async Task<ActionResult<memberDTO>> getUserByUserNameAsync(string username)
        {
            var user = await _iUserRepository.getMemberDTOAsync(username);
            return user;
        }

        [HttpPut]
        public async Task<ActionResult> updateUserAsync(memberUpdateDTO memberUpdateDTO)
        {
            var userName = User.getUserName();
            var user = await _iUserRepository.getUserByUserNameAsync(userName);
            _mapper.Map(memberUpdateDTO, user);
            _iUserRepository.update(user);
            if (await _iUserRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user");
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<photoDTO>> addPhoto(IFormFile file)
        {
            var user = await _iUserRepository.getUserByUserNameAsync(User.getUserName());
            var result = await _iPhotoService.AddPhotoAsync(file);
            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new photo
            {
                url = result.SecureUrl.AbsoluteUri,
                publicId = result.PublicId,
            };

            if (user.photos.Count == 0)
            {
                photo.isMain = true;
            }
            user.photos.Add(photo);

            if (await _iUserRepository.SaveAllAsync())
            {
                return CreatedAtRoute("getUser", new { username = user.username }, _mapper.Map<photoDTO>(photo));
            }

            return BadRequest("Problem loading photo");
        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult<photoDTO>> setMainPhoto(int photoId)
        {
            var user = await _iUserRepository.getUserByUserNameAsync(User.getUserName());
            var photo = user.photos.FirstOrDefault(x => x.id == photoId);
            if (photo.isMain) return BadRequest("This is already your main photo!");

            var currentMain = user.photos.FirstOrDefault(x => x.isMain);
            if (currentMain != null) currentMain.isMain = false;
            photo.isMain = true;

            if (await _iUserRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to set main photo");
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult<photoDTO>> deletePhoto(int photoId)
        {
            var user = await _iUserRepository.getUserByUserNameAsync(User.getUserName());
            var photo = user.photos.FirstOrDefault(x => x.id == photoId);
            if (photo == null) return NotFound();
            if (photo.isMain) return BadRequest("You cannot delete your main photo");
            if (photo.publicId != null)
            {
                var result = await _iPhotoService.DeletePhotoAsync(photo.publicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }

            user.photos.Remove(photo);
            if (await _iUserRepository.SaveAllAsync()) return Ok();
            return BadRequest("Failed to delete the photo");
        }
    }
}
