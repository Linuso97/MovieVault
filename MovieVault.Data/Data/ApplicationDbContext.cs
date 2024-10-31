using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MovieVault.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }

    // Makes property UserId of object Movie to a FK of IdentityUser Id.
    // Could've used EF Core for this and just added prop in Movie object
    // but I wanted to try this out.
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    base.OnModelCreating(modelBuilder);

    //    modelBuilder.Entity<Movie>()
    //        .HasOne<IdentityUser>()                
    //        .WithMany()                            
    //        .HasForeignKey(m => m.UserId)          
    //        .OnDelete(DeleteBehavior.Cascade);     
    //}
}
