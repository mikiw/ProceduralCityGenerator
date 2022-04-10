using Caliburn.Micro;
using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace SimpleGenerator
{
    public abstract class BaseModuleViewModel : PropertyChangedBase
    {
        int frequency;
        public int Frequency
        {
            get { return frequency; }
            set
            {
                frequency = value;
                NotifyOfPropertyChange(() => Frequency);
            }
        }

        PreviewControlViewModel previewControlViewModel;
        public PreviewControlViewModel PreviewControlViewModel
        {
            get { return previewControlViewModel; }
            set
            {
                previewControlViewModel = value;
                NotifyOfPropertyChange(() => PreviewControlViewModel);
            }
        }

        public BaseModuleViewModel()
        {
            Frequency = 1;
            DispatcherTimerSetup();
        }

        //refresh random model every 5 seconds
        private void DispatcherTimerSetup()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(5);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        public void Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        public void Refresh()
        {
            try
            {
                PreviewControlViewModel = new PreviewControlViewModel(Create3dObject().Model);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public abstract GeometryModel3D CreateModel(double x, double y, double z, double height, Material m);

        public abstract MeshElement3D Create3dObject();
    }
}
