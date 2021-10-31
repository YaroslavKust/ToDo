﻿using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;
using ToDo.DataAccess.Interfaces;
using ToDo.Entities.DTO;
using ToDo.Entities.Models;

namespace ToDo.API.Authentication
{
    public class AuthenticationManager: IAuthenticationManager
    {
        private User _user;
        private IUnitOfWork _unit;
        private IConfiguration _config;
        private IMapper _mapper;


        public AuthenticationManager(IUnitOfWork unit, IConfiguration config, IMapper mapper)
        {
            _unit = unit;
            _config = config;
            _mapper = mapper;
        }


        public async Task<bool> ValidateUser(UserForAuth user)
        {
            _user = await _unit.Users.GetUserAsync(user.Name, user.Password);
            return _user != null;
        }


        public string GenerateToken()
        {
            var credentials = GetCredentials();
            var claims = GetClaims();

            var token = new JwtSecurityToken(
                _config["JwtSettings:issuer"],
                _config["JwtSettings:audience"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<bool> CreateUser(UserForAuth user)
        {
            var existedUser = await _unit.Users.GetUserAsync(user.Name, user.Password);

            if (existedUser != null)
            {
                return false;
            }

            var createUser = _mapper.Map<User>(user);

            _unit.Users.Create(createUser);
            await _unit.SaveAsync();

            return true;
        }


        private SigningCredentials GetCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_config["JwtSettings:key"]);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }


        private List<Claim> GetClaims()
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, _user.Id.ToString()) };
            return claims;
        }
    }
}