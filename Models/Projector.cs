namespace APDB_Cw1_s21417.Models;

public class Projector(string name, string connectorType, string resolution) : Device(name)
{
    public string ConnectorType { get; } = connectorType;
    public string Resolution { get; } = resolution;

    public override string GetDetails() =>
        $"{this} | Connector: {ConnectorType}, Resolution: {Resolution}";
}
