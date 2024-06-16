using Microsoft.AspNetCore.Mvc;
using StorageManagement.Domain.Interfaces;
using StorageManagement.Domain.Models;

namespace StorageManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpGet]
    public IActionResult GetAllInventoryItems()
    {
        var item = _inventoryService.GetAllInventoryItems();
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpGet("{id}")]
    public IActionResult GetInventoryItem(int id)
    {
        var item = _inventoryService.GetInventoryItem(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpPost]
    public IActionResult AddInventoryItem([FromBody] InventoryItem item)
    {
        _inventoryService.AddInventoryItem(item);
        return CreatedAtAction(nameof(GetInventoryItem), new { id = item.Id }, item);
    }

    [HttpPut("{id}/quantity")]
    public IActionResult UpdateInventoryItemQuantity(int id, [FromBody] int amount)
    {
        _inventoryService.UpdateInventoryItemQuantity(id, amount);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteInventoryItem(int id)
    {
        var item = _inventoryService.GetInventoryItem(id);
        if (item == null)
        {
            return NotFound();
        }

        _inventoryService.DeleteInventoryItem(id);
        return Ok();
    }
}
