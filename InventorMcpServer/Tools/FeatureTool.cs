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
}