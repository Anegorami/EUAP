using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace RapeEngine.GameStates
{
    /// <summary>
    /// State for managing, displaying and executing the game's title menu.
    /// </summary>
    public class TitleMenuState : IGameState
    {
        public const string STATE_ID = "TitleMenuState";
        private readonly Renderer renderer;
        private readonly StateSystemManager stateManager;

        public string StateId { get { return STATE_ID; } }

        /// <summary>
        /// Standard constructor, initializes internal variables and waits for update cycle to begin.
        /// </summary>
        /// <param name="stateSystemManager">The global state system manager.</param>
        /// <param name="renderer">The system renderer.</param>
        public TitleMenuState(StateSystemManager stateSystemManager, Renderer renderer)
        {
            this.renderer = renderer;
            stateManager = stateSystemManager;
        }
        public void Update(double elapsedTimeMs)
        {
            
        }

        public void Render()
        {
            renderer.SetClearScreenColor(0, 0, 0, 255);
            renderer.ClearScreen();

            renderer.BeginVertexDraw();
            {
                renderer.DrawImmediateModeVertex(new Vector3D(-50f, 0f, -1f), Color.FromArgb(255, 0, 255, 0));
                renderer.DrawImmediateModeVertex(new Vector3D(50f, 0f, -1f), Color.FromArgb(255, 255, 0, 0));
                renderer.DrawImmediateModeVertex(new Vector3D(50f, 50f, -1f), Color.FromArgb(255, 0, 0, 255));
            }
            renderer.EndVertexDraw();
            renderer.Finish();
        }
    }
}
