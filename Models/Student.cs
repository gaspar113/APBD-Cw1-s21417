namespace APDB_Cw1_s21417.Models;

public class Student(string firstName, string lastName) : User(firstName, lastName)
{
    public override int MaxActiveLoans => 2;
}
