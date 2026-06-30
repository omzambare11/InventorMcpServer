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
            var sketch = _connection.CreateSketch();

            if (sketch == null)
                return "No Sketch";

            var tg = _connection.Application!.TransientGeometry;

            sketch.SketchLines.AddByTwoPoints(
                tg.CreatePoint2d(startX, startY),
                tg.CreatePoint2d(endX, endY));

            sketch.ExitEdit();
            return "Line Created Successfully.";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string CreateCircle(
        double centerX,
        double centerY,
        double radius)
    {
        try
        {
            var sketch = _connection.CreateSketch();

            if (sketch == null)
                return "No Sketch";

            var tg = _connection.Application!.TransientGeometry;

            sketch.SketchCircles.AddByCenterRadius(
                tg.CreatePoint2d(centerX, centerY),
                radius);

            sketch.ExitEdit();
            return "Circle Created Successfully.";
        }
        catch (Exception ex)
        {
            return ex.Message;
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
}