using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace SimpleGenerator
{
    public class CityGenerator
    {
        private ObservableCollection<TextureItemDataModel> materials;
        private DockWindow parametersFromView; 
        // TODO: refactor this shit with MainWindow as parametersFromView - it sux. Automapper to map objects to parameters as object or something like this

        public CityGenerator(ObservableCollection<TextureItemDataModel> materials, DockWindow parametersFromView)
        {
            this.materials = materials;
            this.parametersFromView = parametersFromView;
        }

        public Model3DGroup GenerateCity()
        {
            // Create a model group
            var modelGroup = new Model3DGroup();

            for (int i = 0; i < Convert.ToInt32(parametersFromView.iterations.Text); i++)
            {
                double positionX = 0;
                double positionY = 0;

                double cityCenterX = Convert.ToDouble(parametersFromView.minCenterMapSize.Text);
                double cityCenterY = Convert.ToDouble(parametersFromView.maxCenterMapSize.Text);

                double middleCityCenterX = Convert.ToDouble(parametersFromView.minMiddleMapSize.Text);
                double middleCityCenterY = Convert.ToDouble(parametersFromView.maxMiddleMapSize.Text);

                double amplitude = Math.Abs(Convert.ToDouble(parametersFromView.minMapSize.Text)) + Math.Abs(Convert.ToDouble(parametersFromView.maxMapSize.Text));

                //Spaces between
                double space = Convert.ToDouble(parametersFromView.spacesBetween.Text);

                bool isInCircle = false;

                if (space == 0)
                {
                    //MessageBox.Show("Space can not be 0");
                    space = 1;
                }


                if (parametersFromView.combobox.SelectedItem == parametersFromView.combobox.Items[0])
                {
                    //Grid City
                    positionX = (ProbabilityModule.randomSeed.Next(Convert.ToInt32(parametersFromView.minMapSize.Text), Convert.ToInt32(parametersFromView.maxMapSize.Text)));
                    positionY = (ProbabilityModule.randomSeed.Next(Convert.ToInt32(parametersFromView.minMapSize.Text), Convert.ToInt32(parametersFromView.maxMapSize.Text)));
                }
                else if (parametersFromView.combobox.SelectedItem == parametersFromView.combobox.Items[1])
                {
                    //Grid Random City
                    positionX = ProbabilityModule.NextDouble(Convert.ToDouble(parametersFromView.minMapSize.Text), Convert.ToDouble(parametersFromView.maxMapSize.Text));
                    positionY = ProbabilityModule.NextDouble(Convert.ToDouble(parametersFromView.minMapSize.Text), Convert.ToDouble(parametersFromView.maxMapSize.Text));
                }
                else if (parametersFromView.combobox.SelectedItem == parametersFromView.combobox.Items[2])
                {
                    //Circle City
                    positionX = (ProbabilityModule.randomSeed.Next(Convert.ToInt32(parametersFromView.minMapSize.Text), Convert.ToInt32(parametersFromView.maxMapSize.Text)));
                    positionY = (ProbabilityModule.randomSeed.Next(Convert.ToInt32(parametersFromView.minMapSize.Text), Convert.ToInt32(parametersFromView.maxMapSize.Text)));

                    double distance = Point.Subtract(new Point(0,0), new Point(positionX, positionY)).Length;
                    double maxR = Math.Abs(Convert.ToDouble(parametersFromView.minMapSize.Text) - Convert.ToDouble(parametersFromView.maxMapSize.Text)) / 2;

                    if (distance < maxR)
                    {
                        isInCircle = true;
                    }
                }
                else if (parametersFromView.combobox.SelectedItem == parametersFromView.combobox.Items[3])
                {
                    //Circle Random City
                    positionX = ProbabilityModule.NextDouble(Convert.ToDouble(parametersFromView.minMapSize.Text), Convert.ToDouble(parametersFromView.maxMapSize.Text));
                    positionY = ProbabilityModule.NextDouble(Convert.ToDouble(parametersFromView.minMapSize.Text), Convert.ToDouble(parametersFromView.maxMapSize.Text));

                    double distance = Point.Subtract(new Point(0, 0), new Point(positionX, positionY)).Length;
                    double maxR = Math.Abs(Convert.ToDouble(parametersFromView.minMapSize.Text) - Convert.ToDouble(parametersFromView.maxMapSize.Text)) / 2;

                    if (distance < maxR)
                    {
                        isInCircle = true;
                    }
                }

                //Correction Shift
                if (space > 1)
                {
                    positionX *= space;
                    positionY *= space;
                }

                //Normal Height
                double height = ProbabilityModule.randomSeed.Next(Convert.ToInt32(parametersFromView.minHeight.Text), Convert.ToInt32(parametersFromView.maxHeight.Text));

                //City Center Height
                if (positionX > cityCenterX && positionX < cityCenterY)
                    if (positionY > cityCenterX && positionY < cityCenterY)
                        height *= Convert.ToDouble(parametersFromView.cityCenterHeightMultiplayer.Text);

                //Middle City Height
                if (positionX > middleCityCenterX && positionX < middleCityCenterY)
                    if (positionY > middleCityCenterX && positionY < middleCityCenterY)
                        height *= Convert.ToDouble(parametersFromView.middleCityCenterHeightMultiplayer.Text);

                var chance = ProbabilityModule.randomSeed.Next(0, ProbabilityModule.dynamicBuildingProbability + ProbabilityModule.normalBuildingProbability);

                // City plan
                if (parametersFromView.combobox.SelectedItem == parametersFromView.combobox.Items[2] ||
                    parametersFromView.combobox.SelectedItem == parametersFromView.combobox.Items[3])
                {
                    if (isInCircle == true)
                    {
                        if (chance < ProbabilityModule.dynamicBuildingProbability)
                            GenerateDynamicTower(height * 2, modelGroup, positionX, positionY, 
                                ProbabilityModule.NextDouble(Convert.ToDouble(parametersFromView.minAngle.Text), 
                                Convert.ToDouble(parametersFromView.maxAngle.Text)), 
                                Convert.ToDouble(parametersFromView.maxBaseSize.Text),
                                Convert.ToDouble(parametersFromView.minBaseSize.Text)
                                );
                        else
                            GenerateBuilding(height, modelGroup, positionX, positionY);
                    }
                    else
                        i--; // -1 from i coz there will be no building builded
                }
                else
                {
                    if (chance < ProbabilityModule.dynamicBuildingProbability)
                        GenerateDynamicTower(height * Convert.ToInt32(parametersFromView.dynamicHeightFactor.Text), modelGroup, positionX, positionY, 
                            ProbabilityModule.NextDouble(Convert.ToDouble(parametersFromView.minAngle.Text),
                                Convert.ToDouble(parametersFromView.maxAngle.Text)),
                                Convert.ToDouble(parametersFromView.maxBaseSize.Text),
                                Convert.ToDouble(parametersFromView.minBaseSize.Text)
                            );
                    else
                        GenerateBuilding(height, modelGroup, positionX, positionY);
                }

                //TODO:
                //GenerateDynamicTower
                //GenerateAnimatedDynamicTower
                //GenerateBuildingOfHeightWithRandomElements
                //GenerateBuildingOfHeightWithMaxNumberOfElements
            }

            return modelGroup;
        }

        private Material GetRandomEnabledMaterial()
        {
            List<TextureItemDataModel> listWithEnabledTextures = materials.Where(x => x.Enable == true).ToList();

            return listWithEnabledTextures[(ProbabilityModule.randomSeed.Next(0, listWithEnabledTextures.Count))].Material;
        }

        private List<BaseModuleViewModel> GetModulesWithFrequency()
        {
            //Modules witf Frequency as list with VM references
            List<BaseModuleViewModel> modulesWithFrequency = new List<BaseModuleViewModel>();

            //TODO: dynamicly generation of viewmodels and view in runtime using reflection based on list of parameters or something

            List<BaseModuleViewModel> modules = new List<BaseModuleViewModel>();
            modules.Add((parametersFromView.boxModule.DataContext as BoxModuleViewModel));
            modules.Add((parametersFromView.sphereModule.DataContext as SphereModuleViewModel));
            modules.Add((parametersFromView.coneModule.DataContext as ConeModuleViewModel));
            modules.Add((parametersFromView.pipeModule.DataContext as PipeModuleViewModel));

            foreach (var module in modules)
                for (int i = 0; i < module.Frequency; i++)
                    modulesWithFrequency.Add(module); // it's just ref

            return modulesWithFrequency; // change to yeld later
        }

        private void GenerateBuilding(double height, Model3DGroup modelGroup, double positionX, double positionY)
        {
            // Signle texture per building        
            Material material = GetRandomEnabledMaterial();

            var modulesWithFrequency = GetModulesWithFrequency();

            double totalHeight = 0;

            for (double j = 0; j <= height; j++)
            {
                var module = modulesWithFrequency[ProbabilityModule.randomSeed.Next(0, modulesWithFrequency.Count)];

                // TODO: IOC resolver to this section of code instead of if sections
                if (module is BoxModuleViewModel)
                {
                    var vm = (parametersFromView.boxModule.DataContext as BoxModuleViewModel);
                    var heightOfElement = ProbabilityModule.NextDouble(vm.MinHeight, vm.MaxHeight);

                    var model = (parametersFromView.boxModule.DataContext as BoxModuleViewModel).
                        CreateModel(positionX, positionY, totalHeight + (heightOfElement / 2), heightOfElement, material);
                    modelGroup.Children.Add(model);
                    totalHeight += heightOfElement;
                }
                else if (module is SphereModuleViewModel)
                {
                    var vm = (parametersFromView.sphereModule.DataContext as SphereModuleViewModel);
                    var radius = ProbabilityModule.NextDouble(vm.MinRadius, vm.MaxRadius);
                    var heightOfElement = (radius * 2);

                    modelGroup.Children.Add((parametersFromView.sphereModule.DataContext as SphereModuleViewModel).
                        CreateModel(positionX, positionY, totalHeight + (heightOfElement / 2), heightOfElement, material));
                    totalHeight += heightOfElement - heightOfElement / 10;// for not sphere cube connection
                }
                else if (module is ConeModuleViewModel)
                {
                    var vm = (parametersFromView.coneModule.DataContext as ConeModuleViewModel);
                    var heightOfElement = ProbabilityModule.NextDouble(vm.MinHeight, vm.MaxHeight);

                    var model = (parametersFromView.coneModule.DataContext as ConeModuleViewModel).
                        CreateModel(positionX, positionY, totalHeight + (heightOfElement / 2), heightOfElement, material);
                    modelGroup.Children.Add(model);
                    totalHeight += heightOfElement;
                }
                else if (module is PipeModuleViewModel)
                {
                    var vm = (parametersFromView.pipeModule.DataContext as PipeModuleViewModel);
                    var heightOfElement = ProbabilityModule.NextDouble(vm.MinHeight, vm.MaxHeight);

                    var model = (parametersFromView.pipeModule.DataContext as PipeModuleViewModel).
                        CreateModel(positionX, positionY, totalHeight + (heightOfElement / 2), heightOfElement, material);
                    modelGroup.Children.Add(model);
                    totalHeight += heightOfElement;
                }

                if (parametersFromView.floatInTheAir.IsChecked.Value)
                    totalHeight += ProbabilityModule.NextDouble(
                        double.Parse(parametersFromView.minFloatHeight.Text, CultureInfo.InvariantCulture), 
                        double.Parse(parametersFromView.maxFloatHeight.Text, CultureInfo.InvariantCulture)
                        );
                                
                // CreateAntennas at the top od building
                // Commented coz it's not ready to use 
                //if (j  == height && true)
                //    CreateAntennas(positionX, positionY, totalHeight, material, modelGroup);
            }
        }

        private void CreateAntennas(double positionX, double positionY, double j, Material material, Model3DGroup modelGroup)
        {
            for (int i = 0; i < ProbabilityModule.randomSeed.Next(5, 15); i++)
            {
                var x = ProbabilityModule.NextDouble(-0.5, 0.5);
                var y = ProbabilityModule.NextDouble(-0.5, 0.5);
                double antennaHeight = ProbabilityModule.NextDouble(1, 10);

                PipeVisual3D pipe = new PipeVisual3D()
                {
                    Point1 = new Point3D(positionX + x, positionY + y, j - 0.5),
                    Point2 = new Point3D(positionX + x, positionY + y, antennaHeight + 0.5),
                    ThetaDiv = 10,
                    InnerDiameter = 0,
                    Diameter = 0.05,
                    Material = material
                };
                modelGroup.Children.Add(pipe.Model);
            }
        }

        private void GenerateDynamicTower(double height, Model3DGroup modelGroup, double x, double y, double angle, double maxWidth, double minWidth)
        {
            var material = GetRandomEnabledMaterial();
            var thetaDiv = ProbabilityModule.randomSeed.Next(Convert.ToInt32(parametersFromView.minConeThetaDiv.Text), Convert.ToInt32(parametersFromView.maxConeThetaDiv.Text));
            var chance = ProbabilityModule.randomSeed.Next(0, 2);
            
            double deltaPerIteration;
            double delta;

            if ((maxWidth - minWidth) != 0)
            {
                deltaPerIteration = Math.Abs(maxWidth - minWidth) / height;
                delta = 0;
            }
            else
            {
                deltaPerIteration = 0;
                delta = maxWidth;
            }

            double buildingHeight = (0.5 * Convert.ToDouble(parametersFromView.zSize.Text));

            for (double j = 0; j < height; j += buildingHeight)
            {
                #region animation test
                //DoubleAnimation anim = new DoubleAnimation
                //{
                //    From = 0,
                //    To = 360,
                //    RepeatBehavior = RepeatBehavior.Forever
                //};

                //myAxisAngleRotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, anim);


                //var blinkStoryboard = new Storyboard
                //{
                //    Duration = TimeSpan.FromMilliseconds(500),
                //    RepeatBehavior = RepeatBehavior.Forever,
                //    AutoReverse = true
                //};

                //Storyboard.SetTarget(anim, MyCanvas);
                //Storyboard.SetTargetProperty(anim, new PropertyPath(OpacityProperty));
                //blinkStoryboard.Children.Add(anim);
                //MyCanvas.BeginStoryboard(blinkStoryboard);

                //rotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, angleAnimation);

                //var storyboard = new Storyboard();
                //var totalDuration = TimeSpan.Zero;


                //var rotation = new AxisAngleRotation3D(new Vector3D(0, 0, 1),10);

                //var transform = new RotateTransform3D(rotation, new Point3D(0, 0, 0));
                //var duration = TimeSpan.FromMilliseconds(100);
                //var animation = new DoubleAnimation(0, 360, duration);
                //animation.RepeatBehavior = RepeatBehavior.Forever;

                //Storyboard.SetTarget(animation, rotation);
                //Storyboard.SetTargetProperty(animation, new PropertyPath(AxisAngleRotation3D.AngleProperty));

                //storyboard.Children.Add(animation);
                //storyboard.Begin();
                #endregion

                RotateTransform3D myRotateTransform3D = new RotateTransform3D();
                AxisAngleRotation3D myAxisAngleRotation3d = new AxisAngleRotation3D();
                myAxisAngleRotation3d.Axis = new Vector3D(0, 0, 1);
                myAxisAngleRotation3d.Angle = j * angle;
                myRotateTransform3D.Rotation = myAxisAngleRotation3d;
                myRotateTransform3D.CenterX = x;
                myRotateTransform3D.CenterY = y;
                myRotateTransform3D.CenterZ = 0;

                if (delta != maxWidth)
                    delta += deltaPerIteration;

                double size = (maxWidth - delta + minWidth) * Convert.ToDouble(parametersFromView.xSize.Text);

                if (chance == 0)
                {
                    
                    var mesh = new BoxVisual3D() {
                        Width = size,
                        Length = 1 * Convert.ToDouble(parametersFromView.ySize.Text),
                        Height = buildingHeight,
                        Center = new Point3D(x,y,j + buildingHeight),
                        Material = material
                        };

                    mesh.Model.Transform = myRotateTransform3D;
                    modelGroup.Children.Add(mesh.Model);
                }
                else if (chance == 1)
                {
                    var mesh = new TruncatedConeVisual3D() {
                        Height = buildingHeight,
                        BaseCap = true,
                        BaseRadius = size,
                        TopRadius = size - (0.5 * Convert.ToDouble(parametersFromView.ySize.Text)),
                        TopCap = true,
                        ThetaDiv = thetaDiv,
                        Origin = new Point3D(x,y,j + buildingHeight),
                        Material = material };

                    mesh.Model.Transform = myRotateTransform3D;
                    modelGroup.Children.Add(mesh.Model);
                }
                //else if (chance == 2)
                //{
                //    GeometryModel3D mesh = CreateTorus(new Point3D(x, y, j), 1, 0.05, 0.5);
                //    mesh.Material = material;

                //    var model = mesh;
                //    model.Transform = myRotateTransform3D;
                //    model.BeginAnimation(AxisAngleRotation3D.AngleProperty, animation);

                //    modelGroup.Children.Add(model);
                //}
            }
        }
    }
}
