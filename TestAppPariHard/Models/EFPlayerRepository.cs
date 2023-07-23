using Microsoft.EntityFrameworkCore;

namespace TestAppPariHard.Models
{
    public class EFPlayerRepository : IPlayerRepository
    {
        private DataBaseContext _context;

        public EFPlayerRepository(DataBaseContext context)
        {
            _context = context;
        }

        public IQueryable<Player> Players => _context.Players
            .Include(p => p.Bets)
            .Include(p => p.Transactions);
    }
}
