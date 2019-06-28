using RapeEngine.GameStates;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapeEngine
{
    /// <summary>
    /// Class representing the overall entry point for game logic, IE the 'main()' for the video game. 
    /// </summary>
    /// Static because there's no real need for seperate instantiations for the one main game.
    static public class GameMain
    {
        static private GameLoopExecutor gameLoopExecutor;
        static private OpenGL gl;
        static private OpenGLControl glGUI_Element;
        static private StateSystemManager gameStateManager;

        /// <summary>
        /// Begins the game. Sets up game states, initializes core elements, and starts the game loop.
        /// </summary>
        /// <param name="glControl">The OpenGLControl the app's windows form is using to display the rendering viewport.</param>
        /// <param name="renderer">The system renderer, if one has been set up beforehand. Otherwise creates its own.</param>
        static public void GameMainBegin(OpenGLControl glControl, Renderer renderer = null)
        {
            gl = glControl.OpenGL;
            glGUI_Element = glControl;

            if(null == renderer)
            {
                renderer = new Renderer(gl);
            }

            gameStateManager = new StateSystemManager();

            // WIP just messing with states and stuff.
            gameStateManager.AddState(new SplashScreenState(gameStateManager, renderer));
            gameStateManager.AddState(new TitleMenuState(gameStateManager, renderer));
            gameStateManager.AddState(new GameStates.TestStates.DrawSpriteState(renderer));

            gameStateManager.SetState(GameStates.TestStates.DrawSpriteState.STATE_ID);

            // BEGIN GAME LOOP WUZZAH
            gameLoopExecutor = new GameLoopExecutor(GameLoop);
        }

        /// <summary>
        /// The main loop for the game. Gets user input, updates game state, tells game state to render all its visual/audio information, tells renderer (if it's manually triggered) to output 
        /// rendering information to the gui
        /// </summary>
        /// <param name="elapsedTimeMs"></param>
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
