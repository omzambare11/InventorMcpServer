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
}