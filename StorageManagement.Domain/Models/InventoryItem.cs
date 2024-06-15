namespace StorageManagement.Domain.Models;

public class InventoryItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime LastUpdated { get; set; }

    public void UpdateQuantity(int amount)
    {
        Quantity += amount;
        LastUpdated = DateTime.UtcNow;
    }
}
