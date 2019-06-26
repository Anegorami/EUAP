using RapeEngine.GameStates;
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
        static private StateSystemManager gameGlobalStateManager;

        static public void GameMainBegin(OpenGLControl glControl, StateSystemManager stateSystemManager)
        {
            gl = glControl.OpenGL;
            glGUI_Element = glControl;
            gameGlobalStateManager = stateSystemManager;

            gl.ClearColor(0, 0, 0, 1);

            gameLoopExecutor = new GameLoopExecutor(GameLoop);
        }

        static void GameLoop(double elapsedTimeMs)
        {
            gameGlobalStateManager.Update(elapsedTimeMs);
            gameGlobalStateManager.Render();

            if (glGUI_Element.RenderTrigger == RenderTrigger.Manual)
            {
                glGUI_Element.Refresh();
            }
        }
    }
}
