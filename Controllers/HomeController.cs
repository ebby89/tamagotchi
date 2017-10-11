using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TamagotchiGame.Models;
using System;

namespace TamagotchiGame.Controllers
{
    public class HomeController : Controller
    {
        private Dictionary<string, Object> _model = new Dictionary<string, Object>(){};

        public HomeController()
        {
            _model.Add("TamagotchiList", Tamagotchi.GetAll());
            _model.Add("SelectedTamagotchi", Tamagotchi.CurrentlySelected);
        }

        [HttpGet("/"), ActionName("Index")]
        public ActionResult Index()
        {
            return View(_model);
        }

        [HttpPost("/tamagotchi/new"), ActionName("Index")]
        public ActionResult NewTamagotchi()
        {
            string tamagotchiName = Request.Form["tamagotchi-name"];
            if (tamagotchiName != "")
            {
                Tamagotchi NewTamagotchi = new Tamagotchi(tamagotchiName);
            }

            return View(_model);
        }

        [HttpGet("/tamagotchi/{id}"), ActionName("Index")]
        public ActionResult TamagotchiStatus(int id)
        {
            _model["SelectedTamagotchi"] = Tamagotchi.Select(id);
            return View(_model);
        }

        [HttpGet("/tamagotchi/fast-forward"), ActionName("Index")]
        public ActionResult FastForward()
        {
            foreach (Tamagotchi tamagotchi in Tamagotchi.GetAll())
            {
                tamagotchi.PassTime();
            }
            return View(_model);
        }

        [HttpGet("/tamagotchi/clear"), ActionName("Index")]
        public ActionResult Clear()
        {
            Tamagotchi.ClearAll();
            _model["SelectedTamagotchi"] = null;
            return View(_model);
        }

        [HttpGet("/tamagotchi/sleep"), ActionName("Index")]
        public ActionResult Sleep()
        {
            Tamagotchi.CurrentlySelected.Sleep();
            return View(_model);
        }

        [HttpGet("/tamagotchi/feed"), ActionName("Index")]
        public ActionResult Feed()
        {
            Tamagotchi.CurrentlySelected.Feed();
            return View(_model);
        }

        [HttpGet("/tamagotchi/pet"), ActionName("Index")]
        public ActionResult Pet()
        {
            Tamagotchi.CurrentlySelected.Pet();
            return View(_model);
        }
    }
}
