namespace TestAppPariHard.Models;

public record ReportPlayer(
    string FullName,
    decimal Balance,
    DateTime RegistrationDate,
    string Status,
    decimal TotalDeposits,
    decimal TotalBets);