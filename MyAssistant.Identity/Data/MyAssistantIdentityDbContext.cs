using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyAssistant.Identity.Models;

namespace MyAssistant.Identity.Data;

public class MyAssistantIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public MyAssistantIdentityDbContext(DbContextOptions<MyAssistantIdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
