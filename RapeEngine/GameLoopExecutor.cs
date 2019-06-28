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
    /// <summary>
    /// Class for managing executing the global game loop. Interfacing a game loop with 
    /// an event-driven form needs some processing on its own, encapsulated into here. Calls game loop whenever the application is idle.
    /// Primarily used by just passing the game loop to it as a callback and letting it do its
    /// thing.
    /// </summary>
    public class GameLoopExecutor
    {
        // Variables for calling back the gameloop 
        public delegate void LoopCallback(double elapsedTimeMs);
        private readonly LoopCallback callback;

        //Variables for tracking loop timing.
        private Stopwatch stopWatch;
        private long previousTime;

        /// <summary>
        /// Standard constructor. Takes in the game loop as a callback and starts calling it whenever it can.
        /// </summary>
        /// <param name="loopCallback"></param>
        public GameLoopExecutor(LoopCallback loopCallback)
        {
            callback = loopCallback;
            stopWatch = new Stopwatch();
            stopWatch.Start();
            previousTime = 0;
            Application.Idle += new EventHandler(OnApplicationEnterIdle);
        }

        /// <summary>
        /// Event handler for application entering idle, this is where the game loop actually gets called. 
        /// Monitors the app state and continues to loop when the app is idle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Returns whether or not the app is still idle. Uses the interop PeekMessage function from User32.dll 
        /// to find out -- window is idle when windows is giving it no further messages.
        /// </summary>
        /// <returns></returns>
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
