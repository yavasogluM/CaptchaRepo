using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;

namespace Captcha.Web.Helper
{
    public class DrawHelper
    {
        public static System.Drawing.Image DrawImage(string ImageText)
        {
            #region Image Process
            Random rand = new Random();
            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(130, 30))
            using (var gfx = Graphics.FromImage((Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));
                gfx.DrawString(ImageText, new Font("Verdana", 15), Brushes.Gray, 2, 3);
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                Image image = (Image)bmp;
                return image;
            }
            #endregion
        }

        public static string DrawImage(string ImageText, string RootPath)
        {
            try
            {
                Image image = new Bitmap(200, 50);

                Graphics graph = Graphics.FromImage(image);

                graph.Clear(Color.Azure);

                Pen pen = new Pen(Brushes.Black);

                graph.DrawString(ImageText,
                new Font(new FontFamily("Arial"), 15, FontStyle.Bold),
                Brushes.Blue, new PointF(10, 10));

                string fileName = "graph.jpeg";
                string path = System.IO.Path.Combine(RootPath + fileName);
                image.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);

                return fileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
