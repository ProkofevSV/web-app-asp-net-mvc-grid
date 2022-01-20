using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using ToDoGrid.Models;

namespace ToDoGrid.Controllers
{
    public class TasksController : Controller
    {
        
        [HttpGet]
        public ActionResult Index()
        {
            var db = new ToDoContext();
            return View(db.Set<Task>());


            //var tasks = db.Tasks.ToList();
            //return View(tasks);

        }

        [HttpGet]
        public ActionResult Create()
        {
            var task = new Task();
            return View(task);
        }

        [HttpPost]
        public ActionResult Create(Task model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var db = new ToDoContext();

           

            if (model.TopicIds != null && model.TopicIds.Any())
            {
                var topic = db.Topics.Where(s => model.TopicIds.Contains(s.Id)).ToList();
                model.Topics = topic;
            }
            if (model.DayOfWeekIds != null && model.DayOfWeekIds.Any())
            {
                var dayOfWeeks = db.DayOfWeeks.Where(s => model.DayOfWeekIds.Contains(s.Id)).ToList();
                model.DayOfWeeks = dayOfWeeks;
            }
            if (!ModelState.IsValid)
                return View(model);



            db.Tasks.Add(model);
            db.SaveChanges();

            return RedirectPermanent("/Tasks/Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new ToDoContext();
            var task = db.Tasks.FirstOrDefault(x => x.Id == id);
            if (task == null)
                return RedirectPermanent("/Tasks/Index");

            db.Tasks.Remove(task);
            db.SaveChanges();

            return RedirectPermanent("/Tasks/Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new ToDoContext();
            var task = db.Tasks.FirstOrDefault(x => x.Id == id);
            if (task == null)
                return RedirectPermanent("/Tasks/Index");

            return View(task);
        }

        [HttpPost]
        public ActionResult Edit(Task model)
        {
            var db = new ToDoContext();
            var task = db.Tasks.FirstOrDefault(x => x.Id == model.Id);
            if (task == null)
                ModelState.AddModelError("Id", "Задача не найдена");
           
            if (!ModelState.IsValid)
                return View(model);

            MappingTask(model, task, db);

            db.Entry(task).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectPermanent("/Tasks/Index");
        }

        private void MappingTask(Task sourse, Task destination, ToDoContext db)
        {
            destination.Name = sourse.Name;
            destination.PerformerId = sourse.PerformerId;
            destination.Performer = sourse.Performer;
            

            if (destination.Topics != null)
                destination.Topics.Clear();

            if (sourse.TopicIds != null && sourse.TopicIds.Any())
                destination.Topics = db.Topics.Where(s => sourse.TopicIds.Contains(s.Id)).ToList();

            if (destination.DayOfWeeks != null)
                destination.DayOfWeeks.Clear();

            if (sourse.DayOfWeekIds != null && sourse.DayOfWeekIds.Any())
                destination.DayOfWeeks = db.DayOfWeeks.Where(s => sourse.DayOfWeekIds.Contains(s.Id)).ToList();
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

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var db = new ToDoContext();
            var task = db.Tasks.FirstOrDefault(x => x.Id == id);
            if (task == null)
                return RedirectPermanent("/Tasks/Index");

            return View(task);
        }
    }
}