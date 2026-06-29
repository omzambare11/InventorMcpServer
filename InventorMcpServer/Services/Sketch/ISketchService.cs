namespace InventorMcpServer.Services.Sketch;

public interface ISketchService
{
    string CreateLine(
        double startX,
        double startY,
        double endX,
        double endY);

    string CreateCircle(
        double centerX,
        double centerY,
        double radius);

    string CreateRectangle(
        double x,
        double y,
        double width,
        double height);
}