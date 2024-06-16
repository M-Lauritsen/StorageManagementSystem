using StorageManagement.Domain.Models;

namespace StorageManagement.Domain.Interfaces;
public interface IInventoryService
{
    void AddInventoryItem(InventoryItem item);
    void DeleteInventoryItem(int id);
    List<InventoryItem> GetAllInventoryItems();
    InventoryItem GetInventoryItem(int id);
    void UpdateInventoryItemQuantity(int id, int amount);
}