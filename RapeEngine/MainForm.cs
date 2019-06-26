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

            StateSystemManager stateSystemManager = new StateSystemManager();

            stateSystemManager.AddState(new SplashScreenState(stateSystemManager, openGLControl1.OpenGL));
            stateSystemManager.AddState(new TitleMenuState(stateSystemManager, openGLControl1.OpenGL));

            stateSystemManager.SetState(SplashScreenState.STATE_ID);

            GameMain.GameMainBegin(openGLControl1, stateSystemManager);
        }

        private void OpenGLControl1_ClientSizeChanged(object sender, EventArgs e)
        {
            base.OnClientSizeChanged(e);
            openGLControl1.OpenGL.Viewport(0, 0, this.ClientSize.Width, this.ClientSize.Height);
        }
    }
}