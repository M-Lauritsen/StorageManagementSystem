using StorageManagement.Domain.Interfaces;
using StorageManagement.Domain.Models;

namespace StorageManagement.Application.Services;

public class InventoryService(IInventoryRepository inventoryRepository) : IInventoryService
{
    private readonly IInventoryRepository _inventoryRepository = inventoryRepository;

    public void AddInventoryItem(InventoryItem item)
    {
        _inventoryRepository.Add(item);
    }

    public void DeleteInventoryItem(int id)
    {
        _inventoryRepository.Delete(id);
    }

    public List<InventoryItem> GetAllInventoryItems()
    {
        return _inventoryRepository.GetAll();
    }

    public InventoryItem GetInventoryItem(int id)
    {
        return _inventoryRepository.GetById(id);
    }

    public void UpdateInventoryItemQuantity(int id, int amount)
    {
        var item = _inventoryRepository.GetById(id);
        item.UpdateQuantity(amount);
        _inventoryRepository.Update(item);
    }
}
