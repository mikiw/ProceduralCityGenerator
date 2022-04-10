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
    public class BoxModuleViewModel : BaseModuleViewModel
    {
        double minWidth;
        public double MinWidth
        {
            get { return minWidth; }
            set
            {
                minWidth = Double.Parse(value.ToString());
                NotifyOfPropertyChange(() => MinWidth);
            }
        }

        double maxWidth;
        public double MaxWidth
        {
            get { return maxWidth; }
            set
            {
                maxWidth = Double.Parse(value.ToString());
                NotifyOfPropertyChange(() => MaxWidth);
            }
        }

        double minLength;
        public double MinLength
        {
            get { return minLength; }
            set
            {
                minLength = Double.Parse(value.ToString());
                NotifyOfPropertyChange(() => MinLength);
            }
        }

        double maxLength;
        public double MaxLength
        {
            get { return maxLength; }
            set
            {
                maxLength = Double.Parse(value.ToString());
                NotifyOfPropertyChange(() => MaxLength);
            }
        }

        double minHeight;
        public double MinHeight
        {
            get { return minHeight; }
            set
            {
                minHeight = Double.Parse(value.ToString());
                NotifyOfPropertyChange(() => MinHeight);
            }
        }

        double maxHeight;
        public double MaxHeight
        {
            get { return maxHeight; }
            set
            {
                maxHeight = Double.Parse(value.ToString());
                NotifyOfPropertyChange(() => MaxHeight);
            }
        }

        public BoxModuleViewModel()
        {
            Frequency = 20;
            minWidth = 1;
            maxWidth = 3;
            minLength = 1;
            maxLength = 3;
            minHeight = 1;
            maxHeight = 3;
        }

        public override GeometryModel3D CreateModel(double x, double y, double z, double height, Material m)
        {
            var newElement = new BoxVisual3D()
            {
                Width = ProbabilityModule.NextDouble(minWidth, maxWidth),
                Length = ProbabilityModule.NextDouble(minLength, maxLength),
                Height = height,
                Material = m,
                Center = new Point3D(x, y, z)
            };
            return newElement.Model;
        }

        public override MeshElement3D Create3dObject()
        {
            return new BoxVisual3D()
            {
                Width = ProbabilityModule.NextDouble(minWidth, maxWidth),
                Length = ProbabilityModule.NextDouble(minLength, maxLength),
                Height = ProbabilityModule.NextDouble(minHeight, maxHeight),
                Center = new System.Windows.Media.Media3D.Point3D(0, 0, 0),
                Material = MaterialHelper.CreateMaterial(Colors.WhiteSmoke)
            };
        }
    }
}
