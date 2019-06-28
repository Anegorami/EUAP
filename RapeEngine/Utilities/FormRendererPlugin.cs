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
        /// Initializes
        /// </summary>
        /// <param name="renderer"></param>
        public FormRendererPlugin(Renderer renderer)
        {
            FormRendererPlugin.renderer = renderer;
            gl = renderer.getGlObject();
        }

        public void WindowSizeChangedEventPlugin(object sender, EventArgs e)
        {
            Form form = (Form)sender;

            gl.Viewport(0, 0, form.ClientSize.Width, form.ClientSize.Height);

            renderer.Setup2DGraphics((float)form.ClientSize.Width / (float)form.ClientSize.Height); 
        }
    }
}
