using Inventor;

namespace InventorMcpServer.Services.Connection;

public interface IInventorConnectionService
{
    Application? Application { get; }

    bool Connect();

    PartDocument? GetActivePart();

    PlanarSketch CreateSketch();

    PlanarSketch? GetLastSketch();

    Profile? GetLastProfile();

    void ClearCurrentSketch();

    Face? GetFace(int index);

    SketchPoint? GetLastPoint();

    void SetLastPoint(SketchPoint point);

    void SetCurrentWorkPoint(WorkPoint point);

    WorkPoint? GetCurrentWorkPoint();

    WorkAxis? GetDefaultAxis();

    SketchPoint? GetLastSketchPoint();
    void SetLastSketchPoint(SketchPoint point);
}