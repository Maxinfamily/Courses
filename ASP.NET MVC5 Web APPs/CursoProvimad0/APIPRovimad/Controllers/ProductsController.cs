using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using MisDatos2;

namespace APIProvimad.Controllers
{
    public class ProductsController : ApiController
    {
        protected CursoEntities db = new CursoEntities();

        protected IMapper _mMapper;

        protected IMapper mMapper
        {
            get
            {
                _mMapper = _mMapper ?? WebApiApplication.MyMapperConfiguration.CreateMapper();
                return _mMapper;
            }
        }

        // GET api/<controller>
        public IEnumerable<Models.ProductViewModel> Get()
        {
            return mMapper.Map<List<Product>, List<Models.ProductViewModel> > (db.Products.ToList());
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            Product p = db.Products.Find(id);

            if (p == null)
            {
                return NotFound();
            }

            return Ok(mMapper.Map<Product, Models.ProductViewModel>(db.Products.Find(id)));

        }

        // POST api/<controller>
        public IHttpActionResult Post(Product p)
        {
            try
            {
                db.Products.Add(p);
                db.SaveChanges();
                string url = Url.Link(WebApiConfig.NameDefaultApi, new {id = p.Id});
                return Created(url, p);
            }
            catch (Exception)
            {
                return Conflict();
            }
        }


        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, Product newP)
        {
            try
            {
                Product oldP = db.Products.Find(id);
                if(newP == null)
                {
                    return NotFound();
                }

                oldP.IsDiscontinued = newP.IsDiscontinued;
                oldP.Package = newP.Package;
                oldP.UnitPrice = newP.UnitPrice;
                oldP.ProductName = newP.ProductName;
                oldP.SupplierId = newP.SupplierId;
                try
                {
                    db.Suppliers.Find(newP.SupplierId);
                }
                catch (Exception)
                {
                    return Conflict();
                }

                db.SaveChanges();
                string url = Url.Link(WebApiConfig.NameDefaultApi, new { id = newP.Id });
                return Ok();

            }
            catch (Exception)
            {
                return Conflict();
            }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
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