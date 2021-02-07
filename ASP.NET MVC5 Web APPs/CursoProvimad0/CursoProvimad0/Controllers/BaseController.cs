using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using MisDatos2;

namespace CursoProvimad0.Controllers
{

    public abstract class BaseController : Controller
    {
        protected CursoEntities db = new CursoEntities();
        protected IMapper _mMapper;

        protected IMapper mMapper
        {
            get { 
                _mMapper = _mMapper ?? MvcApplication.MyMapperConfiguration.CreateMapper();
                return _mMapper;
            }
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