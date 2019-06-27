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
        private readonly OpenGL gl;

        public Renderer(OpenGL openGl)
        {
            gl = openGl;

            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.Enable(OpenGL.GL_BLEND);
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
        }

        public void DrawImmediateModeVertex(Vector3D position, Color color, Point uvs)
        {
            gl.Color(color.R, color.G, color.B, color.A);
            gl.TexCoord(uvs.X, uvs.Y);
            gl.Vertex(position.X, position.Y, position.Z);
        }

        public void DrawImmediateModeVertex(Vector3D position, Color color)
        {
            gl.Color(color.R, color.G, color.B, color.A);
            gl.Vertex(position.X, position.Y, position.Z);
        }

        public void DrawImmediateModeVertex(Vector3D position, Point uvs)
        {
            gl.TexCoord(uvs.X, uvs.Y);
            gl.Vertex(position.X, position.Y, position.Z);
        }

        public void DrawImmediateModeVertexSingle(Vector3D position, Color color, Point uvs)
        {
            gl.Begin(OpenGL.GL_TRIANGLES);
            gl.Color(color.R, color.G, color.B, color.A);
            gl.TexCoord(uvs.X, uvs.Y);
            gl.Vertex(position.X, position.Y, position.Z);
            gl.End();
        }

        public void DrawImmediateModeVertexSingle(Vector3D position, Color color)
        {
            gl.Begin(OpenGL.GL_TRIANGLES);
            gl.Color(color.R, color.G, color.B, color.A);
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

        public void ClearScreen(Color clearColor)
        {
            gl.ClearColor(clearColor.R, clearColor.G, clearColor.B, clearColor.A);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
        }

        public void Finish()
        {
            gl.Finish();
        }

        public void Draw(Sprite sprite)
        {
            gl.Begin(OpenGL.GL_TRIANGLES);
            {
                for(int i =0; i < Sprite.VERTEX_AMOUNT; i++)
                {
                    if(sprite.HasTexture())
                    {
                        gl.BindTexture(OpenGL.GL_TEXTURE_2D, sprite.Texture.glId);                        
                    }

                    DrawImmediateModeVertex(sprite.VertexPositions[i], sprite.VertexColors[i], sprite.VertexUvs[i]);
                }
            }
            gl.End();
        }
    }
}
