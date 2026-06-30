using Inventor;
using InventorMcpServer.Models;
using InventorMcpServer.Services.Connection;

namespace InventorMcpServer.Services.Geometry;

public class GeometryService : IGeometryService
{
    private readonly IInventorConnectionService _connection;

    public GeometryService(IInventorConnectionService connection)
    {
        _connection = connection;
    }

    public List<LineModel> ReadLines()
    {
        List<LineModel> result = new();

        var sketch = _connection.GetLastSketch();

        if (sketch == null)
            return result;

        foreach (SketchLine line in sketch.SketchLines)
        {
            result.Add(new LineModel
            {
                StartX = line.StartSketchPoint.Geometry.X,
                StartY = line.StartSketchPoint.Geometry.Y,
                EndX = line.EndSketchPoint.Geometry.X,
                EndY = line.EndSketchPoint.Geometry.Y
            });
        }

        return result;
    }

    public List<CircleModel> ReadCircles()
    {
        List<CircleModel> result = new();

        var sketch = _connection.CreateSketch();

        if (sketch == null)
            return result;

        foreach (SketchCircle circle in sketch.SketchCircles)
        {
            result.Add(new CircleModel
            {
                CenterX = circle.CenterSketchPoint.Geometry.X,
                CenterY = circle.CenterSketchPoint.Geometry.Y,
                Radius = circle.Radius
            });
        }

        return result;
    }

    public List<ArcModel> ReadArcs()
    {
        return new List<ArcModel>();
    }

    public List<PointModel> ReadPoints()
    {
        List<PointModel> result = new();

        var sketch = _connection.CreateSketch();

        if (sketch == null)
            return result;

        foreach (SketchPoint point in sketch.SketchPoints)
        {
            result.Add(new PointModel
            {
                X = point.Geometry.X,
                Y = point.Geometry.Y
            });
        }

        return result;
    }
}