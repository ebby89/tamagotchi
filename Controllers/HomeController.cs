using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TamagotchiGame.Models;
using System;

namespace TamagotchiGame.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/"), ActionName("Index")]
        public ActionResult Index()
        {
            Dictionary<string, Object> _model = new Dictionary<string, Object>(){};
            _model.Add("TamagotchiList", Tamagotchi.GetAll());
            _model.Add("SelectedTamagotchi", Tamagotchi.CurrentlySelected);
            return View(_model);
        }

        [HttpGet("/tamagotchi/{id}")]
        public ActionResult TamagotchiStatus(int id)
        {
            Tamagotchi.Select(id);
            return Redirect("/");
        }

        [HttpPost("/tamagotchi/new")]
        public ActionResult NewTamagotchi()
        {
            string tamagotchiName = Request.Form["tamagotchi-name"];
            if (tamagotchiName != "")
            {
                Tamagotchi NewTamagotchi = new Tamagotchi(tamagotchiName);
            }

            return Redirect("/");
        }

        [HttpGet("/tamagotchi/fast-forward")]
        public ActionResult FastForward()
        {
            foreach (Tamagotchi tamagotchi in Tamagotchi.GetAll())
            {
                tamagotchi.PassTime();
            }
            return Redirect("/");
        }

        [HttpGet("/tamagotchi/clear")]
        public ActionResult Clear()
        {
            Tamagotchi.ClearAll();
            return Redirect("/");
        }

        [HttpGet("/tamagotchi/sleep")]
        public ActionResult Sleep()
        {
            Tamagotchi.CurrentlySelected.Sleep();
            return Redirect("/");
        }

        [HttpGet("/tamagotchi/feed")]
        public ActionResult Feed()
        {
            Tamagotchi.CurrentlySelected.Feed();
            return Redirect("/");
        }

        [HttpGet("/tamagotchi/pet")]
        public ActionResult Pet()
        {
            Tamagotchi.CurrentlySelected.Pet();
            return Redirect("/");
        }
    }
}
