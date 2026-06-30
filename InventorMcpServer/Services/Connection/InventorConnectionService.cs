using Inventor;
using InventorMcpServer.Interop;

namespace InventorMcpServer.Services.Connection;

public class InventorConnectionService : IInventorConnectionService
{
    private Application? _inventor;

    public Application? Application => _inventor;
    
    private PlanarSketch? _currentSketch;

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

        return _inventor?.ActiveDocument as PartDocument;
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


}