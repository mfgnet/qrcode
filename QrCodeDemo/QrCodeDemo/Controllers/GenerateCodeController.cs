using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;

namespace QrCodeDemo.Controllers
{
    public class GenerateCodeController : Controller
    {
        //
        // GET: /GenerateCode/

        public ActionResult Index()
        {
          //  using (var ms = new MemoryStream())
            {
                var ms = new MemoryStream();
                string stringtest = "中国inghttp://www.baidu.com/mvc.test?&";
                bool result=GetQRCode(stringtest, ms);
                //Response.ContentType = "image/Png";
                FileStream fs = new FileStream(@"e:\a.jpg", FileMode.OpenOrCreate);
                BinaryWriter w = new BinaryWriter(fs);
                w.Write(ms.ToArray());
                FileStreamResult fsr=new FileStreamResult(ms,"image/Png");
                return File(fs, "image/Png");
            }
            
        }
        public static bool GetQRCode(string strContent, MemoryStream ms)
        {
            ErrorCorrectionLevel Ecl = ErrorCorrectionLevel.M; //误差校正水平   
            string Content = strContent;//待编码内容  
            QuietZoneModules QuietZones = QuietZoneModules.Two;  //空白区域   
            int ModuleSize = 12;//大小  
            var encoder = new QrEncoder(Ecl);
            QrCode qr;
            if (encoder.TryEncode(Content, out qr))//对内容进行编码，并保存生成的矩阵  
            {
                var render = new GraphicsRenderer(new FixedModuleSize(ModuleSize, QuietZones));
                render.WriteToStream(qr.Matrix, ImageFormat.Png, ms);
            }
            else
            {
                return false;
            }
            return true;
        }   
    }
}
