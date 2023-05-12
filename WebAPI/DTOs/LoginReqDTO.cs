using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs
{
    public class LoginReqDTO
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
}