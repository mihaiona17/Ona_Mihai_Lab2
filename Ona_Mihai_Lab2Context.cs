using Microsoft.EntityFrameworkCore;

public class Ona_Mihai_Lab2Context(DbContextOptions<Ona_Mihai_Lab2Context> options) : DbContext(options)
{
    public DbSet<Ona_Mihai_Lab2.Models.Book> Book { get; set; } = default!;
    public DbSet<Ona_Mihai_Lab2.Models.Author> Authors { get; set; } = default!;
    public DbSet<Ona_Mihai_Lab2.Models.Publisher> Publishers { get; set; } = default!;
}
