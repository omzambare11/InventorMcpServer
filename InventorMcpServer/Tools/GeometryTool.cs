using System.ComponentModel;
using InventorMcpServer.Models;
using InventorMcpServer.Services.Geometry;
using ModelContextProtocol.Server;

namespace InventorMcpServer.Tools;

[McpServerToolType]
public class GeometryTool
{
    private readonly IGeometryService _geometryService;

    public GeometryTool(IGeometryService geometryService)
    {
        _geometryService = geometryService;
    }

    [McpServerTool]
    [Description("Read all lines")]
    public List<LineModel> ReadLines()
    {
        return _geometryService.ReadLines();
    }

    [McpServerTool]
    [Description("Read all circles")]
    public List<CircleModel> ReadCircles()
    {
        return _geometryService.ReadCircles();
    }

    [McpServerTool]
    [Description("Read all arcs")]
    public List<ArcModel> ReadArcs()
    {
        return _geometryService.ReadArcs();
    }

    [McpServerTool]
    [Description("Read all points")]
    public List<PointModel> ReadPoints()
    {
        return _geometryService.ReadPoints();
    }

    [McpServerTool]
    [Description("Read all faces")]
    public List<FaceModel> ReadFaces()
    {
        return _geometryService.ReadFaces();
    }
}