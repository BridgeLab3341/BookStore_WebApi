﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class RegistrationModel
    {
        public string TypeOfRegistration { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
