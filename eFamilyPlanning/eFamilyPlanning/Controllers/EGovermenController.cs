using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eFamilyPlanning.Controllers
{
    public class EGovermenController : Controller
    {
        // GET: EGovermen
        public ActionResult Index()
        {
            return View();
        }

        //表单申请页面显示
        public ActionResult AppForm()
        {
            return View();
        }

        //表单申请页面处理
        [HttpPost]
        public ActionResult AppForm(FormCollection fc)
        {
            return View();
        }

        public ActionResult Query()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Query(FormCollection fc)
        {
            return View();
        }

        public ActionResult AppInfoBoard()
        {
            return View();
        }

    }
}