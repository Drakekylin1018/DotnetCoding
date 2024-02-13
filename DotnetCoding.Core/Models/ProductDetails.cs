

namespace DotnetCoding.Core.Models
{
    public class ProductDetails
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int ProductPrice { get; set; }
        public int ProductStatusTypeId { get; set; }
        //Adding CreateDate proprity for sorting the products list for latest first.
        public DateTime CreateDate { get; set; }
        //Create look up table for the Product Status to avoid hard coding on the Status.
        public ProductStatus ProductStatusType { get;set; }
    }
}
