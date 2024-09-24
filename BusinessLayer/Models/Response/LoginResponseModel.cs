using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Response
{
    public class LoginResponseModel
    {
        public TokenModel Token { get; set; }
        public UserResponseModel User { get; set; }
    }
}
