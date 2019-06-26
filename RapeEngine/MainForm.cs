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

            StateSystemManager stateSystemManager = new StateSystemManager();
            SplashScreenState splashScreen = new SplashScreenState(stateSystemManager, openGLControl1.OpenGL);
            TitleMenuState titleMenu = new TitleMenuState(stateSystemManager, openGLControl1.OpenGL);

            stateSystemManager.AddState(splashScreen);
            stateSystemManager.AddState(titleMenu);

            stateSystemManager.SetState(SplashScreenState.STATE_ID);

            GameMain.GameMainBegin(openGLControl1, stateSystemManager);
        }
    }
}