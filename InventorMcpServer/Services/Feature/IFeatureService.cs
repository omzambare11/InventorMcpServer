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
    }
}
