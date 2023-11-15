using CommonLayer.Models;
using RepoLayer.Context.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interface
{
    //Implemented Interfaces for Registration Repo
    public interface IRegistrationRepo
    {
        public Task<RegistrationTable> Registration(RegistrationModel model);
        public Task<LoginData> Login(Login login);
        public Task<string> ForgotPassword(string email);
        public Task<bool> ResetPassword(string email, string password, string confirmPassword);
    }
}
