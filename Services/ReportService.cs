using APDB_Cw1_s21417.Enums;

namespace APDB_Cw1_s21417.Services;

public class ReportService
{
    private readonly DeviceService _deviceService;
    private readonly LoanService _loanService;
    private readonly UserService _userService;

    public ReportService(DeviceService equipmentService, LoanService loanService, UserService userService)
    {
        _deviceService = equipmentService;
        _loanService = loanService;
        _userService = userService;
    }

    public string GenerateSummary()
    {
        var allDevice = _deviceService.GetAll();
        var allLoans = _loanService.GetAllLoans();
        var activeLoans = allLoans.Where(l => l.IsActive).ToList();
        var overdueLoans = _loanService.GetOverdueLoans();
        var completedLoans = allLoans.Where(l => !l.IsActive).ToList();
        var totalPenalties = allLoans.Sum(l => l.Penalty);

        var lines = new List<string>
        {
            "========== SYSTEM STATUS REPORT ==========",
            "",
            $"Total equipment:          {allDevice.Count}",
            $"  Available:              {allDevice.Count(e => e.Status == DeviceStatus.Available)}",
            $"  Currently borrowed:     {allDevice.Count(e => e.Status == DeviceStatus.Borrowed)}",
            $"  Unavailable (service):  {allDevice.Count(e => e.Status == DeviceStatus.Unavailable)}",
            "",
            $"Total users:              {_userService.GetAll().Count}",
            "",
            $"Total loans:              {allLoans.Count}",
            $"  Active loans:           {activeLoans.Count}",
            $"  Overdue loans:          {overdueLoans.Count}",
            $"  Completed loans:        {completedLoans.Count}",
            "",
            $"Total penalties collected: {totalPenalties:C}",
            "",
            "=========================================="
        };

        return string.Join(Environment.NewLine, lines);
    }

    public string GenerateDeviceList(bool onlyAvailable = false)
    {
        var items = onlyAvailable
            ? _deviceService.GetAvailable()
            : _deviceService.GetAll();

        if (items.Count == 0)
            return onlyAvailable ? "No available equipment." : "No equipment in the system.";

        var header = onlyAvailable ? "--- Available Device ---" : "--- All Device ---";
        var lines = new List<string> { header };
        foreach (var item in items)
            lines.Add(item.GetDetails());

        return string.Join(Environment.NewLine, lines);
    }

    public string GenerateUserLoanReport(int userId)
    {
        var user = _userService.GetById(userId);
        if (user is null)
            return $"User with ID {userId} not found.";

        var activeLoans = _loanService.GetActiveLoansForUser(userId);
        if (activeLoans.Count == 0)
            return $"{user.FullName} has no active loans.";

        var lines = new List<string>
        {
            $"--- Active Loans for {user.FullName} ({user.UserType}) ---"
        };
        foreach (var loan in activeLoans)
            lines.Add(loan.ToString());

        return string.Join(Environment.NewLine, lines);
    }

    public string GenerateOverdueReport()
    {
        var overdue = _loanService.GetOverdueLoans();
        if (overdue.Count == 0)
            return "No overdue loans.";

        var lines = new List<string> { "--- Overdue Loans ---" };
        foreach (var loan in overdue)
            lines.Add(loan.ToString());

        return string.Join(Environment.NewLine, lines);
    }
}

