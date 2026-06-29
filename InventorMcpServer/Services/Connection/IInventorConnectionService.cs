using Inventor;

namespace InventorMcpServer.Services.Connection;

public interface IInventorConnectionService
{
    Application? Application { get; }

    bool Connect();

    PartDocument? GetActivePart();

    PlanarSketch? GetSketch();
}