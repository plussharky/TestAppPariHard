namespace TestAppPariHard.Models
{
    public interface IPlayerRepository
    {
        IQueryable<Player> Players { get; }
    }
}