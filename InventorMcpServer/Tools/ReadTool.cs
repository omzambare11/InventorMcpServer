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
    [Description("Returns active Inventor document name")]
    public string ReadActiveDocument()
    {
        return _inventorService.GetActiveDocumentName();
    }
}