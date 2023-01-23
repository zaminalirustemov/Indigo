using Indigo_Travel.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Indigo_Travel.Contex;
public class IndigoDbContext: IdentityDbContext
{
	public IndigoDbContext(DbContextOptions<IndigoDbContext> options):base(options)  { }

	public DbSet<RecentPost> RecentPosts { get; set; }
    public DbSet<AppUser> Users { get; set; }
}

