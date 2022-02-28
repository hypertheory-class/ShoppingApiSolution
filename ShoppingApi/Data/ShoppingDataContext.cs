using Microsoft.EntityFrameworkCore;

namespace ShoppingApi.Data;

public class ShoppingDataContext : DbContext
{
    public ShoppingDataContext(DbContextOptions<ShoppingDataContext> options): base(options)
    {

    }
    public DbSet<ShoppingItem>? Items { get; set; } 
}
