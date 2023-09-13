using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using APPR6312_POE.Models;

namespace APPR6312_POE.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<MonetaryDon> MonetaryDon { get; set; } = default!;
    public DbSet<GoodsDonations> Goods { get; set; }


    public DbSet<ApplicationUser> ApplicationUsers { get; set; }


    public DbSet<APPR6312_POE.Models.DisasterData> DisasterData { get; set; } = default!;


    public DbSet<APPR6312_POE.Models.AddGoods> AddGoods { get; set; } = default!;
}
