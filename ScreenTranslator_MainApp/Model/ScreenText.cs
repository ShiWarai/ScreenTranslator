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
        public ScreenText((MouseCoordinates FirstPos, MouseCoordinates SecondPos) coordinates) : base()
        {
            found_text = 
                TextFromImage(ScreenShot(coordinates.FirstPos.X, coordinates.FirstPos.Y, coordinates.SecondPos.X, coordinates.SecondPos.Y));
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
            BM = this.Crop(BM,new Rectangle(x0,y0,x1,y1));
            BM.Save("temp.png");
            return BM;
        }

        private System.Drawing.Image Crop(System.Drawing.Image image, Rectangle selection)
        {
            Bitmap bmp = image as Bitmap;

            // Check if it is a bitmap:
            if (bmp == null)
                throw new ArgumentException("No valid bitmap");

            // Crop the image:
            Bitmap cropBmp = bmp.Clone(selection, bmp.PixelFormat);

            // Release the resources:
            image.Dispose();

            return cropBmp;
        }
    }
}
