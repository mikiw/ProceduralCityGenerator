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
    public class PipeModuleViewModel : BaseModuleViewModel
    {
        double minDiameter;
        public double MinDiameter
        {
            get { return minDiameter; }
            set
            {
                minDiameter = Double.Parse(value.ToString());
                NotifyOfPropertyChange(() => MinDiameter);
            }
        }

        double maxDiameter;
        public double MaxDiameter
        {
            get { return maxDiameter; }
            set
            {
                maxDiameter = Double.Parse(value.ToString());
                NotifyOfPropertyChange(() => MaxDiameter);
            }
        }

        double minInnerDiameter;
        public double MinInnerDiameter
        {
            get { return minDiameter; }
            set
            {
                minInnerDiameter = Double.Parse(value.ToString());
                NotifyOfPropertyChange(() => MinInnerDiameter);
            }
        }

        double maxInnerDiameter;
        public double MaxInnerDiameter
        {
            get { return maxInnerDiameter; }
            set
            {
                maxInnerDiameter = Double.Parse(value.ToString());
                NotifyOfPropertyChange(() => MaxInnerDiameter);
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

        public PipeModuleViewModel()
        {
            Frequency = 1;
            minDiameter = 2;
            maxDiameter = 3;
            minInnerDiameter = 0.5;
            MaxInnerDiameter = 1.5;
            minThetaDiv = 4;
            maxThetaDiv = 12;
            minHeight = 0.5;
            maxHeight = 2;
        }

        public override GeometryModel3D CreateModel(double x, double y, double z, double height, Material m)
        {
            var newElement = new PipeVisual3D()
            {
                ThetaDiv = ProbabilityModule.randomSeed.Next(minThetaDiv, maxThetaDiv),
                Diameter = ProbabilityModule.NextDouble(minDiameter, maxDiameter),
                InnerDiameter = ProbabilityModule.NextDouble(minInnerDiameter, maxInnerDiameter),
                Material = m,
                Point1 = new Point3D(x, y, z - height / 2),
                Point2 = new Point3D(x, y, z + height / 2)
            };

            return newElement.Model;
        }

        public override MeshElement3D Create3dObject()
        {
            return new PipeVisual3D()
            {
                ThetaDiv = ProbabilityModule.randomSeed.Next(minThetaDiv, maxThetaDiv),
                Diameter = ProbabilityModule.NextDouble(minDiameter, maxDiameter),
                InnerDiameter = ProbabilityModule.NextDouble(minInnerDiameter, maxInnerDiameter),
                Material = MaterialHelper.CreateMaterial(Colors.WhiteSmoke),
                Point1 = new Point3D(0, 0, 0 - 0.5),
                Point2 = new Point3D(0, 0, 0 + 0.5)
            };
        }
    }
}
