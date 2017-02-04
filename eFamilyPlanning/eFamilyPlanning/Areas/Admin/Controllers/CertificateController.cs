using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eFamilyPlanning.Areas.Admin.Controllers
{
    public class CertificateController : Controller
    {
        // GET: Admin/Certificate
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FormPrintIndex()
        {
            return View();
        }

        public ActionResult PhotoPrintIndex()
        {
            return View();
        }

        public ActionResult PhotoPrintDetial()
        {
            return View();
        }
    }
}