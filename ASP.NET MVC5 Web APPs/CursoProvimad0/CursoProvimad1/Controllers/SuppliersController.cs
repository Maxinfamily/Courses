using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using MisDatos2;
using AutoMapper;
using CursoProvimad1.Models;

namespace CursoProvimad1.Controllers
{
    public class SuppliersController : BaseController
    {


        // GET: Suppliers
        public ActionResult Index()
        {
            var list = db.Suppliers.ToList();
            List<SupplierViewModel> vm = mMapper.Map<List<Supplier>, List<SupplierViewModel>>(list);
            return View(vm);
        }

        public ActionResult ProductList(int id)
        {
            List<Product> list = db.Products.Where(d => d.SupplierId == id).ToList();
            return PartialView(list);
        }

        // GET: Suppliers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }

            SupplierViewModel vm = mMapper.Map<Supplier, SupplierViewModel>(supplier);
            return View(vm);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CompanyName,ContactName,ContactTitle,City,Country,Phone,Fax")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(supplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }

            SupplierViewModel vm = mMapper.Map<Supplier, SupplierViewModel>(supplier);
            return View(vm);
        }

        // POST: Suppliers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CompanyName,ContactName,ContactTitle,City,Country,Phone,Fax")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            db.Suppliers.Remove(supplier);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet, ActionName("DeleteConfirmed")]
        public ActionResult DeleteConfirmed2(int id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            db.Suppliers.Remove(supplier);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet, ActionName("DeleteAjax")]
        public JsonResult DeleteConfirmed3(int id)
        {
            int code = 0;
            string errorText = null;
            try
            {
                Supplier supplier = db.Suppliers.Find(id);

                if (supplier == null)
                {
                    code = 404;
                    errorText = "Proveedor no encontrado";
                }
                else
                {

                    db.Suppliers.Remove(supplier);
                    db.SaveChanges();
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                code = 409;
                errorText = "No se permite borrar un proveedor que tiene productos asociados";
            }
            catch (Exception e)
            {
                code = 418;
                
            }

            JsonResult jr = new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = id
            };
            if (code != 0)
            {
                Response.StatusCode = code;
                if (errorText != null)
                {
                    Response.StatusDescription = errorText;
                }
            }
            return jr;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
