﻿using CommonLayer.Models;
using RepoLayer.Context.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Inteface
{
    public interface IRegistrationBusiness
    {
        public Task<RegistrationTable> Registration(RegistrationModel model);
        public Task<LoginData> Login(Login login);
    }
}