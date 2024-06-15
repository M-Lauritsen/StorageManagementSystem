using Microsoft.EntityFrameworkCore;
using StorageManagement.Domain.Models;
using StorageManagement.Infrastructure.Data;
using StorageManagement.Infrastructure.Repositories;

namespace StorageManagement.Test.Infrastructure;

public class InventoryRepositoryTests
{
    private readonly DbContextOptions<DataContext> _dbContextOptions;

    public InventoryRepositoryTests()
    {
        // Brug en unik database navn for hver testkørsel for at sikre isolerede testcases
        _dbContextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "InventoryTestDb_" + Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void Add_ShouldAddItemToDatabase()
    {
        using (var context = new DataContext(_dbContextOptions))
        {
            var repository = new InventoryRepository(context);
            var item = new InventoryItem
            {
                Id = 1,
                Name = "Item1",
                Quantity = 10,
                Description = "Description",
                LastUpdated = DateTime.Now,
                Price = 101,
            };

            repository.Add(item);

            var addedItem = context.InventoryItems.Find(1);
            Assert.NotNull(addedItem);
            Assert.Equal("Item1", addedItem.Name);
            Assert.Equal(10, addedItem.Quantity);
        }
    }

    [Fact]
    public void Delete_ShouldRemoveItemFromDatabase()
    {
        using (var context = new DataContext(_dbContextOptions))
        {
            var repository = new InventoryRepository(context);
            var item = new InventoryItem
            {
                Id = 1,
                Name = "Item1",
                Quantity = 10,
                Description = "Description",
                LastUpdated = DateTime.Now,
                Price = 101,
            };
            context.InventoryItems.Add(item);
            context.SaveChanges();

            repository.Delete(1);

            var deletedItem = context.InventoryItems.Find(1);
            Assert.Null(deletedItem);
        }
    }

    [Fact]
    public void GetAll_ShouldReturnAllItems()
    {
        using (var context = new DataContext(_dbContextOptions))
        {
            var repository = new InventoryRepository(context);
            var items = new List<InventoryItem>
            {
                new() {
                    Id = 1,
                    Name = "Item1",
                    Quantity = 10,
                    Description = "Description",
                    LastUpdated = DateTime.Now,
                    Price = 101,
                },
                new() {
                    Id = 2,
                    Name = "Item2",
                    Quantity = 20,
                    Description = "Description for second item",
                    LastUpdated = DateTime.Now,
                    Price = 20,
                }
            };
            context.InventoryItems.AddRange(items);
            context.SaveChanges();

            var result = repository.GetAll();

            Assert.Equal(2, result.Count);
            Assert.Contains(result, i => i.Id == 1 && i.Name == "Item1" && i.Quantity == 10);
            Assert.Contains(result, i => i.Id == 2 && i.Name == "Item2" && i.Quantity == 20);
        }
    }

    [Fact]
    public void GetById_ShouldReturnCorrectItem()
    {
        using (var context = new DataContext(_dbContextOptions))
        {
            var repository = new InventoryRepository(context);
            var item = new InventoryItem
            {
                Id = 1,
                Name = "Item1",
                Quantity = 10,
                Description = "Description",
                LastUpdated = DateTime.Now,
                Price = 101,
            };
            context.InventoryItems.Add(item);
            context.SaveChanges();

            var result = repository.GetById(1);

            Assert.NotNull(result);
            Assert.Equal("Item1", result.Name);
            Assert.Equal(10, result.Quantity);
        }
    }

    [Fact]
    public void Update_ShouldUpdateItemInDatabase()
    {
        using (var context = new DataContext(_dbContextOptions))
        {
            var repository = new InventoryRepository(context);
            var item = new InventoryItem 
            { 
                Id = 1, 
                Name = "Item1", 
                Quantity = 10,
                Description = "Description",
                LastUpdated = DateTime.Now,
                Price = 101,
            };
            context.InventoryItems.Add(item);
            context.SaveChanges();

            item.Name = "UpdatedItem";
            item.Quantity = 15;
            repository.Update(item);

            var updatedItem = context.InventoryItems.Find(1);
            Assert.NotNull(updatedItem);
            Assert.Equal("UpdatedItem", updatedItem.Name);
            Assert.Equal(15, updatedItem.Quantity);
        }
    }
}
