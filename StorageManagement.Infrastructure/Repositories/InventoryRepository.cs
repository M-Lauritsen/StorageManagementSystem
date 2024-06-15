using StorageManagement.Domain.Interfaces;
using StorageManagement.Domain.Models;
using StorageManagement.Infrastructure.Data;

namespace StorageManagement.Infrastructure.Repositories;

public class InventoryRepository : IInventoryRepository
{
    private readonly DataContext _context;

    public InventoryRepository(DataContext context)
    {
        _context = context;
    }

    public void Add(InventoryItem item)
    {
        _context.InventoryItems.Add(item);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        _context.InventoryItems.Remove(GetById(id));
        _context.SaveChanges();
    }

    public List<InventoryItem> GetAll()
    {
        return _context.InventoryItems.ToList();
    }

    public InventoryItem GetById(int id)
    {
        return _context.InventoryItems.Find(id);
    }

    public void Update(InventoryItem item)
    {
        _context.InventoryItems.Update(item);
        _context.SaveChanges();
    }


}
