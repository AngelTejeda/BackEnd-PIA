using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    class JwtParamehtersModel
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
    }
}
