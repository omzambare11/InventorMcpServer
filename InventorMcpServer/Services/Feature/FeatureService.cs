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

    public string TestFace(int index)
    {
        var face = _connection.GetFace(index);

        if (face == null)
            return "Face Not Found";

        return face.SurfaceType.ToString();
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


    public string CreateWorkPoint(double x, double y, double z)
    {
        try
        {
            var part = _connection.GetActivePart();

            if (part == null)
                return "No active part.";

            var tg = _connection.Application!.TransientGeometry;

            Point point = tg.CreatePoint(x, y, z);

            WorkPoint wp = part.ComponentDefinition.WorkPoints.AddFixed(point);

            _connection.SetCurrentWorkPoint(wp);

            return "WorkPoint created successfully.";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string CreateHole(double diameter, double depth)
    {
        throw new NotImplementedException();
    }


    //testing only
    public string TestAxis()
    {
        var axis = _connection.GetDefaultAxis();

        if (axis == null)
            return "No Axis";

        return "Axis Found";
    }

    public string CreateThroughHole(double diameter)
    {
        try
        {
            var part = _connection.GetActivePart();

            if (part == null)
                return "No active part.";

            var point = _connection.GetLastSketchPoint();

            if (point == null)
                return "Create a sketch point first.";

            var app = _connection.Application!;

            ObjectCollection centers =
                app.TransientObjects.CreateObjectCollection();

            centers.Add(point);

            part.ComponentDefinition.Features
                .HoleFeatures
                .AddDrilledByThroughAllExtent(
                    centers,
                    $"{diameter} mm",
                    PartFeatureExtentDirectionEnum.kNegativeExtentDirection);

            part.Update();

            _connection.Application.ActiveView.Update();
           

            return $"Through Hole Created ({diameter})";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }
}