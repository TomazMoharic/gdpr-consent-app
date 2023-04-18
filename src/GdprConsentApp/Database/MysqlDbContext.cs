using Microsoft.EntityFrameworkCore;

namespace GdprConsentApp.Database;

public class MysqlDbContext : DbContext
{
    public MysqlDbContext(DbContextOptions<MysqlDbContext> options) : base(options) { }
    
    public DbSet<TConsents> Consents { get; set; }
    public DbSet<TUsers> Users { get; set; }
    public DbSet<TUsersConsents> UsersConsents { get; set; }
}