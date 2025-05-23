using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using static Win32;

namespace engine
{
    class OutputWindow : MainGameClass
    {
        public static string? windowName;
        static WndProcDelegate wndProc = WndProc;

        delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        static IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            return DefWindowProc(hWnd, msg, wParam, lParam);//чат gpt конечно молодес но это типа минимальный код для вывода пикселей (массива) и зачем-то шум 
        }
        public const int Width = 1920;
        public const int Height = 1080;

        static IntPtr hwnd;
        static IntPtr hdc;
        static IntPtr memDC;
        static IntPtr dib;
        static IntPtr old;

        public static PixArray img = new(Width, Height, false);
        // public static PixArray img = new("k.png");

        static IntPtr bits;

        static void Main()
        {
            MainGameClass.Start();
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);
            CreateWindowAndBuffer();

            Stopwatch sw = new Stopwatch();

            MSG msg;
            while (true)
            {
                MainGameClass.Update();

                while (PeekMessage(out msg, IntPtr.Zero, 0, 0, 1))
                {
                    TranslateMessage(ref msg);
                    DispatchMessage(ref msg);
                    if (msg.message == 0x0012) return; // WM_QUIT
                }

                sw.Restart();


                // for(int i = 0; i < img.img.Length; i++)img.img[i] = ;
                // Копируем RGB -> DIB (24 bpp, BGR)
                Marshal.Copy(img.img, 0, bits, Width * Height * 3); // copying array to GDI buffer

                BitBlt(hdc, 0, 0, Width, Height, memDC, 0, 0, 0x00CC0020); // show

                sw.Stop();
                int sleep = (int)(16 - sw.ElapsedMilliseconds);
                if (sleep > 0) Thread.Sleep(sleep);
            }
        }

        static void CreateWindowAndBuffer()
        {
            string className = "GDIWindow";

            WNDCLASS wc = new WNDCLASS
            {
                lpfnWndProc = Marshal.GetFunctionPointerForDelegate(wndProc),
                hInstance = GetModuleHandle(null),
                lpszClassName = className
            };
            RegisterClass(ref wc);

            hwnd = CreateWindowEx(0, className, windowName == null ? "VasyaMolodes" : windowName, 0xCF0000, 100, 100, Width, Height, IntPtr.Zero, IntPtr.Zero, wc.hInstance, IntPtr.Zero);
            ShowWindow(hwnd, 5);
            UpdateWindow(hwnd);

            hdc = GetDC(hwnd);
            memDC = CreateCompatibleDC(hdc);

            BITMAPINFO bmi = new BITMAPINFO();
            bmi.bmiHeader.biSize = Marshal.SizeOf<BITMAPINFOHEADER>();
            bmi.bmiHeader.biWidth = Width;
            bmi.bmiHeader.biHeight = -Height; // топ-даун
            bmi.bmiHeader.biPlanes = 1;
            bmi.bmiHeader.biBitCount = 24;
            bmi.bmiHeader.biCompression = 0; // BI_RGB

            dib = CreateDIBSection(memDC, ref bmi, 0, out bits, IntPtr.Zero, 0);
            old = SelectObject(memDC, dib);
        }
    }
}