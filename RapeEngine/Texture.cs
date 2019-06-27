using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapeEngine
{
    public class Texture
    {
        public readonly uint glId;
        public readonly int width;
        public readonly int height;

        //Only texture manager can actually generate these
        internal Texture(uint glId, int width, int height)
        {
            this.glId = glId;
            this.width = width;
            this.height = height;
        }     
    }
}
