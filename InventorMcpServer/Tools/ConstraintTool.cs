using System.ComponentModel;
using InventorMcpServer.Models;
using InventorMcpServer.Services.Constraint;
using ModelContextProtocol.Server;

namespace InventorMcpServer.Tools;

[McpServerToolType]
public class ConstraintTool
{
    private readonly IConstraintService _constraintService;

    public ConstraintTool(IConstraintService constraintService)
    {
        _constraintService = constraintService;
    }

    [McpServerTool]
    [Description("Read dimensions")]
    public List<DimensionModel> ReadDimensions()
    {
        return _constraintService.ReadDimensions();
    }

    [McpServerTool]
    [Description("Read constraints")]
    public List<ConstraintModel> ReadConstraints()
    {
        return _constraintService.ReadConstraints();
    }
}