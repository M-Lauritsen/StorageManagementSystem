using StorageManagement.Domain.Models;

namespace StorageManagement.Test.Domain;

public class InventoryItemTests
{
    [Fact]
    public void UpdateQuantity_ShouldIncreaseQuantity()
    {
        // Arrange
        var item = new InventoryItem
        {
            Quantity = 10,
        };

        // Act
        item.UpdateQuantity(5);

        // Assert
        Assert.Equal(15, item.Quantity);
    }

    [Fact]
    public void UpdateQuantity_ShouldDecreaseQuantity()
    {
        // Arrange
        var item = new InventoryItem
        {
            Quantity = 10,
        };

        // Act
        item.UpdateQuantity(-5);

        // Assert
        Assert.Equal(5, item.Quantity);
    }

    [Fact]
    public void UpdateQuantity_ShouldNotChangeIfAmountIsZero()
    {
        // Arrange
        var item = new InventoryItem
        {
            Quantity = 10,
        };

        var initialLastUpdated = item.LastUpdated;

        // Act
        item.UpdateQuantity(0);

        // Assert
        Assert.Equal(10, item.Quantity);
    }
}
