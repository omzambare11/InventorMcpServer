using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorMcpServer.Services.Feature
{
    public interface IFeatureService
    {
        string Extrude(double distance);

        string TestFace(int index);

        string CreateHole(double diameter,double depth);

        string CreateWorkPoint(double x, double y, double z);

        string CreateThroughHole(double diameter);

        string CreateFillet(double radius);

        string CreateChamfer(double distance);

        string CreateCircularPattern(int count, double angle);

        string CreateMirror(bool removeOriginal = false);

        string CreateRectangularPattern(int xCount,double xSpacing,int yCount,double ySpacing);

        string CreateShell(double thickness);

        string CreateRevolve(double angle);

        string CreateDraft(double angle);
    }
}
