using System.ComponentModel;
using InventorMcpServer.Services.Feature;
using ModelContextProtocol.Server;

namespace InventorMcpServer.Tools;

[McpServerToolType]
public class FeatureTool
{
    private readonly IFeatureService _featureService;

    public FeatureTool(IFeatureService featureService)
    {
        _featureService = featureService;
    }



    [McpServerTool]
    [Description("Extrudes the active sketch profile")]
    public string Extrude(double distance)
    {
        return _featureService.Extrude(distance);
    }

    [McpServerTool]
    public string CreateWorkPoint(double x, double y, double z)
    {
        return _featureService.CreateWorkPoint(x, y, z);
    }

    [McpServerTool]
    public string CreateHole(double diameter, double depth)
    {
        return _featureService.CreateHole(diameter, depth);
    }


    [McpServerTool]
    public string TestFace(int index)
    {
        return _featureService.TestFace(index);
    }

    [McpServerTool]
    [Description("Creates a through hole using the last sketch point.")]
    public string CreateThroughHole(double diameter)
    {
        return _featureService.CreateThroughHole(diameter);
    }

    [McpServerTool]
    [Description("Creates fillet on all outer edges.")]
    public string CreateFillet(double radius)
    {
        return _featureService.CreateFillet(radius);
    }

    [McpServerTool]
    [Description("Creates chamfer on all outer edges.")]
    public string CreateChamfer(double distance)
    {
        return _featureService.CreateChamfer(distance);
    }


    [McpServerTool]
    [Description("Creates a circular pattern from the last created feature.")]
    public string CreateCircularPattern(int count, double angle)
    {
        return _featureService.CreateCircularPattern(count, angle);
    }
}