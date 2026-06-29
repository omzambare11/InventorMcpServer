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
        if (!Connect())
            return "Inventor is not running.";

        if (_inventor?.ActiveDocument is not PartDocument part)
            return "Active document is not a Part.";

        try
        {
            var compDef = part.ComponentDefinition;

            PlanarSketch sketch;

            if (compDef.Sketches.Count == 0)
            {
                var workPlane = compDef.WorkPlanes[3]; // XY Plane
                sketch = compDef.Sketches.Add(workPlane);
            }
            else
            {
                sketch = compDef.Sketches[1];
            }

            var tg = _inventor.TransientGeometry;

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
        return "Coming Next";
    }

    public string CreateRectangle(
        double x,
        double y,
        double width,
        double height)
    {
        return "Coming Next";
    }
}