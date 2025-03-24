namespace RexCommerce.CatalogGrpcService.Data
{
    public class Product : IProduct
    {
        public Guid Id { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public int Stock { get; set; }

        private IList<Category> _categories = [];

        public IList<ICategory> Categories
        {
            get => (IList<ICategory>)_categories;
            set => _categories = [.. value.Cast<Category>()];
        }
    }
}
