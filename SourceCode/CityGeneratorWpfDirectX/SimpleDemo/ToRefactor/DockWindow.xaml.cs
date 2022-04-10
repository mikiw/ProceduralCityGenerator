using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DockWindow : Window
    {
        public string Theme { get; set; }

        // IUnityContainer container;
        // myServiceInstance = myContainer.Resolve<IMyService>();
        private PreviewControlViewModel previewViewModel;

        private CityGenerator cityGenerator;
        private TextureControl tc;

        public DockWindow()
        {
            InitializeComponent();

            ThemeManager.ApplicationThemeName = "MetropolisDark";

            tc = new TextureControl();

            //container = new UnityContainer();
            //container.RegisterType<ISphereModule, SphereModule>("Sphere");
            //container.RegisterType<SphereVisual3D, SphereModule>("SphereModuleImplementation");

            floatInTheAir_Click(null, null);
            Button_Generate_Click(null, null);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ApplicationThemeName = (sender as BarButtonItem).Content.ToString();
        }

        private void Button_Generate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cityGenerator = new CityGenerator((textureControl.DataContext as TextureControlViewModel).TextureCollection, this);
                var city = cityGenerator.GenerateCity();
                //city.Freeze();

                previewViewModel = new PreviewControlViewModel(city); // this is for now for all configuration, do mvvm later
                mainPreviewControl.DataContext = previewViewModel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "error");
            }
        }

        private void Button_Export_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            var uriPaths = (tc.DataContext as TextureControlViewModel).TextureCollection.Select(x => x.ImageSource).ToList<string>();
            var texturePaths = new List<string>();

            foreach (var mat in uriPaths)
                texturePaths.Add(mat.Replace(@"file:///", ""));

            try
            {
                Manager.ExportModel3d(dialog.SelectedPath, previewViewModel.Model, texturePaths);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "error");
            }
        }

        private void Button_Import_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                throw new NotImplementedException();

                var path = @"G:\Mikolaj\Dropbox\Tool\SimpleDemo\SimpleDemo\bin\nice City\exportedModel.obj";
                var model = Manager.ImportScene(path);

                previewViewModel.Model = model;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "error");
            }
        }

        private void floatInTheAir_Click(object sender, RoutedEventArgs e)
        {
            if (floatInTheAir.IsChecked.Value == true)
            {
                minFloatHeight.IsEnabled = true;
                maxFloatHeight.IsEnabled = true;
            }
            else
            {
                minFloatHeight.IsEnabled = false;
                maxFloatHeight.IsEnabled = false;
            }
        }

        private void normalBuildingProbability_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ProbabilityModule.normalBuildingProbability = int.Parse(normalBuildingProbability.Text);
        }

        private void dynamicBuildingProbability_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ProbabilityModule.dynamicBuildingProbability = int.Parse(dynamicBuildingProbability.Text);
        }

        private void probabilityFactor_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ProbabilityModule.deltaForDoubleRandom = double.Parse(probabilityFactor.Text, System.Globalization.CultureInfo.InvariantCulture);
        }


    }
}
