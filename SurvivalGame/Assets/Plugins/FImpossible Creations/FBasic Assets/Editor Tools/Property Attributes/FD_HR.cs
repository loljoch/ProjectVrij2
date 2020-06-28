using UnityEngine;

public class FD_HRAttribute : PropertyAttribute
{
    public float r;
    public float g;
    public float b;
    public float a;
    public int thickness;
    public int padding;

    public FD_HRAttribute(int thickness = 2, int padding = 10, float aR = 0.5f, float aG = 0.5f, float aB = 0.5f, float aA = 0.5f)
    {
        r = aR;
        g = aG;
        b = aB;
        a = aA;
        this.thickness = thickness;
        this.padding = padding;
    }

    public Color Color { get { return new Color(r, g, b, a); } }
}

