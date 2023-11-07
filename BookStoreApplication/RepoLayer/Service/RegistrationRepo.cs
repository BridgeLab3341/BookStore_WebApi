﻿using CommonLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using RepoLayer.Context;
using RepoLayer.Context.Models;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Service
{
    public class RegistrationRepo : IRegistrationRepo
    {
        private readonly IConfiguration _configuration;
        private readonly dbBooksContext _booksContext;
        public RegistrationRepo(IConfiguration configuration, dbBooksContext booksContext)
        {
            this._configuration = configuration;
            this._booksContext = booksContext;
        }
        public async Task<RegistrationTable> Registration(RegistrationModel model)
        {
            try
            {
                RegistrationTable registration = new RegistrationTable();
                registration.TypeofRegister = model.TypeOfRegistration;
                registration.FirstName = model.FirstName;
                registration.LastName = model.LastName;
                registration.PhoneNumber = model.PhoneNumber;
                registration.Email = model.Email;
                registration.Password = model.Password;
                await _booksContext.AddAsync(registration);
                await _booksContext.SaveChangesAsync();
                if (registration != null)
                {
                    return registration;
                }
                return null;
            }
            catch (Exception)
            {
                throw new Exception("Registration failed");
            }
        }
        public async Task<LoginData> Login(Login login)
        {
            try
            {
                RegistrationTable registration = new RegistrationTable();
                registration = await _booksContext.RegistrationTable.FirstOrDefaultAsync(x => x.Email == login.Email);
                var email=login.Email;
                if(registration != null)
                {
                    var token=GenerateJwtToken(registration.RegisterId, registration.Email);
                    LoginData loginData = new LoginData
                    {
                        Token = token,
                        Register = registration,
                    };
                    return loginData;
                }
                else
                {
                    return null;
                }

            }
            catch(Exception) 
            {
                throw new Exception("Login Failed");
            }
        }
        public string GenerateJwtToken(long id,string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtConfig:Secret"]);

            var tokenDscrption = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("RegistrationId", id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDscrption);
            return tokenHandler.WriteToken(token);
        }
        public async Task<string> ForgotPassword(string email)
        {
            try
            {
                var entity = await _booksContext.RegistrationTable.Where(x => x.Email == email).FirstOrDefaultAsync();
                var useremail = entity.Email;
                var id = entity.RegisterId;

                if (entity != null)
                {
                    var token = GenerateJwtToken(id, email);
                    MsmqModel msmql = new MsmqModel();
                    msmql.SendData2Queue(token);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> ResetPassword(string email, string password, string confirmPassword)
        {
            try
            {
                var reset = await _booksContext.RegistrationTable.Where(x => x.Email == email).FirstOrDefaultAsync();
                if (reset != null && password == confirmPassword)
                {
                    //reset.Password = EncryptPassword(password);
                    await _booksContext.SaveChangesAsync();
                    return reset.Password;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
