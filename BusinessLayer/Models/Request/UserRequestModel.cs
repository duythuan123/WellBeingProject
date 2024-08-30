﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Request
{
    public class UserRequestModel
    {
        public string Fullname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phonenumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
    }

    public class UserRequestModelForUpdate
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phonenumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
    }

    public class UserRequestModelForForgotPassword
    {
        public string Email { get; set; }
    }
    public class UserRequestModelForChangePassword
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
