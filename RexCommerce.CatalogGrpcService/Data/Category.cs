namespace RexCommerce.CatalogGrpcService.Data
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IList<Product> Products { get; set; }
    }
}
