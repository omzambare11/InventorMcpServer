using InventorMcpServer.Models;

namespace InventorMcpServer.Services.Constraint;

public interface IConstraintService
{
    List<DimensionModel> ReadDimensions();

    List<ConstraintModel> ReadConstraints();
}