using System;
using System.Collections.Generic;
using System.Numerics;

namespace light_reflection.src;

public class Intersection
{
    private readonly Vector3 _position;
    private readonly Material _material;
    private readonly Vector3 _normal;
    public readonly double Distance;

    public Intersection(Vector3 position, double distance, Material material, Vector3 normal)
    {
        _position = position;
        Distance = distance;
        _material = material;
        _normal = normal;
    }


    public Color CalculateLighting(List<LightSource> lightSources)
    {
        double lightIntensity = 0;
        foreach (var lightSource in lightSources)
        {
            double sourceIntensity = 0;
            var lightDirection = Vector3.Normalize(lightSource.Position - _position);

            // Ambient reflection
            sourceIntensity += _material.Ka * MainWindow.AmbientLight / 255;

            // Diffuse reflection
            var diffuseFactor = Math.Max(Vector3.Dot(_normal, lightDirection), 0);
            var diffuseColor = _material.Kd * diffuseFactor;
            sourceIntensity += diffuseColor;

            // Specular reflection
            var viewDirection = CalculateViewDirection();
            var reflectionDirection = Vector3.Reflect(-lightDirection, _normal);
            var specularFactor = Math.Pow(Math.Max(Vector3.Dot(reflectionDirection, viewDirection), 0), _material.N);
            var specularColor = _material.Ks * specularFactor;
            sourceIntensity += specularColor;
            
            sourceIntensity *= lightSource.Intensity;
            lightIntensity += sourceIntensity;
        }

        return _material.Color * lightIntensity;
    }


    private Vector3 CalculateViewDirection()
    {
        var viewOrigin = new Vector3(_position.X, _position.Y, -600);
        var viewDirection = Vector3.Normalize(viewOrigin - _position);
        return viewDirection;
    }
}