using System;
using System.Windows.Forms;

namespace RapeEngine {
	/// <summary>
	/// Main form.
	/// </summary>
	public sealed partial class MainForm: Form {
        private GameMain gameMain;
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

            GameMain.GameMainBegin(openGLControl1, stateSystemManager);
        }
    }
}