using System.ComponentModel.DataAnnotations;

namespace TestAppPariHard.Models;

public class Player
{
    [Key]
    public int ID { get; set; }
    public string FullName { get; set; }
    public decimal Balance { get; set; }
    [DataType(DataType.Date)]
    public DateTime RegistrationDate { get; set; }
    public string Status { get; set; }

    public ICollection<Bet>? Bets { get; set; }
    public ICollection<Transaction>? Transactions { get; set; }
}