using ImageUploader.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageUploader.Repo
{
    public class EFDbContext: DbContext
    {
            public DbSet<TbImage> TbImages { get; set; }
    }
}
