﻿using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace VetClinicServer
{
    public static class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; 
        public const string AUDIENCE = "MyAuthClient"; 
        const string KEY = "MyKeuihyugyufgtfrtderdhhuigtydeffty";  
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
