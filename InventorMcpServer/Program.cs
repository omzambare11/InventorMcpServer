using InventorMcpServer.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModelContextProtocol.Server;



//Implemented Inventor MCP Server V1
//-Connected to Inventor 2027
//- Read Active Document
//- Read Sketches
//- Create Line
//- Create Circle
//- Create Rectangle
//- Claude Desktop MCP Integration

var builder = Host.CreateEmptyApplicationBuilder(settings: null);

builder.Services.AddSingleton<IInventorService, InventorService>();

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

await builder.Build().RunAsync();

//Implemented Inventor MCP Server V1
//-Connected to Inventor 2027
//- Read Active Document
//- Read Sketches
//- Create Line
//- Create Circle
//- Create Rectangle
//- Claude Desktop MCP Integration