using System;
using System.Numerics;

namespace light_reflection.src;

public class Ball
{
    private readonly Vector3 _center;
    private readonly double _radius;

    private readonly Material _material = new();

    public Ball(Vector3 center, double radius)
    {
        _center = center;
        _radius = radius;
    }

    public Ball(Vector3 center, double radius, Material material)
    {
        _center = center;
        _radius = radius;
        _material = material;
    }

    public Intersection? IntersectRay(Ray ray)
    {
        var oc = ray.Origin - _center;
        var a = Vector3.Dot(ray.Direction, ray.Direction);
        var b = 2.0 * Vector3.Dot(oc, ray.Direction);
        var c = Vector3.Dot(oc, oc) - (_radius * _radius);

        var discriminant = b * b - 4 * a * c;

        if (discriminant < 0)
        {
            // No intersection
            return null;
        }

        var sqrtDiscriminant = Math.Sqrt(discriminant);
        var t1 = (-b - sqrtDiscriminant) / (2.0 * a);
        var t2 = (-b + sqrtDiscriminant) / (2.0 * a);

        // Take the closest intersection point in front of the ray's origin
        var distance = t1 >= 0 ? t1 : t2;

        if (distance < 0)
        {
            // Intersection is behind the ray's origin
            return null;
        }

        var intersectionPoint = ray.Origin + (float)distance * ray.Direction;

        return new Intersection(intersectionPoint, distance, 
            _material, CalculateSurfaceNormal(intersectionPoint));
    }

    private Vector3 CalculateSurfaceNormal(Vector3 position)
    {
        // Calculate the surface normal for a ball with a given center
        var surfaceNormal = Vector3.Normalize(position - _center);
        return surfaceNormal;
    }
}