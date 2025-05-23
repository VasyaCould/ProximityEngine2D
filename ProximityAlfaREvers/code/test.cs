// using System;
// using System.Drawing;
// using System.Drawing.Imaging;

// class Test
// {
//     public static void Main()
//     {
//         var image = Image.Load<Rgb24>("p21.png");
//         Rgb24[] dst = new Rgb24[image.Width * image.Height];
//         image.CopyPixelDataTo(dst);
//         // Console.WriteLine(dst[0].r);
//         for (int x = 1; x < OutputWindow.Width && x < image.Width; x++)
//         {
//             for (int y = 1; y < OutputWindow.Height && y < image.Height; y++)
//             {
//                 OutputWindow.img.SetPixel(x, y, new byte[] { dst[0].R, dst[1].G, dst[2].B });
//             }
//         }
//     }
// }