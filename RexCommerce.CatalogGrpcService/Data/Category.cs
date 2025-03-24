namespace RexCommerce.CatalogGrpcService.Data
{
    public class Category : ICategory
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        private IList<Product> _products = [];

        public IList<IProduct> Products
        {
            get => (IList<IProduct>)_products;
            set => _products = [.. value.Cast<Product>()];
        }
    }
}
