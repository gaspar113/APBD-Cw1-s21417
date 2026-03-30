namespace APDB_Cw1_s21417.Models;

public class Camera(string name, double megaPixels, string lensType) : Device(name)
{
    public double MegaPixels { get; } = megaPixels;
    public string LensType { get; } = lensType;

    public override string GetDetails() =>
        $"{this} | Megapixels: {MegaPixels}, Lens: {LensType}";
}
