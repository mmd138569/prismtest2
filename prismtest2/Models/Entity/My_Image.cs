using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prismtest2.Models.Entity
{
    public class My_Image
    {
            public string ImagePath { get; set; }
            public string ImageName => Path.GetFileName(ImagePath);
            // Add other properties as needed (e.g., ImageName, Thumbnail, etc.)
    }
}
