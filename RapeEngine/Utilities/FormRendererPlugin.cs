using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RapeEngine
{
    /// <summary>
    /// Logic for interfacing the game's renderer with the game's window form. 
    /// </summary>
    /// Decided not to make it static as users really shouldn't be able to forget init'ing it.
    public class FormRendererPlugin
    {
        private static OpenGL gl;
        private static Renderer renderer;

        /// <summary>
        /// Standard constructor, initializes internal elements.
        /// </summary>
        /// <param name="renderer">system renderer</param>
        public FormRendererPlugin(Renderer renderer)
        {
            FormRendererPlugin.renderer = renderer;
            gl = renderer.getGlObject();
        }

        /// <summary>
        /// The plugin for the window size changed event for the game's main form. 
        /// Add this to the WindowSizeChanged event for the renderer to work properly. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void WindowSizeChangedEventPlugin(object sender, EventArgs e)
        {
            Form form = (Form)sender;

            // Updates viewport to match the window size, and tell the renderer to account for the fact 
            // that the viewport size has changed.
            gl.Viewport(0, 0, form.ClientSize.Width, form.ClientSize.Height);
            renderer.Setup2DGraphics((float)form.ClientSize.Width / (float)form.ClientSize.Height); 
        }
    }
}
