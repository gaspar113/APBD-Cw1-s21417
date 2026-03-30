namespace APDB_Cw1_s21417.Models;

public class Laptop(string name, string processor, int ramgb) : Device(name)
{
    public string Processor { get; } = processor;
    public int RamGb { get; } = ramgb;

    public override string GetDetails() =>
        $"{this} | Processor: {Processor}, RAM: {RamGb} GB";
}
