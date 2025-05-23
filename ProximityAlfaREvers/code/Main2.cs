using System;


namespace engine
{
    class MainGameClass
    {
        protected static void Update()
        {
            // Bitmap 
            // // Console.WriteLine(dst[0].r);
            // for (int x = 1; x < OutputWindow.Width && x < image.Width; x++)
            // {
            //     for (int y = 1; y < OutputWindow.Height && y < image.Height; y++)
            //     {
            //         OutputWindow.img.SetPixel(x, y, new byte[] { dst[0].R, dst[1].G, dst[2].B });
            //     }
            // }
            // Console.WriteLine(new Bitmap("p21.png").GetPixel(1, 1).);
            // Out
            // if ()(var input = System.IO.File.OpenRead(path))
            // using (var bitmap = SKBitmap.Decode(input)
            // OutputWindow.img = new PixArray("p21.png", true);

        }
        protected static void Start()
        {
            PixArray png = new("k.png");
            png.show();
            // 

            // img.show();XZ
        }
    }
}