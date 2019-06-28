using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RapeEngine
{
    static class FormRendererPlugin
    {
        private static OpenGL gl;
        private static Renderer renderer;

        public static void Init(Renderer renderer)
        {
            FormRendererPlugin.renderer = renderer;
            gl = renderer.getGlObject();
        }

        public static void WindowSizeChangedEventPlugin(object sender, EventArgs e)
        {
            Form form = (Form)sender;

            gl.Viewport(0, 0, form.ClientSize.Width, form.ClientSize.Height);

            renderer.Setup2DGraphics((float)form.ClientSize.Width / (float)form.ClientSize.Height); 
        }
    }
}
