namespace APDB_Cw1_s21417.Policies;

public interface IPenaltyCalculator
{
    double Calculate(DateTime dueDate, DateTime returnDate);
}

