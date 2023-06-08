using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using light_reflection.src;

namespace light_reflection;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public const int WindowWidth = 1200;
    public const int WindowHeight = 800;
    public static byte AmbientLight { get; private set; } = 60;
    private const byte AmbientLightStep = 30;
    private readonly Image _i = new();
    private readonly WriteableBitmap _frame;

    private readonly List<LightSource> _lightSources = new();
    private readonly List<Ball> _balls = new();

    public MainWindow()
    {
        InitializeComponent();


        _lightSources.Add(new LightSource(800, -100, -400));
        _balls.Add(new Ball(new Vector3(400, 200, 100), 100, Material.Plastic));
        _balls.Add(new Ball(new Vector3(800, 200, 100), 100, Material.Metal));
        _balls.Add(new Ball(new Vector3(400, 600, 100), 100, Material.Wall));
        _balls.Add(new Ball(new Vector3(800, 600, 100), 100, Material.Wood));
        _balls.Add(new Ball(new Vector3(WindowWidth / 2, WindowHeight / 2, 100), 40));


        _frame = new WriteableBitmap(WindowWidth, WindowHeight, 96, 96, PixelFormats.Bgr32, null);
        _i.Source = _frame;
        Canvas.Children.Add(_i);


        CompositionTarget.Rendering += RotateLightSource;
        CompositionTarget.Rendering += Render;
    }

    private void RotateLightSource(object? sender, EventArgs e)
    {
        _lightSources[0].RotateAroundOrigin();
    }


    private void DrawPixels()
    {
        _frame.Lock();
        var buffer = new byte[_frame.BackBufferStride * WindowHeight];

        for (var y = 0; y < WindowHeight; y++)
        {
            for (var x = 0; x < WindowWidth; x++)
            {
                var ray = new Ray(x, y);

                var intersection = ray.FindClosestIntersection(_balls);

                byte red, green, blue;
                if (intersection == null)
                {
                    red = AmbientLight;
                    green = AmbientLight;
                    blue = AmbientLight;
                }
                else
                {
                    var color = intersection.CalculateLighting(_lightSources);
                    red = color.Red;
                    green = color.Green;
                    blue = color.Blue;
                }

                const byte alpha = 255;

                buffer[y * _frame.BackBufferStride + x * 4] = blue;
                buffer[y * _frame.BackBufferStride + x * 4 + 1] = green;
                buffer[y * _frame.BackBufferStride + x * 4 + 2] = red;
                buffer[y * _frame.BackBufferStride + x * 4 + 3] = alpha;
            }
        }

        _frame.WritePixels(new Int32Rect(0, 0, _frame.PixelWidth, _frame.PixelHeight),
            buffer, _frame.PixelWidth * _frame.Format.BitsPerPixel / 8, 0);
        _frame.Unlock();
    }

    private void Render(object? sender, EventArgs e)
    {
        DrawPixels();
    }


    protected override void OnKeyDown(KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.Down:
                if (AmbientLight >= 0 + AmbientLightStep) AmbientLight -= AmbientLightStep;
                break;
            case Key.Up:
                if (AmbientLight <= 255 - AmbientLightStep) AmbientLight += AmbientLightStep;
                break;
        }
    }
}