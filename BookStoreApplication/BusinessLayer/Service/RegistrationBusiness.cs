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
    }
}
