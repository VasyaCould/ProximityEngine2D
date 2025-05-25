using System;
using SkiaSharp;

namespace engine
{
    public class Vector2
    {
        public float x;
        public float y;
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public Vector2()
        {
            this.y = 0;
            this.x = 0;
        }
    }
    public class Vector2int
    {
        public int x;
        public int y;
        public Vector2int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Vector2int()
        {
            this.y = 0;
            this.x = 0;
        }
    }
    public class Vector2Int
    {
        public int x;
        public int y;
        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Vector2Int()
        {
            this.y = 0;
            this.x = 0;
        }
    }
    public class PixArray
    {
        public byte[] img = new byte[4] { 0, 0, 0, 0 };
        public bool rgba;
        public int Width;
        public int Height;

        public PixArray() { }
        public PixArray(int Width, int Height, bool rgba)
        {
            img = new byte[Width * Height * (rgba ? 4 : 3)];
            this.rgba = rgba;
            this.Width = Width;
            this.Height = Height;
        }
        public PixArray(bool rgba) { this.rgba = rgba; }
        public PixArray(string path)
        {
            // try
            // {
            //     using (var image = new MagickImage(path))
            //     {
            //         Width = (int)image.Width;
            //         Height = (int)image.Height;
            //         this.rgba = image.HasAlpha;


            //         // Console.WriteLine(image.GetPixels().ToByteArray(PixelMapping.RGB).Length);

            //         if (rgba) img = image.GetPixels().ToByteArray(PixelMapping.BGRA) ?? new byte[4] { 0, 0, 0, 0 };// magic image is cringe!
            //         if(!rgba) img = image.GetPixels().ToByteArray(PixelMapping.BGR) ?? new byte[3] { 0, 0, 0};
            //     }
            // }
            // catch { }

            // using (Image<Rgba32> image = Image.Load<Rgba32>(path))
            // {
            //     Width = image.Width;
            //     Height = image.Height;
            //     img = new byte[Width * Height * 4];
            //     image.CopyPixelDataTo(img);
            // }
            using (var bmp = SKBitmap.Decode(path))
            {
                img = bmp.Bytes;
                Width = bmp.Width;
                Height = bmp.Height;
                if (bmp.AlphaType == SKAlphaType.Unpremul) rgba = true;
                else rgba = false;
                bgrToRgb(img);
            }
        }
        public static void bgrToRgb(byte[] img)
        {
            for (int i = 0, j = 0; i < img.Length; i += 4, j += 3)
            {
                byte r = img[i + 0];
                byte g = img[i + 1];
                byte b = img[i + 2];

                img[j + 0] = r;
                img[j + 1] = g;
                img[j + 2] = b;
            }
        }
        public void bgraToRgba()
        {
            for (int i = 0, j = 0; i < img.Length; i += 4, j += 3)
            {
                byte r = img[i + 0];
                byte g = img[i + 1];
                byte b = img[i + 2];
                byte a = img[i + 3];

                img[j + 0] = r;
                img[j + 1] = g;
                img[j + 2] = b;
                img[j + 3] = a;
            }
        }

        [Obsolete("Этот метод устарел. Он выводит текущий pixArray на экран напрямую заменяя его пиксели (возможно) без учета прозрачности, может быть перекрыт")]
        public void show(Vector2int? pos = null, float? rotation = null)
        {
            pos = pos ?? new Vector2int(0, 0);
            for (int x = 1; x + pos.x < OutputWindow.Width && x < this.Width; x++)
            {
                for (int y = 1; y + pos.y < OutputWindow.Height && y < this.Height; y++)
                {
                    OutputWindow.img.SetPixel(x + pos.x, y + pos.y, this.GetPixel(x, y));
                }
            }
        }
        // public void rgbaToRgb()
        // {
        //     if (rgba)
        //     {
        //         byte[]? newImg = new byte[Width * Height * 3];

        //         int j = 0;
        //         for (int i = 0; i < img.Length; i++)
        //         {
        //             if ((i + 1) % 4 != 0)
        //             {
        //                 newImg[j++] = img[i];
        //                 // Console.WriteLine(newImg[j]); 
        //             }
        //         }
        //         img = newImg;
        //         newImg = null; //just clearing memory (i know that c# clears memory automatically
        //     }
        // }
        public class Color
        {
            public byte r;
            public byte g;
            public byte b;
            public byte a;
            public Color(byte r, byte g, byte b, byte? a = null)
            {
                this.r = r;
                this.g = g;
                this.b = b;
                this.a = a ?? (byte)255;
            }
        }
        public int GetIndexR(int x, int y)
        {
            return (x + Width * (y - 1)) * (rgba ? 4 : 3) - 1;
        }
        public int GetIndexG(int x, int y)
        {
            return (x + Width * (y - 1)) * (rgba ? 4 : 3);
        }
        public int GetIndexB(int x, int y)
        {
            return ((x + Width * (y - 1)) * (rgba ? 4 : 3)) + 1;
        }
        public int GetIndexA(int x, int y)
        {
            return rgba ? ((x + Width * (y - 1)) * (rgba ? 4 : 3)) + 2 : (byte)255;
        }
        public Color GetPixel(int x, int y)
        {
            try
            {
                return new Color(
                    img[GetIndexR(x, y)],
                    img[GetIndexG(x, y)],
                    img[GetIndexB(x, y)],
                    rgba ? img[GetIndexA(x, y)] : (byte)255
                );
            }
            catch { return new Color(0, 0, 0, 0); }
        }
        public void SetPixel(int x, int y, Color color)
        {
            img[GetIndexR(x, y)] = color.r;
            img[GetIndexG(x, y)] = color.g;
            img[GetIndexB(x, y)] = color.b;
            if (rgba) img[GetIndexA(x, y)] = color.a;
        }
    }
    // class Image
    // {
    //     public string directory;
    //     public float scale;
    //     public PixArray img;

    //     // public Bitmap img;

    //     // public   

    //     // modifiers
    //     public Image(string directory, float scale)
    //     {
    //         img = new Bitmap(directory);
    //         this.directory = directory;
    //         this.scale = scale;
    //     }
    //     public Image(string directory)
    //     {
    //         img = new Bitmap(directory);
    //         this.directory = directory;
    //         this.scale = 1;
    //     }
    //     public Image(float size)
    //     {
    //         this.directory = "";
    //         this.scale = size;
    //     }
    //     public Image()
    //     {
    //         this.directory = "";
    //         this.scale = 1;
    //     }
    //     //all other
    //     // public void DeleteTransparent()
    //     // {
    //         //bool ok;
    //         //Vector2Int size = new Vector2Int();
    //         //for (int i = 1; i < img.Width; i++)
    //         //{
    //         //    ok = true;
    //         //    for (int i2 = 1; i2 < img.Height; i2++)
    //         //    {
    //         //        if (img.GetPixel(i, i2) != Color.Transparent) ok = false;
    //         //    }
    //         //    // delete column
    //         //    if(ok) size.x++;

    //         //}
    //         //Bitmap newImg = new Bitmap(size.x, size.y);
    //         //size = new Vector2Int();
    //         //for (int i = 1; i < img.Height; i++)
    //         //{
    //         //    for (int i2 = 1; i2 < img.Width; i2++)
    //         //    {
    //         //        if (img.GetPixel(i, i2) != Color.Transparent) size.x++;
    //         //    }
    //         //    // delete strings
    //         //    size.y++;
    //         //    Bitmap newImg = new Bitmap(size.x, size.y);
    //         //}
    //     // }
    //     // public void ImportImg(string? directory = null)
    //     // {
    //     //     try
    //     //     {
    //     //         if (directory != null) img = new Bitmap(directory);
    //     //     }
    //     //     catch { }

    //     //     DeleteTransparent();
    //     // }
    //     public void ShowImg()
    //     {
    //         if (img != null)
    //         {
    //             for (int i = 1; i < img.Width && i < engine.OutputWindow.Width; i++)
    //             {
    //                 for (int i2 = 1; i2 < img.Height && i2 < engine.OutputWindow.Height; i2++)
    //                 {
    //                     engine.OutputWindow.img.SetPixel(x: i, y: i2, img.GetPixel(i, i2));

    //                 }
    //             }
    //         }
    //         // MainThread.form1.pictureBox1.Image = MainThread.form1.canvas;
    //         // MainThread.form1.pictureBox1.Dock = DockStyle.Fill;
    //     }
    //     // public void refresh()
    //     // {
    //     //     PixArray newImage = new(Convert.ToInt32(img.Width * scale) + 1, Convert.ToInt32(img.Height * scale) + 1, img.rgba);
    //     //     for (int iW = 1; iW < img.Width; iW++)
    //     //     {
    //     //         for (int iH = 1; iH < img.Height; iH++)
    //     //         {
    //     //             if (Convert.ToInt32(iW * scale) < newImage.Width && Convert.ToInt32(iH * scale) < newImage.Height)
    //     //             {
    //     //                 newImage.SetPixel(Convert.ToInt32(iW * scale), Convert.ToInt32(iH * scale), img.GetPixel(iW, iH));
    //     //             }

    //     //         }
    //     //     }
    //     //     img = newImage;
    //     // }
    //     // public void rotate() { }
    //     //pubilc static void cah
    // }
    // components
    
}
