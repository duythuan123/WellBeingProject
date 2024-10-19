﻿using AutoMapper;
using DataAccessLayer.Entities;
using DataAccessLayer.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Utilities
{
    public class Helper
    {
        public static string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string hashedPassword = Convert.ToBase64String(hashBytes);

            return hashedPassword;
        }


        public static string GenerateJwtToken(User user, IConfiguration _configuration)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Fullname)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            byte[] hash = new byte[20];
            Array.Copy(hashBytes, 16, hash, 0, 20);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] computedHash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hash[i] != computedHash[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static decimal ExtractAmountFromOrderDescription(string orderDescription)
        {
            var words = orderDescription.Split(' ');
            if (decimal.TryParse(words.Last(), out decimal amount))
            {
                return amount;
            }
            return 0;
        }

        public static int? ExtractAppointmentIdFromOrderDescription(string orderDescription)
        {
            var words = orderDescription.Split(' ');
            if (int.TryParse(words.First(), out int appointmentId))
            {
                return appointmentId;
            }
            return null;
        }
    }
}
