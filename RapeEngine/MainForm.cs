using RapeEngine.GameStates;
using SharpGL;
using System;
using System.Windows.Forms;
using System.Drawing;

namespace RapeEngine
{
    /// <summary>
    /// Main form.
    /// </summary>
    public sealed partial class MainForm : Form
    {
        private bool doFullscreen;
        readonly Size defaultSize = new System.Drawing.Size(800, 600);
        FormRendererPlugin rendererPlugin;

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
                ClientSize = defaultSize;
                FormBorderStyle = FormBorderStyle.FixedSingle;
            }

            // Plugs renderer into the main window so it can update properly before starting the game.
            Renderer renderer = new Renderer(openGLControl1.OpenGL, ClientSize.Width, ClientSize.Height, false);
            rendererPlugin = new FormRendererPlugin(renderer);
            this.SizeChanged += rendererPlugin.WindowSizeChangedEventPlugin;

            GameMain.GameMainBegin(openGLControl1, renderer);
        }
    }
}