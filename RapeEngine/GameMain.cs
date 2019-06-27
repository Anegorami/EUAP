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
        static private StateSystemManager gameStateManager;

        static public void GameMainBegin(OpenGLControl glControl)
        {
            gl = glControl.OpenGL;
            glGUI_Element = glControl;
            gameStateManager = new StateSystemManager();

            gl.ClearColor(0, 0, 0, 1);

            gameStateManager.AddState(new SplashScreenState(gameStateManager, gl));
            gameStateManager.AddState(new TitleMenuState(gameStateManager, gl));
            gameStateManager.AddState(new GameStates.TestStates.DrawSpriteState(gl));

            gameStateManager.SetState(GameStates.TestStates.DrawSpriteState.STATE_ID);

            gameLoopExecutor = new GameLoopExecutor(GameLoop);
        }

        static void GameLoop(double elapsedTimeMs)
        {
            gameStateManager.Update(elapsedTimeMs);
            gameStateManager.Render();

            if (glGUI_Element.RenderTrigger == RenderTrigger.Manual)
            {
                glGUI_Element.Refresh();
            }
        }
    }
}
