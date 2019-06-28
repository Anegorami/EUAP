using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapeEngine
{
    class TextureManager : IDisposable
    {
        private readonly OpenGL gl;
        private Dictionary<string, Texture> textureDatabase = new Dictionary<string, Texture>();
        private Dictionary<string, string> texturePathTracker = new Dictionary<string, string>();

        public TextureManager(OpenGL openGl)
        {
            gl = openGl;
        }

        public void AddTexturePath(string textureId, string texturePath)
        {
            if(!File.Exists(texturePath))
            {
                throw new Exception("AddTexturePath: path invalid: " + texturePath);
            }

            texturePathTracker.Add(textureId, texturePath);
        }

        public bool LoadTextureIntoMemory(string textureId)
        {
            if(textureDatabase.ContainsKey(textureId))
            {
                return true;
            }

            try
            {
                int height, width;
                uint glId;

                Bitmap image = new Bitmap(texturePathTracker[textureId]);
             
                if(image == null)
                {
                    return false;
                }

                // Tell gl that we're adding a new texture to it
                uint[] textureIdArray = new uint[1];

                gl.GenTextures(1, textureIdArray);

                //	Get the maximum texture size supported by OpenGL.
                int[] textureMaxSize = { 0 };
                gl.GetInteger(OpenGL.GL_MAX_TEXTURE_SIZE, textureMaxSize);

                //  If need to scale, do so now.
                if (image.Width > textureMaxSize[0] || image.Height > textureMaxSize[0])
                {
                    int newWidth, newHeight;
                    float ratio;
                    bool aspectSkewedTowardsWidth = false;

                    // Resize the image. Maintain aspect ratio.
                    // If they're both bigger than the max allowed size, set the largest value 
                    // to the max and then set the smaller dimension down in proportion using the aspect ratio 
                    // which should wind up under the limit as well by virtue of being smaller than the other
                    // dimension when scaled to aspect ratio
                    if (image.Width > textureMaxSize[0] && image.Height > textureMaxSize[0])
                    {
                        if (image.Width > image.Height)
                        {
                            aspectSkewedTowardsWidth = true;
                        }
                    }
                    else if (image.Width > textureMaxSize[0])
                    {
                        aspectSkewedTowardsWidth = true;
                    }

                    if (aspectSkewedTowardsWidth)
                    {
                        ratio = (float)(image.Width) / (float)(image.Height);

                        newWidth = textureMaxSize[0];
                        newHeight = (int)((float)(newWidth) / ratio);
                    }
                    else
                    {
                        ratio = (float)(image.Height) / (float)(image.Width);

                        newHeight = textureMaxSize[0];
                        newWidth = (int)((float)(newHeight) / ratio);
                    }

                    Image newImage = image.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);

                    //  Destory the old image, and reset.
                    image.Dispose();
                    image = (Bitmap)newImage;
                }

                //  Lock the image bits (so that we can pass them to OGL).
                BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                //	Set the width and height and id.
                width = image.Width;
                height = image.Height;
                glId = textureIdArray[0];

                //	Bind our texture object (make it the current texture).
                gl.BindTexture(OpenGL.GL_TEXTURE_2D, glId);

                //  Set the image data.
                gl.TexImage2D(OpenGL.GL_TEXTURE_2D, 0, (int)OpenGL.GL_RGBA, width, height, 0, OpenGL.GL_BGRA, OpenGL.GL_UNSIGNED_BYTE, bitmapData.Scan0);

                //  Unlock the image.
                image.UnlockBits(bitmapData);

                //  Dispose of the image file.
                image.Dispose();

                //  Set linear filtering mode.
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);

                // Clamp edges of texture 
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_CLAMP_TO_EDGE);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_CLAMP_TO_EDGE);

                //save the Texture information into our table
                textureDatabase.Add(textureId, new Texture(glId, width, height));

                return true;
                
            }
            catch(Exception e)
            {
                throw new Exception("Load texture into memory failed, textureId didn't exist. Make sure it's spelled correctly and that addTexturePath was called first. TextureId: " + textureId, e);
            }
        }

        public void RemoveTextureFromMemory(string textureId)
        {
            try
            {
                Texture texture = textureDatabase[textureId];

                gl.DeleteTextures(1, new uint[1] { texture.glId });

                textureDatabase.Remove(textureId);
            }
            catch(Exception e)
            {
                throw new Exception("Remove texture into memory failed, textureId didn't exist. Make sure it's spelled correctly and that this texture was loaded into memory before calling remove. TextureId: " + textureId, e);
            }
        }

        public void AddTexturePathAndLoad(string textureId, string texturePath)
        {
            AddTexturePath(textureId, texturePath);
            LoadTextureIntoMemory(textureId);
        }

        public Texture GetTexture(string textureId)
        {
            return textureDatabase[textureId];
        }

        public Texture TryGetTexture(string textureId, out bool success)
        {
            try
            {
                success = true;
                return GetTexture(textureId);
            }
            catch
            {
                success = false;
                return null;
            }
        }

        public bool IsTextureLoaded(string textureId)
        {
            return textureDatabase.ContainsKey(textureId);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                foreach(Texture t in textureDatabase.Values)
                {
                    gl.DeleteTextures(1, new uint[] { t.glId });
                }

                textureDatabase.Clear();

                disposedValue = true;
            }
        }

        ~TextureManager()
        {
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
