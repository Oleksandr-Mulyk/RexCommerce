namespace RexCommerce.RepositoryLibrary.Tests
{
    public class TestRepository1(TestDbContext testDbContext) :
        DbContextRepository<ITestModel1, TestModel1>(testDbContext.TestDbSet1, testDbContext),
        IRepository<ITestModel1>
    {
    }
}
