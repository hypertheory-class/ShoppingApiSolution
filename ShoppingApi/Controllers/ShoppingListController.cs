using Microsoft.AspNetCore.Mvc;

namespace ShoppingApi.Controllers;

public class ShoppingListController : ControllerBase
{
    private readonly ShoppingDataContext _context;

    public ShoppingListController(ShoppingDataContext context)
    {
        _context = context;
    }

    // POST purchased-shopping-items
    [HttpPost("purchased-shopping-items")]
    public async Task<ActionResult> MarkItemPurchased([FromBody] ShoppingListItem request)
    {
        await Task.Delay(2000);
        // 1. Does it exist? If so, do something, if not, return a bad request
        if (int.TryParse(request.id, out int id))
        {
            // Fake business rule ahead:
            if(id == 1)
            {
                return BadRequest("You must keep that on your list");
            }
            var savedItem = await _context.Items!.Where(i => i.Id == id).SingleOrDefaultAsync();
            if (savedItem == null)
            {
                return BadRequest("No matching item"); // 400
            }
            else
            {
                // 2. If it does.
                //    - Change the purchased property to true.
                savedItem.Purchased = true;
                //    - Save it.
                await _context.SaveChangesAsync();
                ///   - return Accepted.
                return Accepted();
            }
        } else
        {
            return BadRequest("Must have an ID");
        }
    }


    // GET /shopping-list
    [HttpGet("shopping-list")]
    public async Task<ActionResult> GetShoppingList()
    {
        await Task.Delay(3000);
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