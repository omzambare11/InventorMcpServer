using Inventor;
using InventorMcpServer.Services.Connection;

namespace InventorMcpServer.Services.Sketch;

public class SketchService : ISketchService
{
    private readonly IInventorConnectionService _connection;

    public SketchService(IInventorConnectionService connection)
    {
        _connection = connection;
    }

    public string CreateLine(
        double startX,
        double startY,
        double endX,
        double endY)
    {
        try
        {
            var sketch = _connection.CreateSketch(3);

            if (sketch == null)
                return "No Sketch";

            var tg = _connection.Application!.TransientGeometry;

            var line = sketch.SketchLines.AddByTwoPoints(
            tg.CreatePoint2d(startX, startY),
            tg.CreatePoint2d(endX, endY));

            _connection.SetLastSketchLine(line);

            sketch.ExitEdit();
            return "Line Created Successfully.";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string CreateCircle(double centerX, double centerY, double radius)
    {
        try
        {
            var sketch = _connection.CreateSketch(2);
            if (sketch == null)
                return "Sketch NULL";

            var tg = _connection.Application!.TransientGeometry;

            SketchCircle circle;

            try
            {
                circle = sketch.SketchCircles.AddByCenterRadius(
                    tg.CreatePoint2d(centerX, centerY),
                    radius);

                System.Diagnostics.Debug.WriteLine("Circle OK");
            }
            catch (Exception ex)
            {
                return "Circle Failed : " + ex.Message;
            }

            try
            {
                var profile = sketch.Profiles.AddForSolid();

                _connection.SetLastProfile(profile);

                System.Diagnostics.Debug.WriteLine("Profile OK");
            }
            catch (Exception ex)
            {
                return "Profile Failed : " + ex.Message;
            }

            sketch.ExitEdit();

            return "Circle Created";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    public string CreateRectangle(
    double x,
    double y,
    double width,
    double height)
    {
        try
        {
            var sketch = _connection.CreateSketch();

            if (sketch == null)
                return "No Sketch";

            var tg = _connection.Application!.TransientGeometry;

            var p1 = tg.CreatePoint2d(x, y);
            var p2 = tg.CreatePoint2d(x + width, y + height);

            // Inventor Native Rectangle API
            sketch.SketchLines.AddAsTwoPointRectangle(p1, p2);

            sketch.ExitEdit();

            return "Rectangle Created Successfully.";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string CreatePoint(double x, double y)
    {
        try
        {
            var sketch = _connection.GetLastSketch();

            if (sketch == null)
                return "No Sketch";

            var tg = _connection.Application!.TransientGeometry;

            foreach (SketchPoint p in sketch.SketchPoints)
            {
                if (Math.Abs(p.Geometry.X - x) < 0.0001 &&
                    Math.Abs(p.Geometry.Y - y) < 0.0001)
                {
                    _connection.SetLastSketchPoint(p);
                    return "Point already exists.";
                }
            }

            SketchPoint point =
                sketch.SketchPoints.Add(
                    tg.CreatePoint2d(x, y),
                    false);

            _connection.SetLastSketchPoint(point);

            return "Point Created Successfully.";

            _connection.SetLastSketchPoint(point);

            sketch.ExitEdit();

            return "Point Created Successfully.";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}