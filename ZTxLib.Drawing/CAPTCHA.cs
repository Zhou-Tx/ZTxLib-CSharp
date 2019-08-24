using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI.WebControls;

namespace ZTxLib.Drawing
{
    /// <summary>
    /// 验证码
    /// </summary>
    public static class CAPTCHA
    {
        /// <summary>
        /// 创建验证码（Bitmap）
        /// </summary>
        /// <param name="text">验证码内容</param>
        /// <returns></returns>
        public static Bitmap CreateBmp(string text) => CreateBytes(text).ToBmp();

        /// <summary>
        /// 创建验证码（Bytes）
        /// </summary>
        /// <param name="text">验证码内容</param>
        /// <returns></returns>
        public static byte[] CreateBytes(string text)
        {
            Bitmap image = new Bitmap(text.Length * 15 + 10, 33);
            Graphics g = Graphics.FromImage(image);
            try
            {
                WebColorConverter ww = new WebColorConverter();
                g.Clear((Color)ww.ConvertFromString("#FAE264"));
                Random random = new Random();
                //画图片的背景噪音线
                for (int i = 0; i < 12; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.LightGray), x1, y1, x2, y2);
                }
                Font font = new Font("Arial", 20, FontStyle.Bold | FontStyle.Italic);
                LinearGradientBrush brush = new LinearGradientBrush(
                    new Rectangle(0, 0, image.Width, image.Height),
                    Color.Blue, Color.Gray, 1.2f, true
                );
                g.DrawString(text, font, brush, 0, 0);
                //画图片的前景噪音点
                for (int i = 0; i < 10; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.White);
                }
                //画图片的边框线
                // g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                MemoryStream ms = new MemoryStream();
                image.Save(ms, ImageFormat.Gif);
                return ms.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        /// <summary>
        /// 随机生成验证码内容
        /// </summary>
        /// <param name="len">验证码长度</param>
        /// <param name="set">字符集</param>
        /// <returns></returns>
        public static string Random(int len, params char[] set)
        {
            string text = string.Empty;
            Random rnd = new Random();
            if (set != null)
                for (int i = 0; i < len; i++)
                    text += set[rnd.Next(set.Length)];
            return text;
        }
    }
}