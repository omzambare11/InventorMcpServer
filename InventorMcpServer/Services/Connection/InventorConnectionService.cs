using Inventor;
using InventorMcpServer.Interop;

namespace InventorMcpServer.Services.Connection;

public class InventorConnectionService : IInventorConnectionService
{
    private Application? _inventor;

    public Application? Application => _inventor;
    
    private PlanarSketch? _currentSketch;

    private SketchPoint? _currentPoint;

    private WorkPoint? _currentWorkPoint;

    private SketchPoint? _lastSketchPoint;

    public bool Connect()
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

    public PlanarSketch CreateSketch()
    {
        var part = GetActivePart();

        if (part == null)
            throw new Exception("No active part.");

        _currentSketch = part.ComponentDefinition
            .Sketches
            .Add(part.ComponentDefinition.WorkPlanes[3]);

        _currentSketch.Edit();

        return _currentSketch;
    }

    public PartDocument? GetActivePart()
    {
        if (!Connect())
            return null;

        var part = _inventor?.ActiveDocument as PartDocument;

        if (part != null)
        {
            part.UnitsOfMeasure.LengthUnits =
                UnitsTypeEnum.kMillimeterLengthUnits;
        }

        return part;
    }

    public Profile? GetLastProfile()
    {
        var sketch = GetLastSketch();

        if (sketch == null)
            return null;

        // Inventor generates the profile here
        return sketch.Profiles.AddForSolid();
    }

    public PlanarSketch? GetLastSketch()
    {
        if (_currentSketch != null)
            return _currentSketch;

        var part = GetActivePart();

        if (part == null)
            return null;

        var sketches = part.ComponentDefinition.Sketches;

        if (sketches.Count == 0)
            return null;

        return sketches[sketches.Count];
    }

    public Profile? GetProfile()
    {
        var sketch = GetSketch();

        if (sketch == null)
            return null;

        if (sketch.Profiles.Count == 0)
            return null;

        return sketch.Profiles[1];
    }

    public PlanarSketch? GetSketch()
    {
        var part = GetActivePart();

        if (part == null)
            return null;

        var compDef = part.ComponentDefinition;

        if (compDef.Sketches.Count == 0)
            return compDef.Sketches.Add(compDef.WorkPlanes[3]);

        return compDef.Sketches[1];
    }

    public void ClearCurrentSketch()
    {
        _currentSketch = null;
    }

    public Face? GetFace(int index)
    {
        var part = GetActivePart();

        if (part == null)
            return null;

        var faces = part.ComponentDefinition.SurfaceBodies[1].Faces;

        if (index < 1 || index > faces.Count)
            return null;

        return faces[index];
    }

    public void SetLastPoint(SketchPoint point)
    {
        _currentPoint = point;
    }

    public SketchPoint? GetLastPoint()
    {
        return _currentPoint;
    }

    public void SetCurrentWorkPoint(WorkPoint point)
    {
        _currentWorkPoint = point;
    }

    public WorkPoint? GetCurrentWorkPoint()
    {
        return _currentWorkPoint;
    }

    public WorkAxis? GetDefaultAxis()
    {
        var part = GetActivePart();

        if (part == null)
            return null;

        return part.ComponentDefinition.WorkAxes[3];
    }

    public void SetLastSketchPoint(SketchPoint point)
    {
        _lastSketchPoint = point;
    }

    public SketchPoint? GetLastSketchPoint()
    {
        return _lastSketchPoint;
    }

    private object? _lastFeature;

    public object? GetLastFeature()
    {
        return _lastFeature;
    }

    public void SetLastFeature(object feature)
    {
        _lastFeature = feature;
    }

    public ObjectCollection CreateFeatureCollection()
    {
        var collection =
            Application!.TransientObjects.CreateObjectCollection();

        var feature = GetLastFeature();

        if (feature != null)
            collection.Add(feature);

        return collection;
    }




}