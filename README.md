# ProceduralCityGenerator
CityGenerator v 0.1 is a WPF/DirectX tool to generate mesh of cities or other structures.

This tool can be easily extended to use models instead of 3d figures.

# Day 0 - Inspiration
I like medieval or fantasy games like Dark Souls and The Witcher but I’m loved in Sci-fi worlds and cities. My inspirations to this demo are sci-fi cites from cyberpunk universes like Ghost in the shell, Infinity The Game, Android Netrunner, Eve Online and Blade Runner.

![Android Netrunner City](./Doc/1.png)

![Greeble tool from 3ds max](./Doc/2.png)

![Dynamic tower (architecture concept)](./Doc/3.png)

![Mirrors Edge Minimalistic City](./Doc/4.png)

Unity 3D & WPF close integration
Close integration like using Unity 3D Scene in WPF or WPF ViewPort in unity 3D is almost impossible. Of course all in IT is possible but decompiling Unity 3D or WPF is time consuming. Unity scene or packages are binary format so it also require some kind of reverese engienering.

There is a way to create reusable code with external DLLs. I used it one time with OpenCV and EMGU in Unity 3d for face detection with Kinect but I have a lot of problems with it. It's time consuming method and in my opinion is not worth it. Unity 3D doesn't use .NET but Mono so you need to recompile whole library. Also the Mono version that Unity 3D is using does not support even the all of .NET 2.0 features (!).

Of course you can implement some logic in .NET 2.0 and use it with 4.5 WPF tool with multi-targeting options but you need to compile same code in Mono for Unity. But there is another problem: recreating visual preview of scene in Unity 3D and in WPF ViewPort as far as I know there isn't any framework to do this.

Possible approaches with WPF and Unity 3d integration

If you are creating tool to generate meshes with materials in WPF and export scene to *.obj or import from *.obj or others 3D common formats it’s not a problem but things can go tricky if you want to use something more complicated or uncommon for example objects like sharders, lights, animations, colliders or other features that WPF don’t provide. One way to integrate tool with Unity 3d and Unity 3d with your tool is your own protocol or format for example xml and reconstruction in both sides based on information in file. This solution have one disadvantage you need to write code for each 3d tool for example Unreal Engine, 3dsmax, blender, TopoGun etc as plugins.

I decided to export data in *.obj and *.mtl format for meshes and materials, it’s simple and common format for 3d models. It’s also possible to create / reconstruct other object like shaders, colliders etc in MenuEdit Helper plugin for Unity 3D.
It’s also possible to migrate lights and animations (Animatable class) from WPF to Unity 3D but you need to have come contract for each object to recreate it in Unity 3D from XAML/XML. It might be also possible to migrate even shaders with SharpDX integration. This things are not implemented yet due to time consuming. I had enough problems with *.obj export mainly with localization and textures.

# Day 1 - Proof of concept as Unity 3D plugin
