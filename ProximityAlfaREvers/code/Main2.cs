using System;   
using System.IO;
using System.Transactions;
// File.WriteAllText(path, text);


namespace engine
{
    static class MainGameClass
    {
        public static PixArray? pic;
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
                else Console.Write("try again");
            }
            Console.WriteLine("To change settings or pic press q (on window)\nTo draw hold left mouse button\nTo move pic hold right mouse button");
        }
        public static void Update()
        {
            if (Input.IsPressed(Keys.Q))
            {
                Settings();
            }
        }
        public static void Settings()
        {
            Console.WriteLine("1-change path to pic 2-");
        }
    }
}