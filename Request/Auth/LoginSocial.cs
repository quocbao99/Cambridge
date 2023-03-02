using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Request.Auth
{
    public class LoginSocial
    {
        public int SocialType { get; set; }
        public string Token { get; set; }
    }
}
