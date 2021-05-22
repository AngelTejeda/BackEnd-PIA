using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess;

namespace Core.Models
{
    public class UserPostDTO : IAddible<User>
    {
        [Required]
        [StringLength(30)]
        public string UserName { get; set; }

        [Required]
        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public User GetDataBaseObject()
        {
            return new()
            {
                Username = UserName,
                Email = Email,
                Hashpassword = Password
            };
        }
    }
}
