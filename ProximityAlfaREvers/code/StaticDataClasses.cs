using System;
using System.Runtime.InteropServices;

namespace engine
{
    public static class Input
    {
    
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        public static bool IsPressed(Keys key)
        {
            return (GetAsyncKeyState((int)key) & 0x8000) != 0;
        }
    
    }
}