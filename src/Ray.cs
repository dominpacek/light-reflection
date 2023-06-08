using System.Collections.Generic;
using System.Numerics;

namespace light_reflection.src;

public class Ray
{
    public Vector3 Origin { get; set; }
    public Vector3 Direction = new Vector3(0, 0, 1);
    
    // Generates a ray targeting the given pixel
    public Ray(int x, int y)
    {
        Origin = new Vector3(x, y, 0);
    }

    public Intersection? FindClosestIntersection(IEnumerable<Ball> balls)
    {
        Intersection? closestIntersection = null;
        var minDistance = double.MaxValue;
        
        foreach (var ball in balls)
        {
            var intersection = ball.IntersectRay(this);

            if (intersection != null && intersection.Distance < minDistance)
            {
                minDistance = intersection.Distance;
                closestIntersection = intersection;
            }
            
        }
        


        return closestIntersection;
    }
}