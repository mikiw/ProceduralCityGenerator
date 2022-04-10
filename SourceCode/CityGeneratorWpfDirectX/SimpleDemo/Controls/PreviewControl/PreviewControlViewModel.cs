using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using System.IO;
using Microsoft.Win32;

namespace SimpleGenerator
{
    public class PreviewControlViewModel : PropertyChangedBase
    {
        //Point3D cameraPosition;
        //public Point3D CameraPosition
        //{
        //    get { return cameraPosition; }
        //    set
        //    {
        //        cameraPosition = value;
        //        NotifyOfPropertyChange(() => CameraPosition);
        //    }
        //}

        //Vector3D cameraLookDirection;
        //public Vector3D CameraLookDirection
        //{
        //    get { return cameraLookDirection; }
        //    set
        //    {
        //        cameraLookDirection = value;
        //        NotifyOfPropertyChange(() => CameraLookDirection);
        //    }
        //}

        Model3D model;
        public Model3D Model
        {
            get { return model; }
            set
            {
                model = value;
                NotifyOfPropertyChange(() => Model);
            }
        }

        public PreviewControlViewModel(Model3D model)
        {
            this.Model = model;
        }
    }
}
