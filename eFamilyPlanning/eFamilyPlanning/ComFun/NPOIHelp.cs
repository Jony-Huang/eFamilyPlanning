using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using NPOI;
using NPOI.XWPF.UserModel;

namespace eFamilyPlanning.ComFun
{
    public static class NPOIHelp
    {
        public static MemoryStream ExportDoc(string sourcePath)
        {

            string filepath = sourcePath;
            using (FileStream stream = File.OpenRead(filepath))
            {
                XWPFDocument doc = new XWPFDocument(stream);
                //遍历段落
                foreach (var para in doc.Paragraphs)
                {
                    ReplaceKey(para);
                }
                //遍历表格
                var tables = doc.Tables;
                foreach (var table in tables)
                {
                    foreach (var row in table.Rows)
                    {
                        foreach (var cell in row.GetTableCells())
                        {
                            foreach (var para in cell.Paragraphs)
                            {
                                ReplaceKey(para);
                            }
                        }
                    }
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    doc.Write(ms);
                    return ms;
                }
            }

        }
        private static void ReplaceKey(XWPFParagraph para)
        {
            //BLL.XmxxBLL XmxxBLL = new BLL.XmxxBLL();
            //Model.Xmxx model = new Model.Xmxx();
            //model = XmxxBLL.GetModel(20);

            string text = para.ParagraphText;
            var runs = para.Runs;
            string styleid = para.Style;
            for (int i = 0; i < runs.Count; i++)
            {
                var run = runs[i];
                text = run.ToString();
                //Type t = model.GetType();
                //PropertyInfo[] pi = t.GetProperties();
                //foreach (PropertyInfo p in pi)
                //{
                //    if (text.Contains("{$xmxx." + p.Name + "}"))
                //    {
                //        text = text.Replace("{$xmxx." + p.Name + "}", p.GetValue(model, null).ToString());
                //    }
                //}
                if (text.Contains("dd"))
                {
                    text = text.Replace("dd", "This is NPOI word");
                    
                }
                runs[i].SetText(text, 0);
                runs[i].SetFontFamily("宋体", FontCharRange.None);
                runs[i].FontSize = 10;
            }
        }
        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    using (MemoryStream ms = Export())
        //    {
        //        Response.ContentType = "application/vnd.ms-word";
        //        Response.ContentEncoding = Encoding.UTF8;
        //        Response.Charset = "";
        //        Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("123.doc", Encoding.UTF8));
        //        Response.BinaryWrite(Export().GetBuffer());
        //        Response.End();
        //    }
        //}

        //private void ExportExcel(string fileName)
        //{

        //    //byte[] byteArray;

        //    //if (Request.Browser.Browser == "IE")
        //    //    eFilePath = HttpUtility.UrlEncode(eFilePath);
        //    //using (FileStream fs = new FileStream(eFilePath, FileMode.Open))
        //    //{
        //    //    byteArray = new byte[fs.Length];
        //    //    fs.Read(byteArray, 0, byteArray.Length);
        //    //}

        //    //Response.Buffer = false;
        //    //Response.Clear();
        //    //Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document; name=" + eFilePath;
        //    //Response.AddHeader("content-disposition", "attachment;filename=" + fileName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        //    //Response.BinaryWrite(byteArray);
        //    //Response.End();
        //}
    }
}