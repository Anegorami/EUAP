using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapeEngine
{
    public class GameMain
    {

        private GameLoopExecutor gameLoopExecutor;
        private OpenGL gl;
        private OpenGLControl glGUI_Element;

        public GameMain(OpenGLControl glControl)
        {
            gl = glControl.OpenGL;
            glGUI_Element = glControl;
        }

        public void GameMainBegin()
        {
            gameLoopExecutor = new GameLoopExecutor(GameLoop);
        }

        void GameLoop(double elapsedTimeMs)
        {

            glGUI_Element.Refresh();
        }
    }
}
