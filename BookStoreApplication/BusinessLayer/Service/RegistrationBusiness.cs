using BusinessLayer.Inteface;
using CommonLayer.Models;
using RepoLayer.Context.Models;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class RegistrationBusiness : IRegistrationBusiness
    {
        private readonly IRegistrationRepo registrationRepo;
        public RegistrationBusiness(IRegistrationRepo registrationRepo)
        {
            this.registrationRepo = registrationRepo;
        }
        public async Task<RegistrationTable> Registration(RegistrationModel model)
        {
            try
            {
                return await registrationRepo.Registration(model);
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
                return await registrationRepo.Login(login);
            }
            catch(Exception)
            {
                throw new Exception("Login failed");
            }
        }
        public async Task<string> ForgotPassword(string email)
        {
            try
            {
                return await registrationRepo.ForgotPassword(email);
            }
            catch (Exception)
            {
                throw new Exception("Forgot Password unable to access");
            }
        }
        public async Task<bool> ResetPassword(string email, string password, string confirmPassword)
        {
            try
            {
                return await registrationRepo.ResetPassword(email, password, confirmPassword);
            }
            catch (Exception)
            {
                throw new Exception("Forgot Password unable to access");
            }
        }
    }
}
