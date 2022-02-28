using Microsoft.AspNetCore.Mvc;

namespace ShoppingApi.Controllers;

public class ShoppingListController : ControllerBase
{
    private readonly ShoppingDataContext _context;

    public ShoppingListController(ShoppingDataContext context)
    {
        _context = context;
    }


    // GET /shopping-list
    [HttpGet("shopping-list")]
    public async Task<ActionResult> GetShoppingList()
    {
        var response = await _context.Items!
            .Where(i=> i.Removed == false)
            .Select(i => new ShoppingListItem(i.Id.ToString(), i.Description, i.Purchased))
            .ToListAsync();
        return Ok(new { data = response});
    }

    [HttpPost("shopping-list")]
    public async Task<ActionResult> AddShoppingItem([FromBody] PostShoppingListRequest request)
    {
        // 1. validate the thing.
        var item = new ShoppingItem
        {
            Description = request.description,
            Purchased = false,
            WhenAdded = DateTime.Now,
            Removed = false
        };
        _context.Items!.Add(item);
        await _context.SaveChangesAsync(); // actually save it.

        var response = new ShoppingListItem(item.Id.ToString(), item.Description, item.Purchased);
        return Ok(response);
    }

}

public record ShoppingListItem(string id, string description, bool purchased);

public record PostShoppingListRequest(string description);