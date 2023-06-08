namespace light_reflection.src;

public class Material
{
   
    // ambient reflection
    public readonly double Ka = 0.5;
    // diffuse reflection
    public readonly double Kd = 0.5;
    // specular reflection
    public readonly double Ks = 0.5;
    // shininess (smoothness)
    public readonly double N = 5;
    
    public readonly Color Color = new Color(10, 10, 255);


    private Material(double ka, double kd, double ks, double n, Color color)
    {
        Ka = ka;
        Kd = kd;
        Ks = ks;
        N = n;
        Color = color;
    }

    public Material() { }

    public static readonly Material Metal = new Material(0.2, 0.2, 0.8, 40, new Color(255, 255, 255));
    public static readonly Material Plastic = new Material(0.3, 0.5, 0.5, 3, new Color(255, 10, 10));
    public static readonly Material Wood = new Material(0.3, 0.8, 0.2, 10, new Color(139, 70, 30));
    public static readonly Material Wall = new Material(0.3, 1, 0, 0, new Color(224, 214, 160));
    
}