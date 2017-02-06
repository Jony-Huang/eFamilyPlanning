using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using NPOI;
using NPOI.XWPF.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace eFamilyPlanning.ComFun
{
    public static class NPOIHelp
    {
        public static MemoryStream ExportWord(string templateFileName)
        {

            string filePath = templateFileName;
            using (FileStream stream = File.OpenRead(filePath))
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
                if (text.Contains("XX"))
                {
                    text = text.Replace("XX", "This is NPOI word");
                    
                }
                runs[i].SetText(text, 0);
                runs[i].SetFontFamily("宋体", FontCharRange.None);
                runs[i].FontSize = 12;
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

        public static MemoryStream ExportExcel(string templateFileName)
        {

            string filePath = templateFileName;
            XSSFWorkbook workBook = null;
            XSSFSheet sheet1 = null;
            using (FileStream fs = File.OpenRead(filePath))
            {
                workBook = new XSSFWorkbook(fs);
                sheet1 = (XSSFSheet)workBook.GetSheet("Sheet1");
                //添加或修改WorkSheet里的数据  
                System.Data.DataTable dt = new System.Data.DataTable();
                //dt = DbHelperMySQLnew.Query("select * from t_jb_info where id='" + id + "'").Tables[0];
                //if (dt.Rows.Count > 0)
                //{
                //    if (!string.IsNullOrEmpty(dt.Rows[0]["blrq"].ToString()))
                //    {
                        //sheet.GetRow(2).GetCell(1).SetCellValue("56565");
                        //sheet.GetRow(2).GetCell(2).SetCellValue("hahaha");
                        //sheet.GetRow(2).GetCell(3).SetCellValue(DateTime.Now.ToString());
                //    }
                //}

                // 创建新增行
                for (var i = 1; i <= 10; i++)
                {
                    IRow row1 = sheet1.CreateRow(i);
                    for (var j = 0; j < 10; j++)
                    {
                        //新建单元格
                        NPOI.SS.UserModel.ICell cell = row1.CreateCell(j);

                        // 单元格赋值
                        cell.SetCellValue("");
                    }
                }

                sheet1.GetRow(1).GetCell(0).SetCellValue("56565");
                sheet1.GetRow(1).GetCell(1).SetCellValue("hahaha");
                sheet1.GetRow(1).GetCell(2).SetCellValue(DateTime.Now.ToString());


                sheet1.ForceFormulaRecalculation = true;  
            }

            using (MemoryStream ms = new MemoryStream())
            {
                workBook.Write(ms);
                return ms;
            }
        }
    }
}