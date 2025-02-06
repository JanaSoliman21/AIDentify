using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Data.Common;

namespace AIDentify.Models.Context
{
    public class ContextAIDentify: DbContext
    {
       public ContextAIDentify : base() { }
    //inject
    public BlogDbContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Department> Departments { get; set; }
}
}
