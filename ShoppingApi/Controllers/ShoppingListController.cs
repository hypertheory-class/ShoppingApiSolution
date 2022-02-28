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
    public ActionResult GetShoppingList()
    {
        var response = _context.Items!
            .Where(i=> i.Removed == false)
            .Select(i => new ShoppingListItem(i.Id.ToString(), i.Description, i.Purchased))
            .ToList();
        return Ok(new { data = response});
    }


}

public record ShoppingListItem(string id, string description, bool purchased);