using RexCommerce.RepositoryLibrary;

namespace RexCommerce.CatalogGrpcService.Data
{
    public class ProductRepository(ApplicationDbContext applicationDbContext) :
        DbContextRepository<IProduct, Product>(applicationDbContext.Products, applicationDbContext),
        IRepository<IProduct>
    {
    }
}
