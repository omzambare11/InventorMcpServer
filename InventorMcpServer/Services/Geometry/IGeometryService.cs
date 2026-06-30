using InventorMcpServer.Models;

namespace InventorMcpServer.Services.Geometry;

public interface IGeometryService
{
    List<LineModel> ReadLines();

    List<CircleModel> ReadCircles();

    List<ArcModel> ReadArcs();

    List<PointModel> ReadPoints();

    List<FaceModel> ReadFaces();
}