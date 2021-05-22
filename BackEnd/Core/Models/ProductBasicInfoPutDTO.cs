using System.ComponentModel.DataAnnotations;
using Core.DataAccess;

namespace Core.Models
{
    public class ProductBasicInfoPutDTO : IUpdatable<Product>
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        public bool IsDiscontinued { get; set; }

        [Range(0, float.PositiveInfinity)]
        public decimal? Price { get; set; }


        public void ModifyDataBaseObject(Product dataBaseObject)
        {
            dataBaseObject.ProductName = Name;
            dataBaseObject.Discontinued = IsDiscontinued;
            dataBaseObject.UnitPrice = Price;
        }
    }
}
