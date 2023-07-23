using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAppPariHard.Models;

namespace TestAppPariHard.Controllers;

[ApiController]
[Route("balance")]
public class BalanceController : Controller
{
    private readonly DataBaseContext _dbContext;

    public BalanceController(DataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Метод для пересчета баланса игрока
    [HttpPost]
    public IActionResult RecalculateBalance(int playerId)
    {
        // Находим игрока по ID
        var player = _dbContext.Players.FirstOrDefault(p => p.ID == playerId);
        if (player == null)
        {
            return NotFound();
        }

        // Получаем все транзакции игрока
        var transactions = _dbContext.Transactions.Where(t => t.PlayerID == playerId).ToList();

        // Пересчитываем баланс на основе транзакций
        foreach (var transaction in transactions)
        {
            if (transaction.TransactionType == "Внесение +")
            {
                player.Balance += transaction.Amount; // Добавляем сумму
            }
            else if (transaction.TransactionType == "Снятие -")
            {
                player.Balance -= transaction.Amount; // Вычитаем сумму
            }
        }

        // Сохраняем изменения в базе данных
        _dbContext.SaveChanges();

        return Ok();
    }
}