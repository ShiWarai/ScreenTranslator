using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pranas;
using IronOcr;

namespace ScreenTranslator_MainApp.Model
{
    class ScreenText : TextInitor
    {
        /// <summary>
        /// x0,y0,x1,y1 are screen coordinates of text
        /// </summary>
        public ScreenText(MouseRectangle coordinates) : base()
        {
            text = 
                TextFromImage(ScreenShot(coordinates.First.X, coordinates.First.Y, coordinates.Second.X, coordinates.Second.Y));
        }

        private string TextFromImage(Image image)
        {
            var ORC = new IronOcr.AutoOcr();
            var result = ORC.Read(image);
            return result.Text;
        }

        private Image ScreenShot(int x0,int y0,int x1,int y1)
        {
            System.Drawing.Image BM = Pranas.ScreenshotCapture.TakeScreenshot();
            BM = CutImage(new Bitmap(BM),new Rectangle(x0,y0,x1,y1));
            return BM;
        }

        private Bitmap CutImage(Bitmap src, Rectangle rect)
        {

            Bitmap bmp = new Bitmap(src.Width, src.Height); //создаем битмап

            Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(src, 0, 0, rect, GraphicsUnit.Pixel); //перерисовываем с источника по координатам

            return bmp;
        }
    }
}
