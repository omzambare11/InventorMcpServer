using InventorMcpServer.Services;
using InventorMcpServer.Services.Connection;
using InventorMcpServer.Services.Read;
using InventorMcpServer.Services.Sketch;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModelContextProtocol.Server;


var builder = Host.CreateEmptyApplicationBuilder(settings: null);

builder.Services.AddSingleton<IInventorConnectionService, InventorConnectionService>();

builder.Services.AddSingleton<IReadService, ReadService>();

builder.Services.AddSingleton<ISketchService, SketchService>();

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

await builder.Build().RunAsync();