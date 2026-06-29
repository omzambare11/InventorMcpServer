using System.ComponentModel;
using InventorMcpServer.Services;
using ModelContextProtocol.Server;

namespace InventorMcpServer.Tools;

[McpServerToolType]
public class SketchTool
{
    private readonly IInventorService _inventorService;

    public SketchTool(IInventorService inventorService)
    {
        _inventorService = inventorService;
    }

    [McpServerTool]
    [Description("Create a Line")]
    public string CreateLine(
        double startX,
        double startY,
        double endX,
        double endY)
    {
        return _inventorService.CreateLine(startX, startY, endX, endY);
    }

    [McpServerTool]
    [Description("Create a Circle")]
    public string CreateCircle(
        double centerX,
        double centerY,
        double radius)
    {
        return _inventorService.CreateCircle(centerX, centerY, radius);
    }

    [McpServerTool]
    [Description("Create a Rectangle")]
    public string CreateRectangle(
        double x,
        double y,
        double width,
        double height)
    {
        return _inventorService.CreateRectangle(x, y, width, height);
    }
}