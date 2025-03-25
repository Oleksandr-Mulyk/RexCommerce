using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;

namespace RexCommerce.RepositoryLibrary.Tests
{
    public class DbContextRepositoryTests
    {
        private readonly TestRepository1 _testRepository1;

        private readonly TestRepository2 _testRepository2;

        private readonly TestDbContext _testDbContext;

        private List<TestModel1> _collection1 = [
            new TestModel1 { Id = Guid.NewGuid(), StringTestField = "Test Value 1" },
            new TestModel1 { Id = Guid.NewGuid(), StringTestField = "Test Value 2" },
            new TestModel1 { Id = Guid.NewGuid(), StringTestField = "Test Filtered Value 1" },
            new TestModel1 { Id = Guid.NewGuid(), StringTestField = "Test Filtered Value 2" }
            ];

        private List<TestModel2> _collection2 = [
            new TestModel2 { Id = 1, StringTestField = "Test Value 1" },
            new TestModel2 { Id = 2, StringTestField = "Test Value 2" },
            new TestModel2 { Id = 3, StringTestField = "Test Filtered Value 1" },
            new TestModel2 { Id = 4, StringTestField = "Test Filtered Value 2" }
            ];

        public DbContextRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _testDbContext = new TestDbContext(options);

            FillDbContext();

            _testRepository1 = new TestRepository1(_testDbContext);
            _testRepository2 = new TestRepository2(_testDbContext);
        }

        [Fact]
        public void GetAll_ReturnsAllEntities()
        {
            // Act
            var result1 = _testRepository1.GetAll();
            var result2 = _testRepository2.GetAll();

            // Assert
            Assert.Equal(_testDbContext.TestDbSet1.Count(), result1.Count());
            Assert.Equal(_testDbContext.TestDbSet2.Count(), result2.Count());
        }

        [Fact]
        public void GetAll_ReturnsFilteredEntities()
        {
            // Arrange
            var filteredEntities1 = _testDbContext.TestDbSet1.Where(e => e.StringTestField.Contains("Filtered"));
            var filteredEntities2 = _testDbContext.TestDbSet2.Where(e => e.StringTestField.Contains("Filtered"));

            // Act
            var result1 = _testRepository1.GetAll([e => e.StringTestField.Contains("Filtered")]);
            var result2 = _testRepository2.GetAll([e => e.StringTestField.Contains("Filtered")]);

            // Assert
            Assert.Equal(filteredEntities1.Count(), result1.Count());
            Assert.Equal(filteredEntities2.Count(), result2.Count());
        }

        [Fact]
        public void GetAll_ReturnsSortedEntities()
        {
            // Arrange
            var sortedEntities1 = _testDbContext.TestDbSet1.OrderByDescending(e => e.StringTestField);
            var sortedEntities2 = _testDbContext.TestDbSet2.OrderByDescending(e => e.StringTestField);

            // Act
            var result1 = _testRepository1.GetAll(e => e.StringTestField, ListSortDirection.Descending);
            var result2 = _testRepository2.GetAll(e => e.StringTestField, ListSortDirection.Descending);

            // Assert
            Assert.Equal(sortedEntities1.First(), result1.First());
            Assert.Equal(sortedEntities1.Last(), result1.Last());
            Assert.Equal(sortedEntities2.First(), result2.First());
            Assert.Equal(sortedEntities2.Last(), result2.Last());
        }

        [Fact]
        public void GetAll_ReturnsFilteredAndSortedEntities()
        {
            // Arrange
            var filteredEntities1 = _testDbContext.TestDbSet1
                .Where(e => e.StringTestField.Contains("Filtered"))
                .OrderByDescending(e => e.StringTestField);
            var filteredEntities2 = _testDbContext.TestDbSet2
                .Where(e => e.StringTestField.Contains("Filtered"))
                .OrderByDescending(e => e.StringTestField);

            // Act
            var result1 = _testRepository1.GetAll(
                [e => e.StringTestField.Contains("Filtered")],
                e => e.StringTestField,
                ListSortDirection.Descending
                );
            var result2 = _testRepository2.GetAll(
                [e => e.StringTestField.Contains("Filtered")],
                e => e.StringTestField,
                ListSortDirection.Descending
                );

            // Assert
            Assert.Equal(filteredEntities1.Count(), result1.Count());
            Assert.Equal(filteredEntities1.First(), result1.First());
            Assert.Equal(filteredEntities1.Last(), result1.Last());

            Assert.Equal(filteredEntities2.Count(), result2.Count());
            Assert.Equal(filteredEntities2.First(), result2.First());
            Assert.Equal(filteredEntities2.Last(), result2.Last());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsEntity()
        {
            // Arrange
            var entity1 = _testDbContext.TestDbSet1.FirstOrDefault();
            var entity2 = _testDbContext.TestDbSet2.FirstOrDefault();

            // Act
            var result1 = await _testRepository1.GetByIdAsync(entity1.Id);
            var result2 = await _testRepository2.GetByIdAsync(entity2.Id);

            // Assert
            Assert.Equal(entity1, result1);
            Assert.Equal(entity2, result2);
        }

        [Fact]
        public async Task CreateAsync_AddsEntity()
        {
            // Arrange
            var entity1 = new TestModel1 { Id = Guid.NewGuid(), StringTestField = "TestCreated" };
            var entity2 = new TestModel2 { Id = 999, StringTestField = "TestCreated" };

            // Act
            await _testRepository1.CreateAsync(entity1);
            await _testRepository2.CreateAsync(entity2);

            // Assert
            var newEntity1 = _testDbContext.TestDbSet1.Where(e => e.Id == entity1.Id).FirstOrDefault();
            var newEntity2 = _testDbContext.TestDbSet2.Where(e => e.Id == entity2.Id).FirstOrDefault();

            Assert.Equal(newEntity1, entity1);
            Assert.Equal(newEntity2, entity2);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesEntity()
        {
            // Arrange
            var entity1 = _testDbContext.TestDbSet1.FirstOrDefault();
            entity1!.StringTestField = "Updated Test Field";

            var entity2 = _testDbContext.TestDbSet2.FirstOrDefault();
            entity2!.StringTestField = "Updated Test Field";

            // Act
            await _testRepository1.UpdateAsync(entity1);
            await _testRepository2.UpdateAsync(entity2);

            // Assert
            var updatedEntity1 = _testDbContext.TestDbSet1.Where(e => e.Id == entity1.Id).FirstOrDefault();
            var updatedEntity2 = _testDbContext.TestDbSet2.Where(e => e.Id == entity2.Id).FirstOrDefault();

            Assert.Equal(updatedEntity1, entity1);
            Assert.Equal(updatedEntity2, entity2);
        }

        [Fact]
        public async Task DeleteAsync_DeletesEntity()
        {
            // Arrange
            var entity1 = _testDbContext.TestDbSet1.FirstOrDefault();
            var entity2 = _testDbContext.TestDbSet2.FirstOrDefault();

            // Act
            await _testRepository1.DeleteAsync(entity1);
            await _testRepository2.DeleteAsync(entity2);

            // Assert
            var deletedEntity1 = _testDbContext.TestDbSet1.Where(e => e.Id == entity1.Id).FirstOrDefault();
            var deletedEntity2 = _testDbContext.TestDbSet2.Where(e => e.Id == entity2.Id).FirstOrDefault();

            Assert.Equal(deletedEntity1, null);
            Assert.Equal(deletedEntity2, null);
        }

        private void FillDbContext()
        {
            if (_testDbContext.TestDbSet1.IsNullOrEmpty())
            {
                foreach (var item in _collection1)
                {
                    _testDbContext.TestDbSet1.Add(item);
                }
            }

            if (_testDbContext.TestDbSet1.IsNullOrEmpty())
            {
                foreach (var item in _collection2)
                {
                    _testDbContext.TestDbSet2.Add(item);
                }
            }

            _testDbContext.SaveChanges();
        }
    }
}
