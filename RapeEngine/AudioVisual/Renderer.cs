using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace RapeEngine
{
    public class Renderer
    {
        private const float COLOR_MAX_VALUE_INT = 255;

        private readonly OpenGL gl;
        private Color clearColor;

        public Renderer(OpenGL openGl)
        {
            gl = openGl;
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.Enable(OpenGL.GL_BLEND);
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
        }

        internal OpenGL getGlObject()
        {
            return gl;
        }

        public void BeginVertexDraw()
        {
            gl.Begin(OpenGL.GL_TRIANGLES);
        }

        public void EndVertexDraw()
        {
            gl.End();
        }

        public void DrawImmediateModeVertex(Vector3D position, Color color, Point uvs)
        {
            float colorR = (float)(color.R) / COLOR_MAX_VALUE_INT;
            float colorG = (float)(color.G) / COLOR_MAX_VALUE_INT;
            float colorB = (float)(color.B) / COLOR_MAX_VALUE_INT;
            float colorA = (float)(color.A) / COLOR_MAX_VALUE_INT;

            gl.Color(colorR, colorG, colorB, colorA);
            gl.TexCoord(uvs.X, uvs.Y);
            gl.Vertex(position.X, position.Y, position.Z);
        }

        public void DrawImmediateModeVertex(Vector3D position, Color color)
        {
            float colorR = (float)(color.R) / COLOR_MAX_VALUE_INT;
            float colorG = (float)(color.G) / COLOR_MAX_VALUE_INT;
            float colorB = (float)(color.B) / COLOR_MAX_VALUE_INT;
            float colorA = (float)(color.A) / COLOR_MAX_VALUE_INT;

            gl.Color(colorR, colorG, colorB, colorA);
            gl.Vertex(position.X, position.Y, position.Z);
        }

        public void DrawImmediateModeVertex(Vector3D position, Point uvs)
        {
            gl.TexCoord(uvs.X, uvs.Y);
            gl.Vertex(position.X, position.Y, position.Z);
        }

        public void DrawImmediateModeVertexSingle(Vector3D position, Color color, Point uvs)
        {
            float colorR = (float)(color.R) / COLOR_MAX_VALUE_INT;
            float colorG = (float)(color.G) / COLOR_MAX_VALUE_INT;
            float colorB = (float)(color.B) / COLOR_MAX_VALUE_INT;
            float colorA = (float)(color.A) / COLOR_MAX_VALUE_INT;

            gl.Begin(OpenGL.GL_TRIANGLES);
            gl.Color(colorR, colorG, colorB, colorA);
            gl.TexCoord(uvs.X, uvs.Y);
            gl.Vertex(position.X, position.Y, position.Z);
            gl.End();
        }

        public void DrawImmediateModeVertexSingle(Vector3D position, Color color)
        {
            float colorR = (float)(color.R) / COLOR_MAX_VALUE_INT;
            float colorG = (float)(color.G) / COLOR_MAX_VALUE_INT;
            float colorB = (float)(color.B) / COLOR_MAX_VALUE_INT;
            float colorA = (float)(color.A) / COLOR_MAX_VALUE_INT;

            gl.Begin(OpenGL.GL_TRIANGLES);
            gl.Color(colorR, colorG, colorB, colorA);
            gl.Vertex(position.X, position.Y, position.Z);
            gl.End();
        }

        public void DrawImmediateModeVertexSingle(Vector3D position, Point uvs)
        {
            gl.Begin(OpenGL.GL_TRIANGLES);
            gl.TexCoord(uvs.X, uvs.Y);
            gl.Vertex(position.X, position.Y, position.Z);
            gl.End();
        }

        public void ClearScreen()
        {
            gl.ClearColor((float)clearColor.R, (float)clearColor.G, (float)clearColor.B, (float)clearColor.A);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
        }

        public void Finish()
        {
            gl.Finish();
        }

        public void SetClearScreenColor(Color clearColour)
        {
            clearColor = clearColour;
        }

        public void SetClearScreenColor(int r, int g, int b, int a)
        {
            clearColor = Color.FromArgb(a, r, g, b);
        }

        public void Draw(Sprite sprite)
        {
            if (sprite.HasTexture())
            {
                gl.BindTexture(OpenGL.GL_TEXTURE_2D, sprite.Texture.glId);
            }

            gl.Begin(OpenGL.GL_TRIANGLES);
            {
                for(int i =0; i < Sprite.VERTEX_AMOUNT; i++)
                {
                    DrawImmediateModeVertex(sprite.VertexPositions[i], sprite.VertexColors[i], sprite.VertexUvs[i]);
                }
            }
            gl.End();
        }
    }
}
