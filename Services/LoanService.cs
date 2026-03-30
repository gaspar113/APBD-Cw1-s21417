using APDB_Cw1_s21417.Models;
using APDB_Cw1_s21417.Policies;
using APDB_Cw1_s21417.Enums;

namespace APDB_Cw1_s21417.Services;

public class LoanService
{
    private readonly List<Loan> _loans = new();
    private readonly IPenaltyCalculator _penaltyCalculator;

    public LoanService(IPenaltyCalculator penaltyCalculator)
    {
        _penaltyCalculator = penaltyCalculator;
    }

    public Loan Borrow(User user, Device equipment, DateTime borrowDate, DateTime dueDate)
    {
        if (!equipment.IsAvailable)
            throw new InvalidOperationException($"Device '{equipment.Name}' is not available for borrowing.");

        var activeLoans = GetActiveLoansForUser(user.Id);
        if (activeLoans.Count >= user.MaxActiveLoans)
            throw new InvalidOperationException(
                $"User '{user.FullName}' has reached the maximum of {user.MaxActiveLoans} active loans.");

        if (dueDate <= borrowDate)
            throw new InvalidOperationException("Due date must be after the borrow date.");

        var loan = new Loan(user, equipment, borrowDate, dueDate);
        equipment.Status = DeviceStatus.Borrowed;
        _loans.Add(loan);

        Console.WriteLine(
            $"Device '{equipment.Name}' successfully lent to {user.FullName} until {dueDate:d}.");

        return loan;
    }

    public void Return(int loanId, DateTime returnDate)
    {
        var loan = _loans.FirstOrDefault(l => l.Id == loanId);
        if (loan is null)
            throw new KeyNotFoundException($"Loan with ID {loanId} not found.");

        if (!loan.IsActive)
            throw new InvalidOperationException($"Loan #{loanId} has already been returned.");

        var penalty = _penaltyCalculator.Calculate(loan.DueDate, returnDate);
        loan.CompleteReturn(returnDate, penalty);
        loan.Device.Status = DeviceStatus.Available;

        var message = penalty > 0
            ? $"Device '{loan.Device.Name}' returned late. Penalty: {penalty:C}."
            : $"Device '{loan.Device.Name}' returned on time.";

        Console.WriteLine(message);
    }

    public IReadOnlyList<Loan> GetActiveLoansForUser(int userId) =>
        _loans.Where(l => l.IsActive && l.User.Id == userId).ToList();

    public IReadOnlyList<Loan> GetOverdueLoans() =>
        _loans.Where(l => l.IsOverdue).ToList();

    public IReadOnlyList<Loan> GetAllLoans() => _loans;
}

