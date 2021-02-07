using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;

namespace CursoProvimad1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static MapperConfiguration MyMapperConfiguration;
        public static void Mappers()
        {

            MyMapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MisDatos2.Supplier, CursoProvimad1.Models.SupplierViewModel>()
                    .ForMember(p => p.ProductsCount, opt => opt.MapFrom(PreSendRequestContent => PreSendRequestContent.Products.Count))
                    .ForMember(p2 => p2.AvailableProductsCount, opt2 => opt2.MapFrom(PreSendRequestContent => PreSendRequestContent.Products.Count(continued => !continued.IsDiscontinued)));
                cfg.CreateMap<MisDatos2.Customer, CursoProvimad1.Models.CustomerViewModel>();
            });

            MyMapperConfiguration.AssertConfigurationIsValid();
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Mappers();
        }
    }
}
