using Microsoft.EntityFrameworkCore;

namespace TestAppPariHard.Models;

public class DataBaseContext : DbContext
{
    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {
    }

    public DbSet<Player> Players { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Bet> Bets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=database.db");
}