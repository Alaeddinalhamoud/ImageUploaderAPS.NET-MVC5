using ImageUploader.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageUploader.Service
{
    public interface IImage
    {
        void SaveImage(TbImage image);
        void Delete(int? Id);
        IList<TbImage> ImageGallary { get; }
    }
}
