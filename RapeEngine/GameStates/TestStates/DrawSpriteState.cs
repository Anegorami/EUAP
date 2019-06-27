using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapeEngine.GameStates.TestStates
{
    internal class DrawSpriteState : IGameState
    {
        public const string STATE_ID = "DrawSpriteState";
        private readonly OpenGL gl;

        public string StateId { get { return STATE_ID; } }

        internal DrawSpriteState(OpenGL openGl)
        {
            gl = openGl;
        }

        public void Update(double elapsedTimeMs)
        {
            
        }

        public void Render()
        {
            double halfHeight = 100;
            double halfWidth = 100;

            gl.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.PointSize(5.0f);

            gl.Begin(OpenGL.GL_TRIANGLES);
            {
                gl.Vertex(-halfWidth, halfHeight); // top left
                gl.Vertex(halfWidth, halfHeight); // top right
                gl.Vertex(-halfWidth, -halfHeight); // bottom left

                gl.Vertex(halfWidth, halfHeight); // top right
                gl.Vertex(halfWidth, -halfHeight); // bottom right
                gl.Vertex(-halfWidth, -halfHeight); // bottom left
            }
            gl.End();
            gl.Finish();
        }
    }
}
