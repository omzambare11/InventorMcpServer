using Inventor;
using InventorMcpServer.Interop;

namespace InventorMcpServer.Services.Connection;

public class InventorConnectionService : IInventorConnectionService
{
    private Application? _inventor;

    public Application? Application => _inventor;

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

    public PartDocument? GetActivePart()
    {
        if (!Connect())
            return null;

        return _inventor?.ActiveDocument as PartDocument;
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
}