using System;
using System.Collections.Generic;

#nullable disable

namespace Core.DataAccess
{
    public partial class User
    {
        public Guid Userid { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Hashpassword { get; set; }
        public string Salt { get; set; }
    }
}
