using Inventor;
using InventorMcpServer.Services.Connection;

namespace InventorMcpServer.Services.Read;

public class ReadService : IReadService
{
    private readonly IInventorConnectionService _connection;

    public ReadService(IInventorConnectionService connection)
    {
        _connection = connection;
    }

    public string GetActiveDocumentName()
    {
        var part = _connection.GetActivePart();

        if (part == null)
            return "No Active Part.";

        return part.DisplayName;
    }

    public string GetSketches()
    {
        var part = _connection.GetActivePart();

        if (part == null)
            return "No Active Part.";

        var sketches = part.ComponentDefinition.Sketches;

        if (sketches.Count == 0)
            return "No Sketches.";

        List<string> names = new();

        foreach (PlanarSketch sketch in sketches)
        {
            names.Add(sketch.Name);
        }

        return string.Join(System.Environment.NewLine, names);
    }
}