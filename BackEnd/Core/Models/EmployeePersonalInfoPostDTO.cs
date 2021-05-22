using System.ComponentModel.DataAnnotations;
using Core.DataAccess;

namespace Core.Models
{
    public class EmployeePersonalInfoPostDTO : IAddible<Employee>
    {
        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string FamilyName { get; set; }

        [StringLength(60)]
        public string HomeAddress { get; set; }


        public Employee GetDataBaseObject()
        {
            return new Employee()
            {
                FirstName = Name,
                LastName = FamilyName,
                Address = HomeAddress
            };
        }
    }
}
