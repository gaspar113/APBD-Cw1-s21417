namespace APDB_Cw1_s21417.Policies;

public class DailyPenaltyCalculator(decimal perDayRate = 5.00m) : IPenaltyCalculator
{
    private readonly decimal _perDayRate = perDayRate;

    public decimal Calculate(DateTime dueDate, DateTime returnDate)
    {
        if (returnDate <= dueDate)
            return 0m;

        var daysLate = (returnDate.Date - dueDate.Date).Days;
        return daysLate * _perDayRate;
    }
}

