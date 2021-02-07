using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CursoProvimad1.Controllers;
using MisDatos2;

namespace CursoProvimad1.Tests.Controllers
{
    [TestClass]
    public class SupplierControllerTest
    {
        [TestMethod]
        public void Index()
        {
            var controller = new SuppliersController();
            MvcApplication.Mappers();
            ActionResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Details()
        {
            var controller = new SuppliersController();
            MvcApplication.Mappers();
            ActionResult result = controller.Details(1) as ViewResult;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Create()
        {
            var controller = new SuppliersController();
            MvcApplication.Mappers();
            ActionResult result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void CreatePost()
        {
            var controller = new SuppliersController();
            Supplier s = new Supplier()
            {
                CompanyName = "Pruebimad",
                ContactName = "Pruebonio",
                Country = "Pruebitania"
            };
            ActionResult result = controller.Create(s) as ViewResult; 
            //Assert.IsNotNull(result);

        }
    }
}
