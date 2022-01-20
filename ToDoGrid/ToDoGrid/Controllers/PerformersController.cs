using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using ToDoGrid.Models;

namespace ToDoGrid.Controllers
{
    public class PerformersController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var db = new ToDoContext();
            return View(db.Set<Performer>());            
        }

        [HttpGet]
        public ActionResult Create()
        {
            var teacher = new Performer();
            return View(teacher);
        }

        [HttpPost]
        public ActionResult Create(Performer model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var db = new ToDoContext();


            if (model.PerformerImageFile != null)
            {
                var data = new byte[model.PerformerImageFile.ContentLength];
                model.PerformerImageFile.InputStream.Read(data, 0, model.PerformerImageFile.ContentLength);

                model.PerformerImage = new PerformerImage()
                {
                    Guid = Guid.NewGuid(),
                    DateChanged = DateTime.Now,
                    Data = data,
                    ContentType = model.PerformerImageFile.ContentType,
                    FileName = model.PerformerImageFile.FileName
                };
            }
            db.Performers.Add(model);
            db.SaveChanges();

            return RedirectPermanent("/Performers/Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new ToDoContext();
            var teacher = db.Performers.FirstOrDefault(x => x.Id == id);
            if (teacher == null)
                return RedirectPermanent("/Performers/Index");

            db.Performers.Remove(teacher);
            db.SaveChanges();

            return RedirectPermanent("/Performers/Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new ToDoContext();
            var teacher = db.Performers.FirstOrDefault(x => x.Id == id);
            if (teacher == null)
                return RedirectPermanent("/Performers/Index");

            return View(teacher);
        }

        [HttpPost]
        public ActionResult Edit(Performer model)
        {
            var db = new ToDoContext();
            var teacher = db.Performers.FirstOrDefault(x => x.Id == model.Id);
            if (teacher == null)
                ModelState.AddModelError("Id", "Преподаватель не найден");

            if (!ModelState.IsValid)
                return View(model);

            MappingPerformer(model, teacher, db);

            db.Entry(teacher).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectPermanent("/Performers/Index");
        }

        private void MappingPerformer(Performer sourse, Performer destination, ToDoContext db)
        {
            destination.Name = sourse.Name;
            destination.Sex = sourse.Sex;

            if (sourse.PerformerImageFile != null)
            {
                var image = db.PerformerImages.FirstOrDefault(x => x.Id == sourse.Id);
                if (image != null)
                    db.PerformerImages.Remove(image);

                var data = new byte[sourse.PerformerImageFile.ContentLength];
                sourse.PerformerImageFile.InputStream.Read(data, 0, sourse.PerformerImageFile.ContentLength);

                destination.PerformerImage = new PerformerImage()
                {
                    Guid = Guid.NewGuid(),
                    DateChanged = DateTime.Now,
                    Data = data,
                    ContentType = sourse.PerformerImageFile.ContentType,
                    FileName = sourse.PerformerImageFile.FileName
                };
            }
        }
        [HttpGet]
        public ActionResult GetImage(int id)
        {
            var db = new ToDoContext();
            var image = db.PerformerImages.FirstOrDefault(x => x.Id == id);
            if (image == null)
            {
                FileStream fs = System.IO.File.OpenRead(Server.MapPath(@"~/Content/Images/not-foto.png"));
                byte[] fileData = new byte[fs.Length];
                fs.Read(fileData, 0, (int)fs.Length);
                fs.Close();

                return File(new MemoryStream(fileData), "image/jpeg");
            }

            return File(new MemoryStream(image.Data), image.ContentType);
        }
    }
}