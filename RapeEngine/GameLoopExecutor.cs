using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RapeEngine
{
    public class GameLoopExecutor
    {
        public delegate void LoopCallback(double elapsedTimeMs);
        private Stopwatch stopWatch;
        private readonly LoopCallback callback;
        private long previousTime;

        public GameLoopExecutor(LoopCallback loopCallback)
        {
            callback = loopCallback;
            stopWatch = new Stopwatch();
            previousTime = 0;
            Application.Idle += new EventHandler(OnApplicationEnterIdle);
        }

        private void OnApplicationEnterIdle(object sender, EventArgs e)
        {
            long currentTime;

            while(IsAppStillIdle())
            {
                currentTime = stopWatch.ElapsedMilliseconds;

                callback(currentTime - previousTime);

                previousTime = currentTime;
            }
        }

        private bool IsAppStillIdle()
        {
            InteropMessage msg;
            return !PeekMessage(out msg, IntPtr.Zero, 0, 0, 0);
        }

        #region interop stuff for peeking messages

        [StructLayout(LayoutKind.Sequential)]
        private struct InteropMessage
        {
            public IntPtr hWnd;
            public Int32 msg;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public System.Drawing.Point p;
        }

        [System.Security.SuppressUnmanagedCodeSecurity]
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool PeekMessage(out InteropMessage msg,
                                              IntPtr hWnd,
                                              uint messageFilterMin,
                                              uint messageFilterMax,
                                              uint flags);
    }
    #endregion
}
