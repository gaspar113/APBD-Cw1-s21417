namespace APDB_Cw1_s21417.Models;

public class Camera(string name, int megaPixels, string lensType) : Device(name)
{
    public int MegaPixels { get; } = megaPixels;
    public string LensType { get; } = lensType;
}
