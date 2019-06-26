using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapeEngine.GameStates
{
    public class SplashScreenState: IGameObject
    {
        public const string STATE_ID = "splashScreenState";
        private const int TIME_SPLASH_ON_SCREEN_SECONDS = 3;

        private StateSystemManager stateManager;
        private float timeLeftOnScreen = TIME_SPLASH_ON_SCREEN_SECONDS;
        private OpenGL gl;

        public SplashScreenState(StateSystemManager stateSystemManager, OpenGL openGL)
        {
            stateManager = stateSystemManager;
            gl = openGL;
        }
        public void Update(double elapsedTimeMs)
        {
            timeLeftOnScreen -= (float)(elapsedTimeMs / 1000);

            if(timeLeftOnScreen <= 0)
            {
                timeLeftOnScreen = TIME_SPLASH_ON_SCREEN_SECONDS;

            }
        }

        public void Render()
        {
            gl.ClearColor(1, 1, 1, 1);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT);
            gl.Finish();
        }
    }
}
