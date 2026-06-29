using System.ComponentModel;
using InventorMcpServer.Services;
using ModelContextProtocol.Server;

namespace InventorMcpServer.Tools;

[McpServerToolType]
public class ReadTool
{
    private readonly IInventorService _inventorService;

    public ReadTool(IInventorService inventorService)
    {
        _inventorService = inventorService;
    }

    [McpServerTool]
    [Description("Returns Active Inventor Document")]
    public string ReadActiveDocument()
    {
        return _inventorService.GetActiveDocumentName();
    }

    [McpServerTool]
    [Description("Returns all sketches")]
    public string ReadSketches()
    {
        return _inventorService.GetSketches();
    }
}