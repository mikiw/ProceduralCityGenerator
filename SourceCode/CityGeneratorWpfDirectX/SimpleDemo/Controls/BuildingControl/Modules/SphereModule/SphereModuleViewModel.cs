using Caliburn.Micro;
using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace SimpleGenerator
{
    public class SphereModuleViewModel : BaseModuleViewModel
    {
        double minRadius;
        public double MinRadius
        {
            get { return minRadius; }
            set
            {
                minRadius = Double.Parse(value.ToString());
                NotifyOfPropertyChange(() => MinRadius);
            }
        }

        double maxRadius;
        public double MaxRadius
        {
            get { return maxRadius; }
            set
            {
                maxRadius = Double.Parse(value.ToString());
                NotifyOfPropertyChange(() => MaxRadius);
            }
        }

        int minPhiDiv;
        public int MinPhiDiv
        {
            get { return minPhiDiv; }
            set
            {
                minPhiDiv = value;
                NotifyOfPropertyChange(() => MinPhiDiv);
            }
        }

        int maxPhiDiv;
        public int MaxPhiDiv
        {
            get { return maxPhiDiv; }
            set
            {
                maxPhiDiv = value;
                NotifyOfPropertyChange(() => MaxPhiDiv);
            }
        }

        int minThetaDiv;
        public int MinThetaDiv
        {
            get { return minThetaDiv; }
            set
            {
                minThetaDiv = value;
                NotifyOfPropertyChange(() => MinThetaDiv);
            }
        }

        int maxThetaDiv;
        public int MaxThetaDiv
        {
            get { return maxThetaDiv; }
            set
            {
                maxThetaDiv = value;
                NotifyOfPropertyChange(() => MaxThetaDiv);
            }
        }

        public SphereModuleViewModel()
        {
            Frequency = 1;
            minRadius = 1;
            maxRadius = 2;
            minPhiDiv = 2;
            maxPhiDiv = 12;
            minThetaDiv = 4;
            maxThetaDiv = 12;
        }

        public override GeometryModel3D CreateModel(double x, double y, double z, double height, Material m)
        {
            var newElement = new SphereVisual3D()
            {
                Radius = height / 2,
                PhiDiv = ProbabilityModule.randomSeed.Next(minPhiDiv,maxPhiDiv),
                ThetaDiv= ProbabilityModule.randomSeed.Next(minThetaDiv, maxThetaDiv),
                Material = m,
                Center = new Point3D(x, y, z - (height / 10)) // for sphere and cube connection - (height / 10) constant value
            };
            return newElement.Model;
        }

        public override MeshElement3D Create3dObject()
        {
            return new SphereVisual3D()
            {
                Radius = ProbabilityModule.NextDouble(minRadius, maxRadius),
                PhiDiv = ProbabilityModule.randomSeed.Next(minPhiDiv, maxPhiDiv),
                ThetaDiv = ProbabilityModule.randomSeed.Next(minThetaDiv, maxThetaDiv),
                Center = new System.Windows.Media.Media3D.Point3D(0, 0, 0),
                Material = MaterialHelper.CreateMaterial(Colors.WhiteSmoke)
            };
        }
    }
}
