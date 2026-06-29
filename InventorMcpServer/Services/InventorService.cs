using Inventor;
using InventorMcpServer.Interop;

namespace InventorMcpServer.Services;

public class InventorService : IInventorService
{
    private Application? _inventor;

    private bool Connect()
    {
        try
        {
            _inventor = (Application)ComInterop.GetRunningInstance("Inventor.Application");
            return true;
        }
        catch
        {
            return false;
        }
    }

    public string GetActiveDocumentName()
    {
        if (!Connect())
            return "Inventor is not running.";

        if (_inventor == null)
            return "Inventor connection failed.";

        var doc = _inventor.ActiveDocument;

        if (doc == null)
            return "No active document.";

        return doc.DisplayName;
    }

    public string GetSketches()
    {
        if (!Connect())
            return "Inventor is not running.";

        if (_inventor?.ActiveDocument is not PartDocument part)
            return "Active document is not a Part.";

        var sketches = part.ComponentDefinition.Sketches;

        if (sketches.Count == 0)
            return "No sketches found.";

        List<string> names = new();

        foreach (PlanarSketch sketch in sketches)
        {
            names.Add(sketch.Name);
        }

        return string.Join(System.Environment.NewLine, names);
    }

    public string CreateLine(
    double startX,
    double startY,
    double endX,
    double endY)
    {
        try
        {
            var sketch = GetSketch();

            if (sketch == null)
                return "No Sketch";

            var tg = _inventor!.TransientGeometry;

            sketch.SketchLines.AddByTwoPoints(
                tg.CreatePoint2d(startX, startY),
                tg.CreatePoint2d(endX, endY));

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
            var sketch = GetSketch();

            if (sketch == null)
                return "No Sketch";

            var tg = _inventor!.TransientGeometry;

            sketch.SketchCircles.AddByCenterRadius(
                tg.CreatePoint2d(centerX, centerY),
                radius);

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
            var sketch = GetSketch();

            if (sketch == null)
                return "No Sketch";

            var tg = _inventor!.TransientGeometry;

            var p1 = tg.CreatePoint2d(x, y);
            var p2 = tg.CreatePoint2d(x + width, y);
            var p3 = tg.CreatePoint2d(x + width, y + height);
            var p4 = tg.CreatePoint2d(x, y + height);

            sketch.SketchLines.AddByTwoPoints(p1, p2);
            sketch.SketchLines.AddByTwoPoints(p2, p3);
            sketch.SketchLines.AddByTwoPoints(p3, p4);
            sketch.SketchLines.AddByTwoPoints(p4, p1);

            return "Rectangle Created Successfully.";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }


    private PartDocument? GetActivePart()
    {
        if (!Connect())
            return null;

        return _inventor?.ActiveDocument as PartDocument;
    }



    private PlanarSketch? GetSketch()
    {
        var part = GetActivePart();

        if (part == null)
            return null;

        var compDef = part.ComponentDefinition;

        if (compDef.Sketches.Count == 0)
        {
            return compDef.Sketches.Add(compDef.WorkPlanes[3]);
        }

        return compDef.Sketches[1];
    }


}
