namespace RexCommerce.RepositoryLibrary.Tests
{
    public class TestRepository2(TestDbContext testDbContext) :
        DbContextRepository<ITestModel2, TestModel2, int>(testDbContext.TestDbSet2, testDbContext),
        IRepository<ITestModel2, int>
    {
    }
}
