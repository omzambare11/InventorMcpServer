using InventorMcpServer.Services.Connection;
using InventorMcpServer.Services.Document;
using InventorMcpServer.Services.Geometry;
using InventorMcpServer.Services.Constraint;
using InventorMcpServer.Services.Sketch;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using InventorMcpServer.Services.Feature;

var builder = Host.CreateEmptyApplicationBuilder(settings: null);

builder.Services.AddSingleton<IInventorConnectionService, InventorConnectionService>();

builder.Services.AddSingleton<IDocumentService, DocumentService>();

builder.Services.AddSingleton<IGeometryService, GeometryService>();

builder.Services.AddSingleton<IConstraintService, ConstraintService>();

builder.Services.AddSingleton<ISketchService, SketchService>();

builder.Services.AddSingleton<IFeatureService, FeatureService>();

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

await builder.Build().RunAsync();