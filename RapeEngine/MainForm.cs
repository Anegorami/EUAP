using RapeEngine.GameStates;
using SharpGL;
using System;
using System.Windows.Forms;

namespace RapeEngine
{
    /// <summary>
    /// Main form.
    /// </summary>
    public sealed partial class MainForm : Form
    {
        private bool doFullscreen;

        /// <summary>
        /// Basic constructor.
        /// </summary>
        public MainForm()
        {
            // Required.
            InitializeComponent();

            doFullscreen = false; // for testing purposes

            if(doFullscreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                ClientSize = new System.Drawing.Size(1280, 720);
                FormBorderStyle = FormBorderStyle.FixedSingle;
            }

            Setup2DGraphics(ClientSize.Width, ClientSize.Height);

            GameMain.GameMainBegin(openGLControl1);
        }

        private void OpenGLControl1_ClientSizeChanged(object sender, EventArgs e)
        {
            base.OnClientSizeChanged(e);
            openGLControl1.OpenGL.Viewport(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            Setup2DGraphics(ClientSize.Width, ClientSize.Height);
        }

        private void Setup2DGraphics(double width, double height)
        {
            OpenGL gl = openGLControl1.OpenGL;
            double halfWidth = width / 2f;
            double halfHeight = height / 2f;

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Ortho(-halfWidth, halfWidth, -halfHeight, halfHeight, -100, 100);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
        }
    }
}