using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BL_;
using AutoMapper;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Entities;
using System.Linq;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;

namespace MyFirstWebProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        IuserBL _iuserBl;
        IMapper _mapper;
        IConfiguration _configuration;
        ILogger _logger;
        public LoginController(IuserBL iuserBl, IMapper mapper, IConfiguration configuration,ILogger<LoginController> logger)
        {
            _iuserBl = iuserBl;
            _mapper = mapper;
            _configuration = configuration;
            _logger= logger;
            _logger.LogInformation("LoginController is up\n");
        }
        private string GenerateSecretKey()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var secretKey = new string(Enumerable.Repeat(chars, 32)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return secretKey;
        }
        [AllowAnonymous]
        [HttpGet("{id}/{name}")]
        public async Task<ActionResult<UserDTO>> Get(string name, int id)
        {

            UserTbl user = await _iuserBl.getUser(name, id);
            if (user == null)
            {
                _logger.LogError("no user in DB \n");
                return NotFound();
                
            }
            UserDTO userDTO = new UserDTO();
            userDTO.UserName = name;
            userDTO.IdToken = user.Id;
            
            string token = CreateToken(user);

            return Ok(token);
        }

        private string CreateToken(UserTbl user)
        {
            List<Claim> claims = new List<Claim> {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim("sub", user.Id.ToString())
        //new Claim(ClaimTypes.Role, "User"),
         };

            //// Generate a secure key

            var key = _configuration.GetSection("AppSettings:TokenKey").Value;
          
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var creds = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }



}
