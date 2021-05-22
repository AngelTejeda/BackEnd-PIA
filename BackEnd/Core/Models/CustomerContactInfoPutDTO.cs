using System.ComponentModel.DataAnnotations;
using Core.DataAccess;

namespace Core.Models
{
    public class CustomerContactInfoPutDTO : IUpdatable<Customer>
    {
        [Required]
        [StringLength(40)]
        public string Company { get; set; }

        [StringLength(30)]
        public string ContactFullName { get; set; }

        [StringLength(30)]
        public string ContactPosition { get; set; }

        [StringLength(24)]
        public string ContactPhone { get; set; }


        public Customer GetDataBaseObject()
        {
            return new Customer()
            {
                CompanyName = Company,
                ContactName = ContactFullName,
                ContactTitle = ContactPosition,
                Phone = ContactPhone
            };
        }

        public void ModifyDataBaseObject(Customer dataBaseObject)
        {
            dataBaseObject.CompanyName = Company;
            dataBaseObject.ContactName = ContactFullName;
            dataBaseObject.ContactTitle = ContactPosition;
            dataBaseObject.Phone = ContactPhone;
        }
    }
}
