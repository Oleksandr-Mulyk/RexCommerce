namespace RexCommerce.CatalogLibrary
{
    public interface IProduct
    {
        public string Sku { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public int Stock { get; set; }

        public IList<ICategory> Categories { get; set; }
    }
}
