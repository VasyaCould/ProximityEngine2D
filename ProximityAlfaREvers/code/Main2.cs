using System;   
using System.IO;
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
            Console.WriteLine("To change settings or pic press q (on window)\nTo draw hold left mouse button\nTo move pic hold right mouse button");
        }
        private static Vector2int mapPos = new();
        private static Vector2int LastMousePos = new();
        private static bool firstClick = true;
        public static void Update()
        {
            if (Input.IsPressed(Keys.Q))
            {
                Settings();
            }
            if (Input.IsPressed(Keys.MouseL))
            {
                if (firstClick)
                {
                    LastMousePos = new(Input.GetMousePosRel().x, Input.GetMousePosRel().y);
                    firstClick = false;
                }
                mapPos = new(Input.GetMousePosRel().x - LastMousePos.x + mapPos.x, Input.GetMousePosRel().y - LastMousePos.y + mapPos.y);
                LastMousePos = new(Input.GetMousePosRel().x, Input.GetMousePosRel().y);
            }
            else firstClick = true;

            pic.show(mapPos, fill: true);
        }
        public static void Settings()
        {
            while (true)
            {
                Console.WriteLine("0-to exit\n1-to change abs path to png or jpg\n2-to change abs path where you want to save chars\n3-to change name of cur char");
                switch (Console.ReadLine())
                {
                    case "0":
                        Console.WriteLine("To change settings or pic press q (on window)\nTo draw hold left mouse button\nTo move pic hold right mouse button");
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