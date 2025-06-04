using System;   
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
// File.WriteAllText(path, text);


namespace engine
{
    static class MainGameClass
    {
        public static PixArray pic = new PixArray();
        public static string pathToSaveChars = "";
        public static string curCharName = "unknown";
        public static void Start()
        {
            Console.WriteLine("Write abs path to png or jpg");
            while (true)
            {
                try { pic = new PixArray(Console.ReadLine()); break; }
                catch (Exception ex)
                {
                    Console.WriteLine("error: " + ex + "\ntry again");
                }
            }
            Console.WriteLine("Write abs path where you want to save chars");
            while (true)
            {
                try
                {
                    pathToSaveChars = Console.ReadLine();
                    File.WriteAllText($@"{pathToSaveChars}\test.txt", "test");
                    File.Delete($@"{pathToSaveChars}\test.txt");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error: " + ex + "\ntry again");
                }
            }
            Console.WriteLine("Write name of cur char");
            while (true)
            {
                curCharName = Console.ReadLine();
                if (curCharName != "") break;
                else Console.WriteLine("try again");
            }
            Console.WriteLine("To change settings or pic press q (on window)\nTo draw hold left mouse button\nTo move pic hold right mouse button\nTo draw transition from last char hold CTRL\nTo draw transition to next char hold SHIFT");
            OutputWindow.fullScreen = true;
        }
        private static Vector2int mapPos = new();
        private static bool firstClickR = true;
        private static bool firstClickL = true;
        private static Vector2int lastMousePosL = new();//в зельде хочется все фоткать а нипример в м одессе нет и если делать игру то только такую
        private static Vector2int lastMousePosR = new();
        public static void Update()
        {
            if (Input.IsPressed(Keys.Q))
            {
                Settings();
            }
            if (Input.IsPressed(Keys.MouseR))
            {
                if (firstClickR)
                {
                    lastMousePosR = new(Input.GetMousePosRel().x, Input.GetMousePosRel().y);
                    firstClickR = false;
                }
                mapPos = new(Input.GetMousePosRel().x - lastMousePosR.x + mapPos.x, Input.GetMousePosRel().y - lastMousePosR.y + mapPos.y);
                lastMousePosR = new(Input.GetMousePosRel().x, Input.GetMousePosRel().y);
            }
            else firstClickR = true;

            // pic.show(mapPos, fill: true);

            if (Input.IsPressed(Keys.MouseL))
            {
                if (firstClickL)
                {
                    lastMousePosL = new(Input.GetMousePosRel().x, Input.GetMousePosRel().y);
                    firstClickL = false;
                }

                // Vector2int temp = Input.GetMousePosRel();

                try { OutputWindow.img.DrawLine(lastMousePosL, Input.GetMousePosRel(), new Color(255, 0, 0)); } //OutputWindow.img.SetPixel(Input.GetMousePosRel().x, Input.GetMousePosRel().y, new Color(255, 0, 0)); }//
                catch (Exception e) { Console.WriteLine(e); }
                
                // lastMousePosL = temp;

                // OutputWindow.img.DrawLine(new Vector2int(500, 500), new Vector2int(300, 300), new Color(255, 0, 0));
            }
            else firstClickL = true;
            // lastMousePosL = Input.GetMousePosRel();
            
        }
        public static void Settings()
        {
            while (true)
            {
                Console.WriteLine("0-to exit\n1-to change abs path to png or jpg\n2-to change abs path where you want to save chars\n3-to change name of cur char");
                switch (Console.ReadLine())
                {
                    case "0":
                        Console.WriteLine("To change settings or pic press q (on window)\nTo draw hold left mouse button\nTo move pic hold right mouse button\nTo draw transition from last char hold CTRL\nTo draw transition to next char hold SHIFT");
                        return;
                    case "1":
                        Console.WriteLine("This func is not supporting in current version");
                        // while (true)
                        // {
                        //     try { pic.img = ReadPic.ReadPicM(Console.ReadLine()); break; }
                        //     catch (Exception ex)
                        //     {
                        //         Console.WriteLine("error: " + ex + "\ntry again");
                        //     }
                        // }
                        break;
                    case "2":
                        while (true)
                        {
                            try
                            {
                                pathToSaveChars = Console.ReadLine();
                                File.WriteAllText($@"{pathToSaveChars}\test.txt", "test");
                                File.Delete($@"{pathToSaveChars}\test.txt");
                                break;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("error: " + ex + "\ntry again\n");
                            }
                        }
                        break;
                    case "3":
                        while (true)
                        {
                            curCharName = Console.ReadLine();
                            if (curCharName != "") break;
                            else Console.Write("try again");
                        }
                        break;
                }
            }
        }
    }
}