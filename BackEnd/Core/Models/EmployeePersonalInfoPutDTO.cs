using System.ComponentModel.DataAnnotations;
using Core.DataAccess;

namespace Core.Models
{
    public class EmployeePersonalInfoPutDTO : IUpdatable<Employee>
    {
        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string FamilyName { get; set; }

        [StringLength(60)]
        public string HomeAddress { get; set; }


        public void ModifyDataBaseObject(Employee dataBaseObject)
        {
            dataBaseObject.FirstName = Name;
            dataBaseObject.LastName = FamilyName;
            dataBaseObject.Address = HomeAddress;
        }
    }
}
