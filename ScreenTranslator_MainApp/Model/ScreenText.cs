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
                TextFromImage(ScreenShot(coordinates));
        }

        private string TextFromImage(Image image)
        {
            var ORC = new IronOcr.AutoOcr();
            var result = ORC.Read(image);
            return result.Text;
        }

        private Image ScreenShot(MouseRectangle coordinates)
        {
            System.Drawing.Image BM = Pranas.ScreenshotCapture.TakeScreenshot();

            BM = CutImage(new Bitmap(BM),GetCorrectRectangle(coordinates));
            BM.Save("Test.bmp");

            return BM;
        }

        /// <summary>
        /// Возвращает прямоугольник по координатам
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        private Rectangle GetCorrectRectangle(MouseRectangle coordinates)
        {
            return new Rectangle(
                Math.Min(coordinates.First.X, coordinates.Second.X),
                Math.Min(coordinates.First.Y, coordinates.Second.Y),
                Math.Abs(coordinates.First.X - coordinates.Second.X),
                Math.Abs(coordinates.First.Y - coordinates.Second.Y));
        }

        private Bitmap CutImage(Bitmap src, Rectangle rect)
        {

            Bitmap bmp = new Bitmap(src.Width, src.Height);

            Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(src, 0, 0, rect, GraphicsUnit.Pixel);

            return bmp;
        }
    }
}
