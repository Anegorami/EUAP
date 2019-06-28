using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapeEngine.GameStates
{
    /// <summary>
    /// State for rendering the splash screen. Displays splash screen followed by 
    /// requesting a state change to the title menu.
    /// </summary>
    public class SplashScreenState: IGameState
    {
        public const string STATE_ID = "SplashScreenState";
        private const int TIME_SPLASH_ON_SCREEN_SECONDS = 3;

        private readonly StateSystemManager stateManager;
        private readonly Renderer renderer;
        private float timeLeftOnScreen;

        public string StateId { get { return STATE_ID; } }

        /// <summary>
        /// Standard constructor, sets up internal variables and waits for update sequence to begin. 
        /// </summary>
        /// <param name="stateSystemManager">The global state manager so the splash screen can request a transition when enough time has passed</param>
        /// <param name="renderer">The system renderer</param>
        public SplashScreenState(StateSystemManager stateSystemManager, Renderer renderer)
        {
            stateManager = stateSystemManager;
            this.renderer = renderer;

            timeLeftOnScreen = TIME_SPLASH_ON_SCREEN_SECONDS;
        }
        /// <summary>
        /// Update will check how long the splash screen render has been up, and change to the next state in the
        /// startup sequence if it's been longer than its set timeout value (see constants above).
        /// </summary>
        /// <param name="elapsedTimeMs">The amount of time that has passed since the last update frame was processed</param>
        public void Update(double elapsedTimeMs)
        {
            timeLeftOnScreen -= (float)(elapsedTimeMs / 1000f);

            if(timeLeftOnScreen <= 0)
            {
                timeLeftOnScreen = TIME_SPLASH_ON_SCREEN_SECONDS;
                stateManager.SetState(TitleMenuState.STATE_ID);
            }
        }

        /// <summary>
        /// Renders the splash screen.
        /// </summary>
        public void Render()
        {
            //Just white blank the screen for now.
            renderer.SetClearScreenColor(Color.White);
            renderer.ClearScreen();
            renderer.Finish();
        }
    }
}
