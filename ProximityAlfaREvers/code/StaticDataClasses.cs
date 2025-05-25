using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace engine
{
    public static class Input //в основоном gpt
    {

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        public static bool IsPressed(Keys key)
        {
            return (GetAsyncKeyState((int)key) & 0x8000) != 0;
        }
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out Point lpPoint);
        public struct Point
        {
            public int x;
            public int y;
        }

        public static Vector2Int GetMousePosAbs()
        {
            GetCursorPos(out Point p);
            return new Vector2Int(p.x, p.y);
        }
        [DllImport("user32.dll")]
        static extern bool ScreenToClient(IntPtr hWnd, ref Point lpPoint);

        public static Vector2Int GetMousePosRel()
        {
            GetCursorPos(out Point point);
            ScreenToClient(OutputWindow.hwnd, ref point);
            return new Vector2Int(point.x, point.y);
        }
    }
}