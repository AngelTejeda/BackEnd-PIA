using Core.DataAccess;

namespace Core.Models
{
    public class ProductBasicInfoDTO : IReadable<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDiscontinued { get; set; }
        public decimal? Price { get; set; }

        public ProductBasicInfoDTO()
        {

        }

        public ProductBasicInfoDTO(Product dataBaseProduct)
        {
            CopyInfoFromDataBaseObject(dataBaseProduct);
        }

        public void CopyInfoFromDataBaseObject(Product dataBaseObject)
        {
            Id = dataBaseObject.ProductId;
            Name = dataBaseObject.ProductName;
            IsDiscontinued = dataBaseObject.Discontinued;
            Price = dataBaseObject.UnitPrice;
        }
    }
}
