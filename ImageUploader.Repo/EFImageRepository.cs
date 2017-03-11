using ImageUploader.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageUploader.Data;

namespace ImageUploader.Repo
{
    public class EFImageRepository : IImage
    {
        EFDbContext context = new EFDbContext();

        public IList<TbImage> ImageGallary
        {
            get
            {
                return context.TbImages.ToList<TbImage>();
            }
        }

        public void Delete(int? Id)
        {
            TbImage dbEntry = context.TbImages.Find(Id);
            if(dbEntry!=null)
            {
                context.TbImages.Remove(dbEntry);
                context.SaveChanges();
            }            

        }

        public void SaveImage(TbImage image)
        {

            context.TbImages.Add(image);
            context.SaveChanges();

        }
    }
}
