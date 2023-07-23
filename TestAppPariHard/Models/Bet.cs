using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestAppPariHard.Models;

public class Bet
{
    [Key]
    public int BetID { get; set; }
    [ForeignKey("Player")]
    public int PlayerID { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public decimal WinningAmount { get; set; }
    public DateTime CalculationDate { get; set; }
    public Player Player { get; set; }
}