using Moq;
using StorageManagement.Application.Services;
using StorageManagement.Domain.Interfaces;
using StorageManagement.Domain.Models;

namespace StorageManagement.Test.Application;

public class InventoryServiceTests
{
    private readonly Mock<IInventoryRepository> _inventoryRepositoryMock;
    private readonly InventoryService _inventoryService;

    public InventoryServiceTests()
    {
        _inventoryRepositoryMock = new Mock<IInventoryRepository>();
        _inventoryService = new InventoryService(_inventoryRepositoryMock.Object);
    }

    [Fact]
    public void AddInventoryItem_ShouldCallAddMethod()
    {
        // Arrange
        var item = new InventoryItem();

        // Act
        _inventoryService.AddInventoryItem(item);

        // Assert
        _inventoryRepositoryMock.Verify(r => r.Add(item), Times.Once);
    }

    [Fact]
    public void DeleteInventoryItem_ShouldCallDeleteMethod()
    {
        // Arrange
        var itemId = 1;

        // Act
        _inventoryService.DeleteInventoryItem(itemId);

        // Assert
        _inventoryRepositoryMock.Verify(r => r.Delete(itemId), Times.Once);
    }

    [Fact]
    public void GetAllInventoryItems_ShouldReturnAllItems()
    {
        // Arrange
        var items = new List<InventoryItem> { new InventoryItem(), new InventoryItem() };
        _inventoryRepositoryMock.Setup(r => r.GetAll()).Returns(items);

        // Act
        var result = _inventoryService.GetAllInventoryItems();

        // Assert
        Assert.Equal(items, result);
    }

    [Fact]
    public void GetInventoryItem_ShouldReturnCorrectItem()
    {
        // Arrange
        var itemId = 1;
        var item = new InventoryItem();
        _inventoryRepositoryMock.Setup(r => r.GetById(itemId)).Returns(item);

        // Act
        var result = _inventoryService.GetInventoryItem(itemId);

        // Assert
        Assert.Equal(item, result);
    }

    [Fact]
    public void UpdateInventoryItemQuantity_ShouldUpdateItemQuantity()
    {
        // Arrange
        var itemId = 1;
        var amount = 10;
        var item = new InventoryItem();
        _inventoryRepositoryMock.Setup(r => r.GetById(itemId)).Returns(item);

        // Act
        _inventoryService.UpdateInventoryItemQuantity(itemId, amount);

        // Assert
        _inventoryRepositoryMock.Verify(r => r.GetById(itemId), Times.Once);
        _inventoryRepositoryMock.Verify(r => r.Update(item), Times.Once);
        Assert.Equal(amount, item.Quantity); // Assuming InventoryItem has a Quantity property
    }
}
