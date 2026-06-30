using InventorMcpServer.Models;
using InventorMcpServer.Services.Connection;

namespace InventorMcpServer.Services.Constraint;

public class ConstraintService : IConstraintService
{
    private readonly IInventorConnectionService _connection;

    public ConstraintService(IInventorConnectionService connection)
    {
        _connection = connection;
    }

    public List<DimensionModel> ReadDimensions()
    {
        return new List<DimensionModel>();
    }

    public List<ConstraintModel> ReadConstraints()
    {
        return new List<ConstraintModel>();
    }
}