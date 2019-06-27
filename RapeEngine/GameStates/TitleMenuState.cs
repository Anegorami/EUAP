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
    public class TitleMenuState : IGameState
    {
        public static string STATE_ID = "TitleMenuState";
        private readonly Renderer renderer;
        private readonly StateSystemManager stateManager;

        public string StateId { get { return STATE_ID; } }

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
                renderer.DrawImmediateModeVertex(new Vector3D(-50f, 0f, 0f), Color.FromArgb(0, 0, 255, 0));
                renderer.DrawImmediateModeVertex(new Vector3D(50f, 0f, 0f), Color.FromArgb(0, 255, 0, 0));
                renderer.DrawImmediateModeVertex(new Vector3D(50f, 50f, 0f), Color.FromArgb(0, 0, 0, 255));
            }
            renderer.EndVertexDraw();
            renderer.Finish();
        }
    }
}
