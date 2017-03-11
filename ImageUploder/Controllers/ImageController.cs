using ImageUploader.Data;
using ImageUploader.Repo;
using ImageUploader.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ImageUploder.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImage imageRepository;
         
        public ImageController(IImage repo)
        {
            imageRepository = repo;
        }
        // GET: Image
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ImageGallary()
        {
           IEnumerable<TbImage> model = imageRepository.ImageGallary;
            return View(model);
        }
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }
             imageRepository.Delete(Id);
             
            return RedirectToAction("ImageGallary", "Image");
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadFile()
        {
            string _imgname = string.Empty;

            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
                if (pic.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);

                    _imgname = Guid.NewGuid().ToString();

                    var _comPath = Server.MapPath("/Upload/Id_") + _imgname + _ext;
                    _imgname = "Id_" + _imgname + _ext;

                    ViewBag.Msg = _comPath;
                    var path = _comPath;

                    // Saving Image in Original Mode
                    pic.SaveAs(path);
                    var _lenght = new FileInfo(path).Length;

                    //here to add Image Path to You Database ,
                    TbImage data = new TbImage();
                    data.Imagepath = _imgname;
                    data.Size = Convert.ToInt32(_lenght);
                   // data.Create_time = DateTime.Now;


                    //To your Database
                    imageRepository.SaveImage(data);
                     
                    // resizing image
                    //     MemoryStream ms = new MemoryStream();
                    //    WebImage img = new WebImage(_comPath);

                    //     if (img.Width > 200)
                    //         img.Resize(200, 200);
                    //      img.Save(_comPath);
                    // end resize
                }
            }
            return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);
        }

    }
}