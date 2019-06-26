using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapeEngine
{
    static public class GameMain
    {

        static private GameLoopExecutor gameLoopExecutor;
        static private OpenGL gl;
        static private OpenGLControl glGUI_Element;

        static public void GameMainBegin(OpenGLControl glControl, StateSystemManager stateSystemManager)
        {
            gl = glControl.OpenGL;
            glGUI_Element = glControl;
        }

            gameLoopExecutor = new GameLoopExecutor(GameLoop);
        }

        static void GameLoop(double elapsedTimeMs)
        {

            if (glGUI_Element.RenderTrigger == RenderTrigger.Manual)
            {
                glGUI_Element.Refresh();
            }
        }
    }
}
