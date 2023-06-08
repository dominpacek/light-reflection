using System;

namespace light_reflection.src;

public class Color
{
    public readonly byte Red = 255;
    public readonly byte Green = 10;
    public readonly byte Blue = 10;

    public Color(byte red, byte green, byte blue)
    {
        Red = red;
        Green = green;
        Blue = blue;
    }

    public Color() { }
    
    public static Color operator *(Color color, double multiplier)
    {
        var red = color.Red * multiplier;
        var green = color.Green * multiplier;
        var blue = color.Blue * multiplier;

        red = Math.Min(red, 255);
        green = Math.Min(green, 255);
        blue = Math.Min(blue, 255);

        return new Color((byte)red, (byte)green, (byte)blue);
    }
}