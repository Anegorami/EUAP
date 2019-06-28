using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace RapeEngine
{
    public delegate void RendererAspectChangedEvent();

    public class Renderer
    {
        private const double DEFAULT_VIRTUAL_HEIGHT = 1000;
        private const double DEFAULT_VIRTUAL_WIDTH = 1000;

        private const float COLOR_MAX_VALUE_INT = 255;
        private const double DEFAULT_ASPECT_RATIO = 1280f / 720f;

        private bool maintainAspectRatio;
        private readonly OpenGL gl;
        private Color clearColor;

        public event RendererAspectChangedEvent VirtualHeightMaxChanged;
        public event RendererAspectChangedEvent VirtualWidthMaxChanged;

        public double VirtualHeightMax
        {
            get
            {
                return DEFAULT_VIRTUAL_HEIGHT;
            }
        }

        public double VirtualWidthMax
        {
            get
            {
                if (maintainAspectRatio)
                {
                    return VirtualHeightMax * AspectRatio;
                }
                else
                {
                    return VirtualHeightMax;
                }
            }
        }

        internal static double AspectRatio { get; set; }

        public Renderer(OpenGL openGl, int setViewportWidth, int setViewportHeight, bool maintainAspectRatio = true)
        {
            AspectRatio = 1;

            this.maintainAspectRatio = maintainAspectRatio;

            gl = openGl;
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.Enable(OpenGL.GL_BLEND);
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);

            gl.Viewport(0, 0, setViewportWidth, setViewportHeight);

            Setup2DGraphics((float)setViewportWidth / (float)setViewportHeight);
        }

        public Renderer(OpenGL openGl, bool maintainAspectRatio = true)
        {

            AspectRatio = 1;

            this.maintainAspectRatio = maintainAspectRatio;

            gl = openGl;
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.Enable(OpenGL.GL_BLEND);
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);

            int[] viewportData = new int[4];
            gl.GetInteger(OpenGL.GL_VIEWPORT, viewportData);

            Setup2DGraphics((float)viewportData[2] / (float)viewportData[3]);
        }

        public void Setup2DGraphics(double newAspectRatio)
        {
            if (maintainAspectRatio)
            {
                AspectRatio = newAspectRatio;

                VirtualWidthMaxChanged?.Invoke();
            }

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Ortho(-VirtualWidthMax / 2f, VirtualWidthMax / 2f, -VirtualHeightMax / 2f, VirtualHeightMax / 2f, -100, 100);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
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

        public void DrawBackground(Sprite sprite)
        {
            double oldWidth = sprite.Width;
            double oldHeight = sprite.Height;
            Vector3D oldPosition = sprite.GetPosition();

            sprite.Width = VirtualWidthMax;
            sprite.Height = VirtualHeightMax;
            sprite.SetPosition(0, 0);

            ClearScreen();
            gl.Disable(OpenGL.GL_DEPTH_TEST);

            Draw(sprite);

            gl.Clear(OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.Enable(OpenGL.GL_DEPTH_TEST);

            sprite.Width = oldWidth;
            sprite.Height = oldHeight;
            sprite.SetPosition(oldPosition);
        }
    }


}
