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

            var feature =compDef.Features.ExtrudeFeatures.Add(extrudeDef);

            _connection.SetLastFeature(feature);

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

            var feature =part.ComponentDefinition.Features
            .HoleFeatures
            .AddDrilledByThroughAllExtent(
                centers,
                diameter,
                PartFeatureExtentDirectionEnum.kPositiveExtentDirection);

            _connection.SetLastFeature(feature);

            part.Update();

            _connection.Application.ActiveView.Update();
           

            return $"Through Hole Created ({diameter})";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    public string CreateFillet(double radius)
    {
        try
        {
            var part = _connection.GetActivePart();

            if (part == null)
                return "No active part.";

            var app = _connection.Application!;

            var compDef = part.ComponentDefinition;

            EdgeCollection edges =
                app.TransientObjects.CreateEdgeCollection();

            foreach (SurfaceBody body in compDef.SurfaceBodies)
            {
                foreach (Edge edge in body.Edges)
                {
                    edges.Add(edge);
                }
            }

            if (edges.Count == 0)
                return "No edges found.";

            var feature =
            compDef.Features.FilletFeatures.AddSimple(
                edges,
                $"{radius} mm");

            _connection.SetLastFeature(feature);

            part.Update();

            app.ActiveView.Update();

            return $"Fillet ({radius} mm) created.";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    public string CreateChamfer(double distance)
    {
        try
        {
            var part = _connection.GetActivePart();

            if (part == null)
                return "No active part.";

            var app = _connection.Application!;
            var compDef = part.ComponentDefinition;

            EdgeCollection edges =
                app.TransientObjects.CreateEdgeCollection();

            foreach (SurfaceBody body in compDef.SurfaceBodies)
            {
                foreach (Edge edge in body.Edges)
                {
                    edges.Add(edge);
                }
            }

            if (edges.Count == 0)
                return "No edges found.";

            var feature =
            compDef.Features.ChamferFeatures.AddUsingDistance(
                edges,
                $"{distance} mm");

            _connection.SetLastFeature(feature);

            part.Update();
            app.ActiveView.Update();

            return $"Chamfer ({distance} mm) created.";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    public string TestFeatureCollection()
    {
        var collection = _connection.CreateFeatureCollection();

        return $"Features : {collection.Count}";
    }

    public string CreateCircularPattern(int count, double angle)
    {
        try
        {
            var part = _connection.GetActivePart();

            if (part == null)
                return "No active part.";

            var feature = _connection.GetLastFeature();

            if (feature == null)
                return "No last feature.";

            var axis = part.ComponentDefinition.WorkAxes[3];

            return
                $@"Feature : {feature.GetType().Name}
            Axis : {axis.Name}
            Count : {count}
            Angle : {angle}";
                    }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    public string CreateMirror(bool removeOriginal = false)
    {
        try
        {
            var part = _connection.GetActivePart();

            if (part == null)
                return "No active part.";

            var feature = _connection.GetLastFeature();

            if (feature == null)
                return "No feature available.";

            var app = _connection.Application!;

            ObjectCollection features =
                app.TransientObjects.CreateObjectCollection();

            features.Add(feature);

            // Default Origin Plane
            // 1 = YZ
            // 2 = XZ
            // 3 = XY
            WorkPlane mirrorPlane =
                part.ComponentDefinition.WorkPlanes[1];

            var mirror =
                part.ComponentDefinition.Features
                    .MirrorFeatures
                    .Add(
                        features,
                        mirrorPlane,
                        removeOriginal,
                        PatternComputeTypeEnum.kOptimizedCompute);

            _connection.SetLastFeature(mirror);

            part.Update();

            app.ActiveView.Update();

            return "Mirror Created Successfully.";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    public string CreateRectangularPattern(
    int xCount,
    double xSpacing,
    int yCount,
    double ySpacing)
    {
        try
        {
            var part = _connection.GetActivePart();

            if (part == null)
                return "No active part.";

            var feature = _connection.GetLastFeature();

            if (feature == null)
                return "No feature available.";

            var app = _connection.Application!;

            ObjectCollection features =
                app.TransientObjects.CreateObjectCollection();

            features.Add(feature);

            // Origin axes
            WorkAxis xAxis = part.ComponentDefinition.WorkAxes[1];
            WorkAxis yAxis = part.ComponentDefinition.WorkAxes[2];

            var pattern =
                part.ComponentDefinition.Features
                .RectangularPatternFeatures
                .Add(
                    features,
                    xAxis,
                    true,
                    xCount,
                    $"{xSpacing} mm",
                    PatternSpacingTypeEnum.kDefault,
                    Type.Missing,
                    yAxis,
                    true,
                    yCount,
                    $"{ySpacing} mm",
                    PatternSpacingTypeEnum.kDefault,
                    Type.Missing,
                    PatternComputeTypeEnum.kOptimizedCompute,
                    PatternOrientationEnum.kIdentical);

            _connection.SetLastFeature(pattern);

            part.Update();

            app.ActiveView.Update();

            return "Rectangular Pattern Created Successfully.";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }


    public string CreateShell(double thickness)
    {
        try
        {
            var part = _connection.GetActivePart();

            if (part == null)
                return "No active part.";

            var app = _connection.Application!;

            // Remove Top Face
            Face topFace = part.ComponentDefinition.SurfaceBodies[1].Faces[1];

            FaceCollection faces =
                app.TransientObjects.CreateFaceCollection();

            faces.Add(topFace);

            ShellDefinition definition =
                part.ComponentDefinition.Features
                    .ShellFeatures
                    .CreateDefinition(
                        faces,
                        Type.Missing,
                        $"{thickness} mm",
                        ShellDirectionEnum.kInsideShellDirection,
                        ShellMethodEnum.kSharpShellMethod,
                        Type.Missing);

            var shell =
                part.ComponentDefinition.Features
                    .ShellFeatures
                    .Add(definition);

            _connection.SetLastFeature(shell);

            part.Update();

            app.ActiveView.Update();

            return $"Shell Created ({thickness} mm)";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }


    public string CreateRevolve(double angle)
    {
        try
        {
            var part = _connection.GetActivePart();

            if (part == null)
                return "No active part.";

            var sketch = _connection.GetLastSketch();

            if (sketch == null)
                return "No sketch found.";

            // Create profile
            Profile profile = sketch.Profiles.AddForSolid();

            if (profile == null)
                return "No closed profile found.";

            // Find an axis line in sketch
            SketchLine axis = null;

            foreach (SketchEntity entity in sketch.SketchEntities)
            {
                if (entity is SketchLine line)
                {
                    axis = line;
                    break;
                }
            }

            if (axis == null)
                return "No axis line found in sketch.";

            var revolve =
                part.ComponentDefinition
                    .Features
                    .RevolveFeatures
                    .AddByAngle(
                        profile,
                        axis,
                        $"{angle} deg",
                        PartFeatureExtentDirectionEnum.kPositiveExtentDirection,
                        PartFeatureOperationEnum.kJoinOperation);

            _connection.SetLastFeature(revolve);

            part.Update();

            _connection.Application!.ActiveView.Update();

            return $"Revolved by {angle}° successfully.";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    public string CreateDraft(double angle)
    {
        try
        {
            var part = _connection.GetActivePart();

            if (part == null)
                return "No active part.";

            var app = _connection.Application!;
            var compDef = part.ComponentDefinition;

            FaceCollection faces =
                app.TransientObjects.CreateFaceCollection();

            // Temporary: use first side face
            faces.Add(compDef.SurfaceBodies[1].Faces[1]);

            // Temporary: use XY plane as neutral plane
            WorkPlane fixedPlane =
                compDef.WorkPlanes[3];

            var definition =
                compDef.Features
                       .FaceDraftFeatures
                       .CreateFaceDraftDefinition();

            definition.SetFixedPlane(
                faces,
                fixedPlane,
                $"{angle} deg");

            var feature =
                compDef.Features
                       .FaceDraftFeatures
                       .Add(definition);

            _connection.SetLastFeature(feature);

            part.Update();
            app.ActiveView.Update();

            return $"Draft ({angle}°) created.";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }
}