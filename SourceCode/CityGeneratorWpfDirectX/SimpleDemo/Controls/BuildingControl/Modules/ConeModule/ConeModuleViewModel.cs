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
    public class ConeModuleViewModel : BaseModuleViewModel
    {
        double minBaseRadius;
        public double MinBaseRadius
        {
            get { return minBaseRadius; }
            set
            {
                minBaseRadius = Double.Parse(value.ToString());
                NotifyOfPropertyChange(() => MinBaseRadius);
            }
        }

        double maxBaseRadius;
        public double MaxBaseRadius
        {
            get { return maxBaseRadius; }
            set
            {
                maxBaseRadius = Double.Parse(value.ToString());
                NotifyOfPropertyChange(() => MaxBaseRadius);
            }
        }

        double minTopRadius;
        public double MinTopRadius
        {
            get { return minTopRadius; }
            set
            {
                minTopRadius = Double.Parse(value.ToString());
                NotifyOfPropertyChange(() => MinTopRadius);
            }
        }

        double maxTopRadius;
        public double MaxTopRadius
        {
            get { return maxTopRadius; }
            set
            {
                maxTopRadius = Double.Parse(value.ToString());
                NotifyOfPropertyChange(() => MaxTopRadius);
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

        double minHeight;
        public double MinHeight
        {
            get { return minHeight; }
            set
            {
                minHeight = value;
                NotifyOfPropertyChange(() => MinHeight);
            }
        }

        double maxHeight;
        public double MaxHeight
        {
            get { return maxHeight; }
            set
            {
                maxHeight = value;
                NotifyOfPropertyChange(() => MaxHeight);
            }
        }

        public ConeModuleViewModel()
        {
            Frequency = 1;
            minBaseRadius = 2;
            maxBaseRadius = 4;
            minTopRadius = 2;
            maxTopRadius = 4;
            minThetaDiv = 4;
            maxThetaDiv = 8;
            minHeight = 1;
            maxHeight = 4;
        }

        public override GeometryModel3D CreateModel(double x, double y, double z, double height, Material m)
        {
            var newElement = new TruncatedConeVisual3D()
            {
                Height = height,
                BaseCap = true,
                BaseRadius = ProbabilityModule.NextDouble(minBaseRadius, maxBaseRadius),
                TopRadius = ProbabilityModule.NextDouble(minTopRadius, maxTopRadius),
                TopCap = true,
                ThetaDiv = ProbabilityModule.randomSeed.Next(minThetaDiv, maxThetaDiv),
                Origin = new Point3D(x, y, z - (height / 2)),
                Material = m
            };
            return newElement.Model;
        }

        public override MeshElement3D Create3dObject()
        {
            var height = ProbabilityModule.NextDouble(minBaseRadius, maxBaseRadius);
            return new TruncatedConeVisual3D()
            {
                Height = height,
                BaseCap = true,
                BaseRadius = ProbabilityModule.NextDouble(minBaseRadius, maxBaseRadius),
                TopRadius = ProbabilityModule.NextDouble(minTopRadius, maxTopRadius),
                TopCap = true,
                ThetaDiv = ProbabilityModule.randomSeed.Next(minThetaDiv, maxThetaDiv),
                Origin = new Point3D(0, 0, 0 - (height / 2)),
                Material = MaterialHelper.CreateMaterial(Colors.WhiteSmoke)
            };
        }
    }
}
