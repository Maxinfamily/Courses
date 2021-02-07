using AutoMapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Threading;
using System.Globalization;

using System.Web.Mvc;
using System.Web.Routing;
using MisDatos2;

namespace CursoProvimad1.Controllers
{

    public abstract class BaseController : Controller
    {
        protected string SesLanguageName = "Language";
        protected CursoEntities db = new CursoEntities();
        protected IMapper _mMapper;

        protected IMapper mMapper
        {
            get
            {
                _mMapper = _mMapper ?? MvcApplication.MyMapperConfiguration.CreateMapper();
                return _mMapper;
            }
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session[SesLanguageName] != null)
            {
                _ChangeLanguage(Session[SesLanguageName].ToString());
            }
        }

        protected void _ChangeLanguage(string id)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(id);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(id);
        }
        public ActionResult ChangeLanguage(string id)
        {
            Session[SesLanguageName] = id;
            _ChangeLanguage(id);
            return View("Index");
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