using System;   


namespace engine
{
    static class MainGameClass
    {
        public static void Start()
        {
            // PixArray png = new("k.png");
            // png.show();
            // 

            // img.show();XZ
            GameObject i = new("gmObj", "p1.png", new Vector2(0, 0), 0);
            Console.WriteLine(CurScene.gameObjectsOnScene[0].programName);
            // PixArray b = new("p21.png");
            // b.show();
            // // PixArray a = new("p1.png");
            // // a.show();
        }
        public static void Update()
        {
            // Console.WriteLine(Input.IsPressed(Keys.A));
            Console.WriteLine($"{Input.GetMousePosRel().x} {Input.GetMousePosRel().y}");
            // Bitmap 
            // // Console.WriteLine(dst[0].r);aaaaaaaa
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
            /*cd ProximityAlfaREvers
            cd code
            dotnet run*/
            // using (var bitmap = SKBitmap.Decode(input)
            // OutputWindow.img = new PixArray("p21.png", true);

        }
    }
}