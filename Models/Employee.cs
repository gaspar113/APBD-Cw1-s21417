namespace APDB_Cw1_s21417.Models;

public class Employee(string firstName, string lastName) : User(firstName, lastName)
{
    public override int MaxActiveLoans => 5;
}
