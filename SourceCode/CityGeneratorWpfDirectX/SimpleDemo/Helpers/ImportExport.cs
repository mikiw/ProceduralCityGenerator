using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace SimpleGenerator
{
    public static class Manager
    {
        public static void ExportModel3d(string path, Model3D model, List<string> textures)
        {
            ObjExporter objExporter = new HelixToolkit.Wpf.ObjExporter();// {ExportNormals = true, SwitchYZ = true };
            objExporter.MaterialsFile = path + @"\export.mtl";
            using (Stream filestream = File.Create(path + @"\export.obj"))
                objExporter.Export(model, filestream);

            XamlExporter xamlExporter = new HelixToolkit.Wpf.XamlExporter();
            using (Stream filestream = File.Create(path + @"\export.xml"))
                xamlExporter.Export(model, filestream);

            //StlExporter stlExporter = new HelixToolkit.Wpf.StlExporter();
            //using (Stream filestream = File.Create(path + @"\export.stl"))
            //    stlExporter.Export(model, filestream);

            //X3DExporter x3DExporter = new HelixToolkit.Wpf.X3DExporter();
            //using (Stream filestream = File.Create(path + @"\export.x3d"))
            //    x3DExporter.Export(model, filestream);

            //var firstLineOfObj = File.ReadLines(path + @"\export.obj").First();

            foreach (var mat in textures)
                System.IO.File.Copy(mat, path + @"\" + Path.GetFileName(mat));

            //firstLineOfObj.Replace("./", "");

            //TODO: mtllib exportedModel.mtl works for example
            //TODO: mtllib ./C:\Users\mikiw_000\Desktop\New folder\export.mtl change replace mtllib ./ with mtllib and 
        }

        public static Model3D ImportScene(string path)
        {
            var importer = new HelixToolkit.Wpf.ObjReader();
            var groupLoaded = importer.Read(path);
            return groupLoaded;
        }
    }
}
