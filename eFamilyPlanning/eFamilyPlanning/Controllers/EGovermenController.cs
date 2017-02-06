using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eFamilyPlanning.ComFun;
using System.IO;
using System.Text;

namespace eFamilyPlanning.Controllers
{
    public class EGovermenController : Controller
    {
        // GET: EGovermen
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadIMG()
        {
            return View();
        }

        [HttpPost]
        public void Index(FormCollection fc)
        {
            string filePath = Server.MapPath("/Template/Word/w1.docx");
            //using (MemoryStream ms = NPOIHelp.ExportDoc(filepath))
            //{
            //    Response.ContentType = "application/vnd.ms-word";
            //    Response.ContentEncoding = Encoding.UTF8;
            //    Response.Charset = "";
            //    Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("123.docx", Encoding.UTF8));
            //    //Response.BinaryWrite(Export().GetBuffer());
            //    Response.BinaryWrite(ms.GetBuffer());
            //    Response.End();
            //}
            //return View();
            using (MemoryStream ms = NPOIHelp.ExportWord(filePath))
            {
                string fileName = "123" + DateTime.Now.ToString();
                if (Request.Browser.Browser == "IE")
                    fileName = HttpUtility.UrlEncode(fileName);
                //byte[] byteArray = new Byte[ms.Length];
                //ms.Read(byteArray, 0, (int)ms.Length);
                //Response.Buffer = false;
                Response.Clear();
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document; name=" + fileName;
                Response.ContentType = "application/vnd.ms-word; name=" + fileName;
                Response.AddHeader("content-disposition", "attachment;filename=" + fileName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".docx");
                Response.BinaryWrite(ms.ToArray());
                //Response.BinaryWrite(byteArray);
                Response.End();
            }

            //string eFilePath = "123" + DateTime.Now.ToString();
            //byte[] byteArray;

            //if (Request.Browser.Browser == "IE")
            //    eFilePath = HttpUtility.UrlEncode(eFilePath);
            //using (FileStream fs = new FileStream(eFilePath, FileMode.Open))
            //{
            //    byteArray = new byte[fs.Length];
            //    fs.Read(byteArray, 0, byteArray.Length);
            //}

            //Response.Buffer = false;
            //Response.Clear();
            ////Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document; name=" + eFilePath;
            //Response.ContentType = "application/vnd.ms-word; name=" + eFilePath;
            //Response.AddHeader("content-disposition", "attachment;filename=" + eFilePath + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".docx");
            //Response.BinaryWrite(byteArray);
            //Response.End();
        }

        ///// <summary>
        ///// 输出文件到浏览器
        ///// </summary>
        ///// <param name="ms">Excel文档流</param>
        ///// <param name="context">HTTP上下文</param>
        ///// <param name="fileName">文件名</param>
        //private static void RenderToBrowser(MemoryStream ms, HttpContext context, string fileName)
        //{
        //    if (context.Request.Browser.Browser == "IE")
        //        fileName = HttpUtility.UrlEncode(fileName);
        //    byte[] byteArray = new Byte[ms.Length];
        //    ms.Read(byteArray, 0, (int)ms.Length);
        //    HttpContext.Current.Response.Buffer = false;
        //    HttpContext.Current.Response.Clear();
        //    HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document; name=" + fileName;
        //    context.Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
        //    context.Response.BinaryWrite(ms.ToArray());
        //    HttpContext.Current.Response.BinaryWrite(byteArray);
        //    HttpContext.Current.Response.End();
        //}


        public ActionResult ToExcel()
        {
            return View();
        }

        [HttpPost]
        public void ToExcel(FormCollection fc)
        {
            string filePath = Server.MapPath("/Template/Excel/e1.xlsx");
            using (MemoryStream ms = NPOIHelp.ExportExcel(filePath))
            {
                string fileName = "123" + DateTime.Now.ToString();
                if (Request.Browser.Browser == "IE")
                    fileName = HttpUtility.UrlEncode(fileName);
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document; name=" + fileName;
                //Response.ContentType = "application/vnd.ms-word; name=" + fileName;
                Response.AddHeader("content-disposition", "attachment;filename=" + fileName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
                Response.BinaryWrite(ms.ToArray());
                Response.End();
            }
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