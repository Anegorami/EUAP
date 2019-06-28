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

        static public void GameMainBegin(OpenGLControl glControl, Renderer renderer = null)
        {
            gl = glControl.OpenGL;
            glGUI_Element = glControl;

            if(null == renderer)
            {
                renderer = new Renderer(gl);
            }

            gameStateManager = new StateSystemManager();

            gameStateManager.AddState(new SplashScreenState(gameStateManager, renderer));
            gameStateManager.AddState(new TitleMenuState(gameStateManager, renderer));
            gameStateManager.AddState(new GameStates.TestStates.DrawSpriteState(renderer));

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
