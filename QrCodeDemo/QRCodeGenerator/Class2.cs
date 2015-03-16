using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;

namespace QRCodeGenerator
{
    public class Generator2
    {
        public void Generate(IEnumerable<CodeInfo> infos)
        {
            using (var ms = new MemoryStream())
            {
                foreach (CodeInfo codeInfo in infos)
                {
                    bool result = GetQRCode(codeInfo.Content, codeInfo.FileName, codeInfo.BackgroundPath, codeInfo.Width, codeInfo.Height, codeInfo.X, codeInfo.Y, ms);


                }


            }


        }

        public bool GetQRCode(string strContent, string path, string backgroundPath, int width, int height, int x, int y, MemoryStream ms)
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

                Bitmap b = new Bitmap(width, height);
                Graphics g = Graphics.FromImage(b);
                using (FileStream fs = new FileStream(backgroundPath, FileMode.Open))
                {
                    Bitmap bmp = new Bitmap(fs);
                    Bitmap bmp2 = new Bitmap(ms);

                  //  Graphics g2 = Graphics.FromImage(bmp);
                    g.DrawImage(bmp2, new Rectangle(x, y, width, height));

                    bmp.Save(path);
                }

            }
            else
            {
                return false;
            }
            return true;
        }   
    }
}
