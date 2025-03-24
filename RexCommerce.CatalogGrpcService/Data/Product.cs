using System.ComponentModel.DataAnnotations.Schema;

namespace RexCommerce.CatalogGrpcService.Data
{
    public class Product : IProduct
    {
        public Guid Id { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public int Stock { get; set; }

        [Column("Categories")]
        private IList<Category> _categories = [];

        [NotMapped]
        public IList<ICategory> Categories
        {
            get => (IList<ICategory>)_categories;
            set => _categories = [.. value.Cast<Category>()];
        }
    }
}
