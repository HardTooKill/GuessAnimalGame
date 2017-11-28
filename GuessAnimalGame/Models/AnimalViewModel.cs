using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuessAnimalGame.Models
{
    public class AnimalViewModel
    {
        public List<Animal> Animals { get; set; }
        public List<SelectListItem> list { get; set; }
        public string SelectedAnimal { get; set; }
    }
}