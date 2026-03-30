namespace APDB_Cw1_s21417.Models;

public class Loan(User user, Device device, DateTime startDate, DateTime dueDate)
{
    private static int _nextId = 1;

    public int Id { get; } = _nextId++;
    public User User { get; } = user;
    public Device Device { get; } = device;
    public DateTime StartDate { get; } = startDate;
    public DateTime DueDate { get; } = dueDate;
    public DateTime? ReturnDate { get; set; }
    public decimal Penalty { get; private set; }

    public bool IsActive => ReturnDate is null;

    public bool IsOverdue => IsActive && DateTime.Now > DueDate;

    public void CompleteReturn(DateTime returnDate, decimal penalty)
    {
        ReturnDate = returnDate;
        Penalty = penalty;
    }

    public override string ToString()
    {
        var status = IsActive ? (IsOverdue ? "OVERDUE" : "Active") : "Returned";
        var penaltyInfo = Penalty > 0 ? $", Penalty: {Penalty:C}" : "";
        return $"[Loan #{Id}] {Device.Name} -> {User.FullName} | " +
               $"Borrowed: {StartDate:d}, Due: {DueDate:d}" +
               (ReturnDate.HasValue ? $", Returned: {ReturnDate:d}" : "") +
               $" [{status}]{penaltyInfo}";
    }
}
