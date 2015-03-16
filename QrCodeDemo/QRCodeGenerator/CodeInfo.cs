using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRCodeGenerator
{
    public  class CodeInfo
    {
        public string FileName { get; set; }
        public string Content { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int  X { get; set; }
        public int Y { get; set; }
        public string BackgroundPath { get; set; }
        public string CenterImagePath { get; set; }
        public int CenterImgSize { get; set; }
    }
}
