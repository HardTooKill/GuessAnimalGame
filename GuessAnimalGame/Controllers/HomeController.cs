using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuessAnimalGame.Controllers
{
    public class HomeController : Controller
    {

        private AnimalEntities _dbcontext;
        public HomeController()
        {
            // a better way is passing context via parameter
            _dbcontext = new AnimalEntities();
        }
        public ActionResult Index()
        {
            List<SelectListItem> animalList = new List<SelectListItem>();

            var animals = _dbcontext.Animals;
            foreach (Animal a in animals)
            {
                SelectListItem li = new SelectListItem();
                li.Text = a.Name;
                animalList.Add(li);
            }
            ViewBag.ListItem = animalList;
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            //Small trick--remember what user selected :D
            if (Session["ToGuess"] == null)
                Session.Add("ToGuess", fc[0].ToString());
            else
                Session["ToGuess"] = fc[0];
            return RedirectToAction("Guess", "Home");
        }
        [HttpGet]
        public ActionResult Guess()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Guess(FormCollection fc)
        {
            string color = fc[0].ToString().ToUpper();
            string sound = fc[1].ToString().ToUpper();
            string skin = fc[2].ToString().ToUpper();

            var animal = _dbcontext.Animals.Where(a => a.Color.ToUpper().Equals(color) && a.Sound.ToUpper().Equals(sound) && a.Skin.ToUpper().Equals(skin)).FirstOrDefault();

            string guess_animal = string.Empty;
            if (animal != null)
                guess_animal = animal.Name.Trim();
            return RedirectToAction("Result", "Home", new { result = guess_animal });
        }

        public ActionResult Result(string result)
        {
            if (string.IsNullOrEmpty(result))
                ViewBag.result = "fail";
            else
                ViewBag.result = result;
            return View();
        }
        [HttpPost]
        public ActionResult Add(FormCollection fc)
        {
            string name = fc[0].ToString().ToUpper();
            string color = fc[1].ToString().ToUpper();
            string sound = fc[2].ToString().ToUpper();
            string skin = fc[3].ToString().ToUpper();

            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("Add", "Home");
            }
            Animal animal = new Animal();
            animal.Name = name;
            animal.Color = color;
            animal.Sound = sound;
            animal.Skin = skin;

            _dbcontext.Animals.Add(animal);
            _dbcontext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
    }
}