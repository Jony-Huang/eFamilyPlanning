using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace eFamilyPlanning.Ashx
{
    /// <summary>
    /// Admin 的摘要说明
    /// </summary>
    public class Admin : IHttpHandler, IRequiresSessionState
    {

        HttpRequest Request;
        HttpResponse Response;
        HttpSessionState Session;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            Request = context.Request;
            Response = context.Response;
            Session = context.Session;
            var action = Request["action"];
            switch (action)
            {
                case "createCode": CreateCode(); break;
                case "getSessionCode": GetSessionCode(); break;
                default:
                    break;
            }

        }

        private void GetSessionCode()
        {
            string codeSession = ((Session["checkCode"]).ToString()).ToLower();
            string inputCode = (Request["code"].ToString()).ToLower();

            Response.Write(inputCode == codeSession ? "true" : "false");
        }


        //生产验证码开始
        // 01 GenerateCode方法：主要是用来产生随机字符串。
        /// <summary>
        /// 随机生成字符串
        /// </summary>
        /// <param name="number">字符串长度</param>
        /// <returns></returns>
        protected string GenerateCode(int number)
        {
            string checkCode = string.Empty;
            string vChar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] vcArray = vChar.Split(',');
            int temp = -1;//记录上次随机数值，尽量避免产生几个一样的随机数
            Random rand = new Random();
            for (int i = 1; i < number + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(vcArray.Length);
                if (temp != -1 && temp == t)
                {
                    return GenerateCode(number);
                }
                temp = t;
                checkCode += vcArray[t];
            }

            return checkCode;
        }

        public void CreateCode()
        {
            string check = this.GenerateCode(4);
            Session["checkCode"] = check.ToLower();

            //生成图片开始

            if (check == null || check.Trim() == string.Empty)
            {
                return;
            }
            System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling((check.Length * 20.0)), 50);
            Graphics g = Graphics.FromImage(image);
            try
            {
                Random radom = new Random();
                g.Clear(Color.White);
                for (int i = 0; i < 4; i++)
                {
                    int x1 = radom.Next(image.Width);
                    int x2 = radom.Next(image.Width);
                    int y1 = radom.Next(image.Height);
                    int y2 = radom.Next(image.Height);
                    g.DrawLine(new Pen(Color.Black), x1, y1, x2, y2);
                }
                Font font = new Font("Arial", 20, (FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 2.4f, true);
                g.DrawString(check, font, brush, 4, 12);
                //画图片的前景噪音点
                for (int i = 0; i < 200; i++)
                {
                    int x = radom.Next(image.Width);
                    int y = radom.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(radom.Next()));
                }


                //画图片的边框线
                //g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                Response.ClearContent();
                Response.ContentType = "image/Gif";
                Response.BinaryWrite(ms.ToArray());
            }
            catch { }
            finally
            {
                g.Dispose();
                image.Dispose();
            }

            //生成图片结束
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}