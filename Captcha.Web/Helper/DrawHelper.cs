using System;
using System.Drawing;

namespace Captcha.Web.Helper
{
    public class DrawHelper
    {
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
