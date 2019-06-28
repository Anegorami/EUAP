using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapeEngine
{
    /// <summary>
    /// Class containing basic abstracted information on textures. Not usually used on its own, 
    /// being just a data container about a texture whereas other classes such as 
    /// the texture manager provide the heavy duty processing and management.
    /// </summary>
    public class Texture
    {
        /// <summary>
        /// The id of the texture in the GPU. GPU stores textures on its own 
        /// when the manager loads them in, the CPU just needs to remember 
        /// the reference id for working with the loaded texture itself.
        /// </summary>
        public readonly uint glId;

        /// <summary>
        /// Referenced texture's width
        /// </summary>
        public readonly int width;

        /// <summary>
        /// Referenced texture's height. 
        /// </summary>
        public readonly int height;

        //Only texture manager can actually generate these
        internal Texture(uint glId, int width, int height)
        {
            this.glId = glId;
            this.width = width;
            this.height = height;
        }     

        internal Texture(Texture texture)
        {
            if(texture == null)
            {
                return;
            }

            this.glId = texture.glId;
            this.width = texture.width;
            this.height = texture.height;
        }
    }
}
