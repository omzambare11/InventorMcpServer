using InventorMcpServer.Services;

var service = new InventorService();

Console.WriteLine(service.CreateLine(0, 0, 10, 10));

Console.ReadKey();