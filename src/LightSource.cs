using System;
using System.Numerics;
using System.Windows.Media.Media3D;

namespace light_reflection.src;

// Point based light source
public class LightSource
{
    public Vector3 Position { get; private set; }
    public double Intensity { get; set; } = 1;

    public LightSource(float x, float y, float z)
    {
        Position = new Vector3(x, y, z);
    }

    public void RotateAroundOrigin()
    {
        const int stepsForFullCircle = 16;
        const double radians = 2 * Math.PI / stepsForFullCircle;

        var cosTheta = Math.Cos(radians);
        var sinTheta = Math.Sin(radians);

        var centerX = MainWindow.WindowWidth / 2;
        var centerY = MainWindow.WindowHeight / 2;
        
        var translatedX = Position.X - centerX;
        var translatedY = Position.Y - centerY;

        var rotatedX = translatedX * cosTheta + translatedY * sinTheta;
        var rotatedY = -translatedX * sinTheta + translatedY * cosTheta;

        var x = rotatedX + centerX;
        var y = rotatedY + centerY;

        Position = new Vector3((float)x, (float)y, Position.Z);
    }
    

}