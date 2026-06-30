using System.ComponentModel;
using InventorMcpServer.Services.Document;
using ModelContextProtocol.Server;

namespace InventorMcpServer.Tools;

[McpServerToolType]
public class DocumentTool
{
    private readonly IDocumentService _documentService;

    public DocumentTool(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    [McpServerTool]
    [Description("Returns active Inventor document")]
    public string ReadActiveDocument()
    {
        return _documentService.GetActiveDocumentName();
    }

    [McpServerTool]
    [Description("Returns all sketches")]
    public string ReadSketches()
    {
        return _documentService.GetSketches();
    }
}