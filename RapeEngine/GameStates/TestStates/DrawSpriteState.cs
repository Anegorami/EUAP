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
        private TextureManager textureManager;

        public string StateId { get { return STATE_ID; } }

        internal DrawSpriteState(OpenGL openGl)
        {
            gl = openGl;
            textureManager = new TextureManager(gl);

            textureManager.AddTexturePathAndLoad("text1", "testImage.png");
        }

        public void Update(double elapsedTimeMs)
        {
            
        }

        public void Render()
        {
            double halfHeight = 300;
            double halfWidth = 300;

            gl.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.PointSize(5.0f);

            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, textureManager.GetTexture("text1").glId);

            gl.Begin(OpenGL.GL_TRIANGLES);
            {
                gl.TexCoord(0, 0);
                gl.Vertex(-halfWidth, halfHeight); // top left
                gl.TexCoord(2, 0);
                gl.Vertex(halfWidth, halfHeight); // top right
                gl.TexCoord(0, 2);
                gl.Vertex(-halfWidth, -halfHeight); // bottom left

                gl.TexCoord(0, 5);
                gl.Vertex(halfWidth, halfHeight); // top right
                gl.TexCoord(5, 5);
                gl.Vertex(halfWidth, -halfHeight); // bottom right
                gl.TexCoord(5, 0);
                gl.Vertex(-halfWidth, -halfHeight); // bottom left
            }
            gl.End();
            gl.Finish();
        }
    }
}
