namespace APDB_Cw1_s21417.Policies;

public class DailyPenaltyCalculator(double perDayRate) : IPenaltyCalculator
{
    private readonly double _perDayRate = perDayRate;

    public double Calculate(DateTime dueDate, DateTime returnDate)
    {
        if (returnDate <= dueDate)
            return 0;

        var daysLate = (returnDate.Date - dueDate.Date).Days;
        return daysLate * _perDayRate;
    }
}

