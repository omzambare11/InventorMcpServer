namespace InventorMcpServer.Services;

public interface IOldInventorService
{
    string CreateCircle(double centerX, double centerY, double radius);
    string CreateLine(double startX, double startY, double endX, double endY);
    string CreateRectangle(double x, double y, double width, double height);
    string GetActiveDocumentName();
    string GetSketches();
}