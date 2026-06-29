using System.ComponentModel;
using InventorMcpServer.Services.Read;
using ModelContextProtocol.Server;

namespace InventorMcpServer.Tools;

[McpServerToolType]
public class ReadTool
{
    private readonly IReadService _readService;

    public ReadTool(IReadService readService)
    {
        _readService = readService;
    }

    [McpServerTool]
    [Description("Returns Active Inventor Document")]
    public string ReadActiveDocument()
    {
        return _readService.GetActiveDocumentName();
    }

    [McpServerTool]
    [Description("Returns all sketches")]
    public string ReadSketches()
    {
        return _readService.GetSketches();
    }
}