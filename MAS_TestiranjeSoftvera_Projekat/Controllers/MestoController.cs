using MAS_TestiranjeSoftvera_Projekat.BusinessLogicLayer.Interfaces;
using MAS_TestiranjeSoftvera_Projekat.DataAccessLayer.Interfaces;
using MAS_TestiranjeSoftvera_Projekat.Domain;
using MAS_TestiranjeSoftvera_Projekat.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MAS_TestiranjeSoftvera_Projekat.Controllers
{
    public class MestoController : Controller
    {
        // GET: MestoController

        private readonly IMestoService service;

        public MestoController(IMestoService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            try
            {
                var entities = service.SelectAll();
                return View(entities);
            }
            catch (Exception ex)
            {

                return View("Error", new ErrorViewModel { ExceptionMessage = ex.Message });
            }
        }

  
        // GET: MestoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MestoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Mesto mesto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Insert(mesto);
                    return RedirectToAction("Index");
                }

                return View(mesto);
            }
            catch(Exception ex)
            {
                return View("Error", new ErrorViewModel { ExceptionMessage = ex.Message });
            }
        }

        // GET: MestoController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var entity = service.SelectById(id);
                return View(entity);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { ExceptionMessage = ex.Message });
            }
        }

        // POST: MestoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Mesto mesto)
        {
            try
            {
                var mestoIzBaze = service.SelectById(mesto.MestoId);
                if (mestoIzBaze == null)
                    throw new Exception("Trazeno mesto ne postoji!");
                else
                    service.Update(mesto);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
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
        public ActionResult Delete([FromForm]Mesto mesto)
        {
            try
            {
                service.Delete(mesto);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View("Error", new ErrorViewModel { ExceptionMessage = ex.Message });
            }
        }


        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifikujBrojStanovnika(int? brojStanovnika)
        {
            if (brojStanovnika == null) return Json(true);
            if (int.TryParse(Convert.ToString(brojStanovnika), out int x))
            {
                if(x <= 0)
                return Json("Broj stanovnika ne moze biti 0 ili manji od 0!");
            }
            else
            {
                return Json("Unesite broj!");
            }
            return Json(true);
        }
    }
}
