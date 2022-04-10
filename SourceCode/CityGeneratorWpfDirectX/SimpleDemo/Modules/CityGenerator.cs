using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace SimpleDemo
{
    public class CityGenerator
    {
        private ObservableCollection<TextureItemDataModel> materials;
        private MainWindow parametersFromView;
        public CityGenerator(ObservableCollection<TextureItemDataModel> materials, MainWindow parametersFromView)
        {
            this.materials = materials;
            this.parametersFromView = parametersFromView;
        }

        public Model3DGroup GenerateCity()
        {
            //var elements = GetElements();
            //List<IMeshElement3D> modelsAvailable = GetElementsWithProbability(elements);
            
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

                //Spaces between
                double space = Convert.ToDouble(parametersFromView.spacesBetween.Text);

                bool isInCircle = false;

                if (parametersFromView.combobox.SelectedItem == parametersFromView.combobox.Items[0])
                {
                    //Grid City
                    positionX = (ProbabilityModule.randomSeed.Next(Convert.ToInt32(parametersFromView.minMapSize.Text), Convert.ToInt32(parametersFromView.maxMapSize.Text)) / Convert.ToInt32(space) * space);
                    positionY = (ProbabilityModule.randomSeed.Next(Convert.ToInt32(parametersFromView.minMapSize.Text), Convert.ToInt32(parametersFromView.maxMapSize.Text)) / Convert.ToInt32(space) * space);
                }
                else if (parametersFromView.combobox.SelectedItem == parametersFromView.combobox.Items[1])
                {
                    //Grid Random City
                    positionX = ProbabilityModule.NextDouble(Convert.ToDouble(parametersFromView.minMapSize.Text), Convert.ToDouble(parametersFromView.maxMapSize.Text) / space) * space;
                    positionY = ProbabilityModule.NextDouble(Convert.ToDouble(parametersFromView.minMapSize.Text), Convert.ToDouble(parametersFromView.maxMapSize.Text) / space) * space;
                }
                else if (parametersFromView.combobox.SelectedItem == parametersFromView.combobox.Items[2])
                {
                    //Circle City
                    positionX = (ProbabilityModule.randomSeed.Next(Convert.ToInt32(parametersFromView.minMapSize.Text), Convert.ToInt32(parametersFromView.maxMapSize.Text)) / Convert.ToInt32(space) * space);
                    positionY = (ProbabilityModule.randomSeed.Next(Convert.ToInt32(parametersFromView.minMapSize.Text), Convert.ToInt32(parametersFromView.maxMapSize.Text)) / Convert.ToInt32(space) * space);

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
                    positionX = ProbabilityModule.NextDouble(Convert.ToDouble(parametersFromView.minMapSize.Text), Convert.ToDouble(parametersFromView.maxMapSize.Text) / space) * space;
                    positionY = ProbabilityModule.NextDouble(Convert.ToDouble(parametersFromView.minMapSize.Text), Convert.ToDouble(parametersFromView.maxMapSize.Text) / space) * space;

                    double distance = Point.Subtract(new Point(0, 0), new Point(positionX, positionY)).Length;
                    double maxR = Math.Abs(Convert.ToDouble(parametersFromView.minMapSize.Text) - Convert.ToDouble(parametersFromView.maxMapSize.Text)) / 2;

                    if (distance < maxR)
                    {
                        isInCircle = true;
                    }
                }

                //Correction Shift
                if (space >= 1)
                {
                    positionX += positionX / space;
                    positionY += positionY / space;
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

                var chance = ProbabilityModule.randomSeed.Next(0, 25);

                if (parametersFromView.combobox.SelectedItem == parametersFromView.combobox.Items[2] ||
                    parametersFromView.combobox.SelectedItem == parametersFromView.combobox.Items[3])
                {
                    if (isInCircle == true)
                    { 
                        if (chance == 0)
                            GenerateDynamicTower(height * 2, modelGroup, positionX, positionY, ProbabilityModule.NextDouble(5, 35),2,2);
                        else
                            GenerateBuildingOfHeight(height, modelGroup, positionX, positionY, false); // w.floatInTheAir.IsChecked.Value do it later
                    }
                    else
                        i--; // -1 from i coz there will be no building builded

                }
                else
                {
                    if (chance == 0)
                        GenerateDynamicTower(height * 2, modelGroup, positionX, positionY, ProbabilityModule.NextDouble(5, 35),2,1);
                    else
                        GenerateBuildingOfHeight(height, modelGroup, positionX, positionY, false); // w.floatInTheAir.IsChecked.Value do it later
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

            List<BaseModuleViewModel> modules = new List<BaseModuleViewModel>();
            modules.Add((parametersFromView.buildingControl.boxModule.DataContext as BoxModuleViewModel));

            foreach (var module in modules)
                for (int i = 0; i < module.Frequency; i++)
                    modulesWithFrequency.Add(module); // it's just ref

            return modulesWithFrequency; // change to yeld later
        }

        private void GenerateBuildingOfHeight(double height, Model3DGroup modelGroup, double positionX, double positionY, bool floatInTheAir)
        {
            // Signle texture per building        
            Material material = GetRandomEnabledMaterial();

            var modulesWithFrequency = GetModulesWithFrequency();

            for (double j = 0; j <= height; j++)
            {
                var module = modulesWithFrequency[ProbabilityModule.randomSeed.Next(0, modulesWithFrequency.Count)];
                
                if(module is BoxModuleViewModel)
                    modelGroup.Children.Add((parametersFromView.buildingControl.boxModule.DataContext as BoxModuleViewModel).CreateModel(positionX, positionY, j, material));
                else if(module is SphereModuleViewModel)
                    modelGroup.Children.Add((parametersFromView.buildingControl.sphereModule.DataContext as SphereModuleViewModel).CreateModel(positionX, positionY, j, material));


                //if (mesh is SphereVisual3D)
                //{
                //    // var type = container.Resolve<SphereVisual3D>();
                //    // ISphereModule myServiceInstance = container.Resolve<ISphereModule>();

                //    modelGroup.Children.Add((new SphereModule((mesh.Mesh as SphereVisual3D))).Create(positionX, positionY, j, material));
                //}
                //else if (mesh is PipeVisual3D)
                //{
                //    modelGroup.Children.Add((new PipeModule((mesh.Mesh as PipeVisual3D)).Create(positionX, positionY, j, material)));
                //}
                //else if (mesh is TruncatedConeVisual3D)
                //{
                //    modelGroup.Children.Add((new ConeModule((mesh.Mesh as TruncatedConeVisual3D)).Create(positionX, positionY, j, material)));
                //}

                //CreateAntennas
                if (j  == height)
                {
                    for (int i = 0; i < ProbabilityModule.randomSeed.Next(1, 5); i++)
                    {
                        var x = ProbabilityModule.NextDouble(-0.5, 0.5);
                        var y = ProbabilityModule.NextDouble(-0.5, 0.5);
                        double antennaHeight = ProbabilityModule.NextDouble(1, 2);

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
                        //modelGroup.Children.Add(CreateTorus(new Point3D(positionX + x, positionY + y, antennaHeight), 0.5, 0.05));
                    }
                }

                // IOC resolver to this
                //if (obj is Foo)
                //{
                //    modelGroup.Children.Add((new FooImplementation((obj as A))).Create());
                //}
            }
        }

        //move to helper class
        // need to be optimized?
        private GeometryModel3D CreateTorus(Point3D startPosition, double r, double meshDelta, double diameter)
        {
            var path = new Point3DCollection();

            //start point
            path.Add(new Point3D(startPosition.X + (meshDelta / 2), startPosition.Y - r, startPosition.Z));

            for (double y = -r; y <= r; y += meshDelta)
            {
                var x1circle = -Math.Pow((-Math.Pow(y, 2) + Math.Pow(r, 2)), 0.5);
                path.Add(new Point3D(x1circle + startPosition.X, y + startPosition.Y, startPosition.Z));
            }

            for (double y = +r; y >= -r; y -= meshDelta)
            {
                var x2circle = +Math.Pow((-Math.Pow(y, 2) + Math.Pow(r, 2)), 0.5);
                path.Add(new Point3D(x2circle + startPosition.X, y + startPosition.Y, startPosition.Z));
            }

            //finish point
            path.Add(new Point3D(startPosition.X - (meshDelta / 2), startPosition.Y - r, startPosition.Z));

            TubeVisual3D tube = new TubeVisual3D()
            {
                Diameter = diameter,
                ThetaDiv = 60,
                Path = path
            };

            return tube.Model;
        }

        //private List<IMeshElement3D> GetElements()
        //{
        //    double minHeight = 1;
        //    double maxHeight = 1;

        //    List<IMeshElement3D> list = new List<IMeshElement3D>();

        //    Random rnd = new Random();

        //    var locationHeight = maxHeight;
        //    var elementHeight = minHeight;

        //    //if (floatInTheAir)
        //    //{
        //    //    locationHeight = NextDouble(double.Parse(minFloatHeight.Text, CultureInfo.InvariantCulture), double.Parse(maxFloatHeight.Text, CultureInfo.InvariantCulture));
        //    //    elementHeight = NextDouble(double.Parse(minFloatHeight.Text, CultureInfo.InvariantCulture), double.Parse(maxFloatHeight.Text, CultureInfo.InvariantCulture));
        //    //}

        

        //    //higher probability
        //    list.Add(new BoxModule(30, new BoxVisual3D() { Width = 1, Length = 1, Height = elementHeight }));
        //    list.Add(new BoxModule(3, new BoxVisual3D() { Width = 0.7, Length = 0.7, Height = elementHeight }));

        //    list.Add(new SphereModule(3, new SphereVisual3D() { PhiDiv = 2, ThetaDiv = 4 })); // sphere as rotated cube
        //    list.Add(new SphereModule(3, new SphereVisual3D() { PhiDiv = 4, ThetaDiv = 4 })); // sphere as rotated cube
        //    list.Add(new SphereModule(3, new SphereVisual3D() { PhiDiv = 15, ThetaDiv = 15 }));

        //    list.Add(new PipeModule(3, new PipeVisual3D() { Diameter = 1, InnerDiameter = 0.5, ThetaDiv = 5 }));
        //    list.Add(new PipeModule(3, new PipeVisual3D() { Diameter = 1, InnerDiameter = 0.5, ThetaDiv = 15 })); //so yea you can use pipe as cylider

        //    list.Add(new ConeModule(3, new TruncatedConeVisual3D() { Height = 1, BaseCap = true, BaseRadius = 2, TopRadius = 1, TopCap = true, ThetaDiv = 4 }));
        //    //list.Add(new ConeModule(1, new TruncatedConeVisual3D() { BaseCap = false, BaseRadius = 4, TopRadius = 1, TopCap = false, ThetaDiv = 10 })); //lamps? lol
        //    //list.Add(new ConeModule(3, new TruncatedConeVisual3D() { Height = 1, BaseCap = true, BaseRadius = 1, TopRadius = 0.5, TopCap = true, ThetaDiv = 10 })); 
        //    list.Add(new ConeModule(1, new TruncatedConeVisual3D() { Height = 1, BaseCap = true, BaseRadius = 3, TopRadius = 0.5, TopCap = true, ThetaDiv = 20 })); // mushroom

        //    //list.Add(new ConeModule(1, new TruncatedConeVisual3D() { BaseCap = true, BaseRadius = 1, TopRadius = 0.5, TopCap = true, ThetaDiv = 4 }));


        //    //var meshBuilder = new MeshBuilder(false, false);
        //    //meshBuilder.AddSphere(new Point3D(), 1, 4, 4);

        //    // Maybe I'll change my mind of mershBuilder
        //    // var meshBuilder = new MeshBuilder(false, false);
        //    // meshBuilder.AddBox(new Point3D(-0.25 + (i * 0.5), -0.25 + (j * 0.5), minModuleHeight), 0.5, 0.5, elementHeight);
        //    // meshBuilder.AddBox(new Point3D(-0.2 + (i * 0.4), -0.2 + (j * 0.4), minModuleHeight), 0.6, 0.6, elementHeight);
        //    // meshBuilder.AddBox(new Point3D(-0.15 + (i * 0.3), -0.15 + (j * 0.3), 0), 0.7, 0.7, elementHeight);

        //    return list;
        //}

        private void GenerateDynamicTower(double height, Model3DGroup modelGroup, double x, double y, double angle, double maxWidth, double minWidth)
        {
            var material = GetRandomEnabledMaterial();

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
            

            for (double j = 0; j < height; j += 0.5)
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

                if (chance == 0)
                {
                    //new mechanism
                    //var mesh = (parametersFromView.buildingControl.boxModule.DataContext as BoxModuleViewModel).CreateModel(x, y, j, material);

                    //old mesh mechanizm
                    var mesh = new BoxVisual3D() { Width = maxWidth, Length = 1, Height = 0.5, Center = new Point3D(x,y,j), Material = material };

                    mesh.Model.Transform = myRotateTransform3D;
                    modelGroup.Children.Add(mesh.Model);
                }
                else if (chance == 1)
                {
                    var mesh = new TruncatedConeVisual3D() { Height = 0.5, BaseCap = true, BaseRadius = maxWidth - delta + minWidth, TopRadius = maxWidth - delta + minWidth - 0.5, TopCap = true, ThetaDiv = 4 };

                    var model = (new ConeModule(mesh)).Create(x, y, j, material);
                    model.Transform = myRotateTransform3D;

                    modelGroup.Children.Add(model);
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
