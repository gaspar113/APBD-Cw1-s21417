namespace APDB_Cw1_s21417.Models;

public abstract class User(string firstName, string lastName)
{
    private static int _nextId = 1;

    public int Id { get; } = _nextId++;
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;

    public abstract int MaxActiveLoans { get; }

    public string FullName => $"{FirstName} {LastName}";

    public string UserType => GetType().Name;

    public override string ToString() => $"{FullName} ({UserType})";
}
