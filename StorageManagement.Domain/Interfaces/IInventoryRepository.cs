using StorageManagement.Domain.Models;

namespace StorageManagement.Domain.Interfaces;

public interface IInventoryRepository
{
    void Add(InventoryItem item);
    List<InventoryItem> GetAll();
    InventoryItem GetById(int id);
    void Update(InventoryItem item);
    void Delete(int id);
}
