namespace APDB_Cw1_s21417.Policies;

public interface IPenaltyCalculator
{
    decimal Calculate(DateTime dueDate, DateTime returnDate);
}

