using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestAppPariHard.Models;

namespace TestAppPariHard.Controllers;

[Route("/[controller]")]
public class PlayerController : Controller
{
    private readonly DataBaseContext _dbContext;
    private readonly IPlayerRepository _playerRepository;

    public PlayerController(DataBaseContext dbContext,
        IPlayerRepository playerRepository)
    {
        _dbContext = dbContext;
        _playerRepository = playerRepository;
    }

    [HttpGet]
    public ViewResult Index()
    {
        IQueryable<Player> players = _dbContext.Players;
        return View(_dbContext.Players.ToList());
    }

    [HttpGet("[action]")]
    public IActionResult Create()
    {
        return View(new Player() { RegistrationDate = DateTime.Now });
    }

    // Метод для создания нового игрока
    [Consumes("multipart/form-data")]
    [HttpPost("[action]")]
    public IActionResult Create([FromBody] Player player)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Players.Add(player);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(player);
    }

    [HttpGet("[action]")]
    public IActionResult PlayerReport(string statusFilter, bool betsGreaterThanDeposits)
    {
        var players = _dbContext.Players
            .Include(p => p.Transactions)
            .Include(p => p.Bets).ToList();

        var statuses = GetStatuses();
        ViewBag.Statuses = new SelectList(statuses);

        if (!string.IsNullOrEmpty(statusFilter))
        {
            players = players
               .Where(p => p.Status == statusFilter)
               .ToList();
        }

        if (betsGreaterThanDeposits)
        {
            players = players
              .Where(p =>
                  p.Bets.Sum(b => b.Amount)  > p.Transactions
                      .Where(t => t.TransactionType == "Внесение +")
                      .Sum(t => t.Amount)) // Условие: сумма ставок > сумма внесений
              .ToList();
        }

            var reportData = players.Select(player => new ReportPlayer(
            FullName: player.FullName,
            Balance: player.Balance,
            RegistrationDate: player.RegistrationDate,
            Status: player.Status,
            TotalDeposits: player.Transactions
                .Where(t => t.TransactionType == "Внесение +")
                .Sum(t => t.Amount),
            TotalBets: player.Bets.Sum(b => b.Amount)))
            .ToList();

        ViewBag.StatusFilter = statusFilter;

        return View(reportData);
    }

    private List<string> GetStatuses()
    {
        return _dbContext.Players.Select(p => p.Status).Distinct().ToList();
    }
}