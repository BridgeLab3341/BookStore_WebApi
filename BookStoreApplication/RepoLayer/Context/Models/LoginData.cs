using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Context.Models
{
    public class LoginData
    {
        public string Token { get; set; }
        public RegistrationTable Register { get; set; }
        public string TypeOfRegister { get; set; }  
    }
}
