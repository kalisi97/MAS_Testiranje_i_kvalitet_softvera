using MAS_TestiranjeSoftvera_Projekat.BusinessLogicLayer.Interfaces;
using MAS_TestiranjeSoftvera_Projekat.DataAccessLayer.Implementations;
using MAS_TestiranjeSoftvera_Projekat.DataAccessLayer.Interfaces;
using MAS_TestiranjeSoftvera_Projekat.Domain;
using MAS_TestiranjeSoftvera_Projekat.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MAS_TestiranjeSoftvera_Projekat.Controllers
{
    public class OsobaController : Controller
    {
        private readonly IOsobaService service;
        private readonly IMestoService serviceMesto;
        public OsobaController(IOsobaService service, IMestoService serviceMesto)
        {
            this.service = service;
            this.serviceMesto = serviceMesto;
        }

        // GET: OsobaController
        public ActionResult Index()
        {
            try
            {
                var osobe = service.SelectAll();
                return View(osobe);
            }
            catch (Exception ex)
            {

                return View("Error", new ErrorViewModel { ExceptionMessage = ex.Message });
            }
        }

        // GET: MestoController/Create
        public ActionResult Create()
        {
            MestoDropDownList();
            return View();
        }

        // POST: MestoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Osoba osoba)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Insert(osoba);
                    return RedirectToAction("Index");
                }

                return View(osoba);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { ExceptionMessage = ex.Message });
            }
        }

        // GET: MestoController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                MestoDropDownList();
                var entity = service.SelectById(id);
                return View(entity);
            }
            catch(Exception ex)
            {
                return View("Error", new ErrorViewModel { ExceptionMessage = ex.Message });

            }
        }

        // POST: MestoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Osoba osoba)
        {
            try
            {
                service.Update(osoba);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { ExceptionMessage = ex.Message });
            }
        }

        // GET: MestoController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
         
                var entity = service.SelectById(id);
                MestoDropDownList(entity.MestoId);
                return View(entity);
            }
            catch (Exception ex)
            {

                return View("Error", new ErrorViewModel { ExceptionMessage = ex.Message });
                
            }
        }

        // POST: MestoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Osoba osoba)
        {
            try
            {
                service.Delete(osoba);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { ExceptionMessage = ex.Message });
            }
        }

        private void MestoDropDownList(object mesto = null)
        {
            var mestaQuery = from m in serviceMesto.SelectAll().AsQueryable()
                             orderby m.Naziv
                             select m;
            ViewBag.MestoId = new SelectList(mestaQuery.AsNoTracking(), "MestoId", "Naziv", mesto);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifikujMaticniBroj(string maticniBroj)
        {
            if (maticniBroj.Length != 13)
            {
                return Json("Maticni broj, nije u dobrom formatu!");
            }
            return Json(true);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifikujTelefon(string telefon)
        {
            if (!telefon.StartsWith("+381"))
                return Json("Unesite broj telefona u formatu +381PPBBBBBBB (PP je pozivni broj bez 0). Primeri: 3816412345678, 381117654321");
            if (telefon.Length > 13 || telefon.Length < 12)
                return Json("Predugacak/prekratak broj!");
            return Json(true);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifikujEmail(string email)
        {
            string[] strings = email.ToString().Split('@');
            bool valid = strings[1].ToUpper() == (".rs").ToUpper();
            if (!valid)
                return Json("Email se mora završavati domenom: .rs");
 
            return Json(true);
        }

    }
}
