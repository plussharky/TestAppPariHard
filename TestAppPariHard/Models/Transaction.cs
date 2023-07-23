using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestAppPariHard.Models;

public class Transaction
{
    [Key]
    public int TransactionID { get; set; }
    [ForeignKey("Player")]
    public int PlayerID { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string TransactionType { get; set; }
    public Player Player { get; set; }
}