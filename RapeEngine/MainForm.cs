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

            Renderer renderer = new Renderer(openGLControl1.OpenGL, 1280, 720, true);
            FormRendererPlugin.Init(renderer);
            this.SizeChanged += FormRendererPlugin.WindowSizeChangedEventPlugin;

            GameMain.GameMainBegin(openGLControl1, renderer);
        }
    }
}