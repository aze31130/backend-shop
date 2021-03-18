using backend_shop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using backend_shop.Interfaces;
using AutoMapper;
using backend_shop.Utils;
using backend_shop.DTO;

namespace backend_shop.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService userService;
        private IMapper mapper;
        public IConfiguration Configuration;

        public UsersController(IUserService _userService, IMapper _mapper, IConfiguration _configuration)
        {
            userService = _userService;
            mapper = _mapper;
            Configuration = _configuration;
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] Authenticate model)
        {
            var user = userService.Authenticate(model.username, model.password);

            if (user == null)
            {
                return BadRequest(new { message = "Invalid credentials" });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration["Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(new
            {
                id = user.id,
                username = user.username,
                firstname = user.firstname,
                lastname = user.lastname,
                age = user.age,
                token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            var user = mapper.Map<User>(model);
            try
            {
                userService.Create(user, model.password);
                return Ok();
            }
            catch (ExceptionRaiser ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = userService.GetAll();
            var model = mapper.Map<IList<UserModel>>(users);
            return Ok(model);
        }

        [HttpGet("id")]
        public IActionResult GetUserById(int id)
        {
            var user = userService.GetById(id);
            var model = mapper.Map<UserModel>(user);
            return Ok(model);
        }

        [HttpPut("id")]
        public IActionResult UpdateUser(int id, [FromBody] UpdateModel model)
        {
            int loggedUserId = int.Parse(User.Identity.Name);
            var user = mapper.Map<User>(model);
            user.id = id;

            if (!loggedUserId.Equals(id))
            {
                return BadRequest(new {message = "Access Denied !");
            }

            try
            {
                userService.Update(user, model.currentpassword, model.newpassword, model.confirmednewpassword);
                return Ok();
            } catch (ExceptionRaiser e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            userService.Delete(id);
            return Ok();
        }
    }
}
