using Inventor;

using InventorMcpServer.Services.Connection;
using System.Diagnostics;

namespace InventorMcpServer.Services.Feature;

public class FeatureService : IFeatureService
{
    private readonly IInventorConnectionService _connection;

    public FeatureService(IInventorConnectionService connection)
    {
        _connection = connection;
    }


    public string Extrude(double distance)
{
    try
    {
            var part = _connection.GetActivePart();

            if (part == null)
                return "No active part.";

            var sketch = _connection.GetLastSketch();

            if (sketch == null)
                return "No sketch found.";

            Profile profile = sketch.Profiles.AddForSolid();

            if (profile == null)
            return "No closed profile found.";

        PartComponentDefinition compDef = part.ComponentDefinition;

        ExtrudeDefinition extrudeDef =
            compDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(
                profile,
                PartFeatureOperationEnum.kJoinOperation);

        extrudeDef.SetDistanceExtent(
            distance,
            PartFeatureExtentDirectionEnum.kPositiveExtentDirection);

        compDef.Features.ExtrudeFeatures.Add(extrudeDef);

        part.Update();

            _connection.ClearCurrentSketch();

            return $"Extruded by {distance} successfully.";
    }
    catch (Exception ex)
    {
        return ex.Message;
    }
}

}