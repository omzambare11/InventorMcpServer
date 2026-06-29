using System.ComponentModel;
using InventorMcpServer.Services.Sketch;
using ModelContextProtocol.Server;

namespace InventorMcpServer.Tools;

[McpServerToolType]
public class SketchTool
{
    private readonly ISketchService _sketchService;

    public SketchTool(ISketchService sketchService)
    {
        _sketchService = sketchService;
    }

    [McpServerTool]
    [Description("Create Line")]
    public string CreateLine(
        double startX,
        double startY,
        double endX,
        double endY)
    {
        return _sketchService.CreateLine(startX, startY, endX, endY);
    }

    [McpServerTool]
    [Description("Create Circle")]
    public string CreateCircle(
        double centerX,
        double centerY,
        double radius)
    {
        return _sketchService.CreateCircle(centerX, centerY, radius);
    }

    [McpServerTool]
    [Description("Create Rectangle")]
    public string CreateRectangle(
        double x,
        double y,
        double width,
        double height)
    {
        return _sketchService.CreateRectangle(x, y, width, height);
    }
}