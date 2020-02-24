using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StarWars.API.Dtos.Authentication;
using StarWars.Core.Interfaces.Authentication;
using StarWars.Core.Models.Authentication;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StarWars.API.Controllers
{
	[Route("auth/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		[HttpPost("register")]
		public async Task<IActionResult> Register(UserRegistrationDto dto)
		{
			if (_authenticationService.IsUserExists(dto.UserName))
			{
				return BadRequest("Username already exists!");
			}

			var userToCreate = new User
			{
				UserName = dto.UserName
			};

			var createdUser = await _authenticationService.RegisterAsync(userToCreate, dto.Password);

			return Created(string.Empty, _mapper.Map<UserInfoDto>(createdUser));
		}

		[HttpPost("login")]
		public IActionResult Login(UserLoginDto dto)
		{
			var user = _authenticationService.Login(dto.UserName, dto.Password);

			if (user == null)
			{
				return Unauthorized();
			}

			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Name, user.UserName)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretToken").Value));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(14),
				SigningCredentials = creds
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return Ok(new
			{
				token = tokenHandler.WriteToken(token)
			});
		}

		public AuthController(IAuthenticationService authenticationService, IConfiguration configuration, IMapper mapper)
		{
			_authenticationService = authenticationService;
			_configuration = configuration;
			_mapper = mapper;
		}

		private readonly IAuthenticationService _authenticationService;
		private readonly IConfiguration _configuration;
		private readonly IMapper _mapper;
	}
}
