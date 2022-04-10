using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Media.Media3D;
using System.Windows;
using System.IO;
using HelixToolkit.Wpf;
using System.Windows.Media;

namespace SimpleGenerator
{
    public class TextureControlViewModel : PropertyChangedBase
    {
        string path;
        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                NotifyOfPropertyChange(() => Path);
            }
        }

        public ObservableCollection<TextureItemDataModel> TextureCollection
        {
            get { return textureCollection; }
            set
            {
                textureCollection = value;
                NotifyOfPropertyChange(() => TextureCollection);
            }
        }
        ObservableCollection<TextureItemDataModel> textureCollection = new ObservableCollection<TextureItemDataModel>();

        public TextureControlViewModel()
        {
            try
            {

                var pathToLoad = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\textures\";
                if(Directory.Exists(path));
                    path = pathToLoad;

                LoadTextures();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "error");
            }
        }

        private List<Material> LoadTexturesFromFolder(string dirPath)
        {
            List<Material> materials = new List<Material>();

            foreach (var file in Directory.GetFiles(dirPath, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".jpeg") || s.EndsWith(".jpg") || s.EndsWith(".png")))
                materials.Add(MaterialHelper.CreateImageMaterial(file, 1));

            return materials;
        }

        public void LoadTextures()
        {
            foreach (var material in LoadTexturesFromFolder(path))
            {
                string filePath = ((material as DiffuseMaterial).Brush as ImageBrush).ImageSource.ToString();

                if (material is DiffuseMaterial)
                    TextureCollection.Add(new TextureItemDataModel()
                    {
                        ImageSource = filePath,
                        Name = System.IO.Path.GetFileName(filePath),
                        Enable = true,
                        Material = material
                    });
            }
        }

        public void Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            try
            {
                path = dialog.SelectedPath;
                LoadTextures();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "error");
            }
        }
    }
}
