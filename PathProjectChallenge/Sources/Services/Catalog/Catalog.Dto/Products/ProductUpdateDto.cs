namespace Catalog.Dto.Products
{
    public class ProductUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public bool UnitsInStock { get; set; }
        public int Quantity { get; set; }
        public string PictureFileName { get; set; }
        public string PictureUri { get; set; }
        public int CategoryId { get; set; }
    }
}
