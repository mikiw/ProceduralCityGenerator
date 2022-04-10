using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGenerator
{
    // it works but need to be optimized
    // create torus in x,y,z
    // circle equasion
    // maybe crete elliptic curve?
    //private GeometryModel3D CreateTorus(Point3D startPosition, double r, double meshDelta, double diameter)
    //{
    // to na razie równanie okręgu ale może by z tego zrobić krzywą eliptyczną?

    //    var path = new Point3DCollection();

    //    //start point
    //    path.Add(new Point3D(startPosition.X + (meshDelta / 2), startPosition.Y - r, startPosition.Z));

    //    for (double y = -r; y <= r; y += meshDelta)
    //    {
    //        var x1circle = -Math.Pow((-Math.Pow(y, 2) + Math.Pow(r, 2)), 0.5);
    //        path.Add(new Point3D(x1circle + startPosition.X, y + startPosition.Y, startPosition.Z));
    //    }

    //    for (double y = +r; y >= -r; y -= meshDelta)
    //    {
    //        var x2circle = +Math.Pow((-Math.Pow(y, 2) + Math.Pow(r, 2)), 0.5);
    //        path.Add(new Point3D(x2circle + startPosition.X, y + startPosition.Y, startPosition.Z));
    //    }

    //    //finish point
    //    path.Add(new Point3D(startPosition.X - (meshDelta / 2), startPosition.Y - r, startPosition.Z));

    //    TubeVisual3D tube = new TubeVisual3D()
    //    {
    //        Diameter = diameter,
    //        ThetaDiv = 60,
    //        Path = path
    //    };

    //    return tube.Model;
    //}
}
