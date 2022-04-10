using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace SimpleGenerator
{
    public class TextureItemDataModel
    {
        public string Name { get; set; }
        public string ImageSource { get; set; }
        public bool Enable { get; set; }
        public Material Material { get; set; } // for all material information
    }
}
