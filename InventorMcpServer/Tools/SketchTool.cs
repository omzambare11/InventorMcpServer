using System.ComponentModel;
using ModelContextProtocol.Server;

namespace InventorMcpServer.Tools;

[McpServerToolType]
public class SketchTool
{
    [McpServerTool]
    [Description("Create a line")]
    public string CreateLine(
        double x1,
        double y1,
        double x2,
        double y2)
    {
        return $"Line ({x1},{y1}) -> ({x2},{y2})";
    }
}