using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Schema;
using CursoProvimad1.Models;
using MisDatos2;

namespace CursoProvimad1.Controllers
{
    public class CustomersController : BaseController
    {
        // GET: Customers
        public ActionResult Index()
        {
            var list = db.Customers.ToList();
            List<CustomerViewModel> vm = mMapper.Map <List<Customer>, List<CustomerViewModel>>(list);
            return View("List", vm);
        }
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            CustomerViewModel vm = mMapper.Map<Customer, CustomerViewModel>(customer);
            return View(vm);
        }

        public ActionResult Create()
        {

            return View();
        }

        public ActionResult CreatePartial()
        {

            return PartialView("_Create");
        }

        // POST: Suppliers/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,City,Country,Phone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        [HttpPost]
        public JsonResult CreateAjax([Bind(Include = "Id,FirstName,LastName,City,Country,Phone")] Customer customer)
        {
            int code = 500;
            try
            {
                if (ModelState.IsValid)
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    // return RedirectToAction("Index");
                    code = 200;
                }
            }
            catch (Exception ex)
            {

            }

            CustomerViewModel vm = mMapper.Map<Customer, CustomerViewModel>(customer);

            Response.StatusCode = code;

            return new JsonResult()
            {
                Data = vm
            };
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            CustomerViewModel vm = mMapper.Map<Customer, CustomerViewModel>(customer);
            return View(vm);
        }

        // POST: Suppliers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,City,Country,Phone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }
    }
}