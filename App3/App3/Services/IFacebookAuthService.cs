﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App3.Services
{
    public interface IFacebookAuthService
    {
        Task<LoginResult> Login();
        void Logout();
    }

    public enum LoginState
    {
        Failed,
        Canceled,
        Success
    }

    public class LoginResult
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTimeOffset ExpireAt { get; set; }
        public LoginState LoginState { get; set; }
        public string ErrorString { get; set; }
    }
}
