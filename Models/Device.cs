namespace APDB_Cw1_s21417.Models;

public abstract class Device(string name)
{
    private static int _nextId = 1;

    public int Id { get; } = _nextId++;
    public string Name { get; } = name;
}
