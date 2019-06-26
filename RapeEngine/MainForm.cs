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

            if (doFullscreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }

            StateSystemManager stateSystemManager = new StateSystemManager();
            bool success = stateSystemManager.TestAddState();
            success = stateSystemManager.TestStateTransitions();

            GameMain.GameMainBegin(openGLControl1, stateSystemManager);
        }
    }
}