using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SoluWalter.BusinessLogic.Users;
using SoluWalter.Entities.Dtos;
using SoluWalter.Entities.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SoluWalter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly APPSettings _appSettings;
        public UsersController(IUserRepository _repository, IMapper _mapper, IOptions<APPSettings> _appSettings)
        {
            this._repository = _repository;
            this._mapper = _mapper;
            this._appSettings = _appSettings.Value;
        }

        [HttpGet("{id}")]
        //[AllowAnonymous]
        public async Task<ActionResult<User>> Get(string id)
        {
            var user = await _repository.GetById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Post(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user = await _repository.CreateUser(user);
            user.Token = GenerarToken(user);
            return Ok(user);
            //return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Authenticate(UserDto userDto)
        {            
            var user = await _repository.Authenticate(userDto.Username, userDto.Password);
            if (user == null) return BadRequest("Usuario u/o Contraseña incorrecto");
            user.Token = GenerarToken(user);

            return Ok(user);
        }

        private string GenerarToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                          {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Name, user.Username),
                          }),
                Expires = DateTime.UtcNow.AddMinutes(_appSettings.TimeExpirations),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };               
          
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
