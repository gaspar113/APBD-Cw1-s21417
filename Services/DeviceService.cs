using APDB_Cw1_s21417.Models;
using APDB_Cw1_s21417.Enums;

namespace APDB_Cw1_s21417.Services;

public class DeviceService
{
    private readonly List<Device> _device = new();

    public void Add(Device item)
    {
        _device.Add(item);
    }

    public Device? GetById(int id) =>
        _device.FirstOrDefault(e => e.Id == id);

    public IReadOnlyList<Device> GetAll() => _device;

    public IReadOnlyList<Device> GetAvailable() =>
        _device.Where(e => e.IsAvailable).ToList();

    public void MarkUnavailable(int equipmentId)
    {
        var item = GetById(equipmentId);
        if (item is null)
            throw new InvalidOperationException($"Equipment with ID {equipmentId} not found.");

        if (item.Status == DeviceStatus.Borrowed)
            throw new InvalidOperationException(
                    $"Equipment '{item.Name}' is currently borrowed and cannot be marked unavailable.");

        item.Status = DeviceStatus.Unavailable;
        Console.WriteLine($"Equipment '{item.Name}' marked as unavailable.");
    }

    public void MarkAvailable(int equipmentId)
    {
        var item = GetById(equipmentId);
        if (item is null)
            throw new InvalidOperationException($"Equipment with ID {equipmentId} not found.");

        item.Status = DeviceStatus.Available;
        Console.WriteLine($"Equipment '{item.Name}' marked as available.");
    }
}

