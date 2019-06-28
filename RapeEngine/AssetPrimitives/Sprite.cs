using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace RapeEngine
{
    /// <summary>
    /// A standard object for your standard sprite, formed out of two triangles making a virtual square on screen for 
    /// textures to be mapped to. Provides the logical counterpart to a texture, being an actual manipulatable element to 
    /// the former's pure picture.
    /// </summary>
    public class Sprite
    {
        /// <summary>
        /// There are 6 vertexes making up one sprite, as it's two triangles (3 vertexes each) forming one quad shape.
        /// </summary>
        internal const int VERTEX_AMOUNT = 6;

        /// <summary>
        /// Positional information on each vertex in 3D space. Default is to be at the view's center
        /// </summary>
        private Vector3D[] vertexPositions = new Vector3D[VERTEX_AMOUNT];

        /// <summary>
        /// Color information on each vertex in 3D space. Default is to be white and opaque, 
        /// letting textures have complete reign over the display (anything other than opaque white 
        /// starts to merge vertex color and texture coordinate color together).
        /// </summary>
        private Color[] vertexColors = new Color[VERTEX_AMOUNT];

        /// <summary>
        /// UV information associated with each vertex. That is, the texture coordinate 
        /// associated with each sprite coordinate. 
        /// </summary>
        private Point[] vertexUVs = new Point[VERTEX_AMOUNT];

        /// <summary>
        /// The texture that has been associated with this sprite. It's the sprites job to anchor it on screen for displaying.
        /// </summary>
        private Texture texture;

        /// <summary>
        /// External accessor/getter for vertex positions.
        /// TODO: might wanna make it return a copy rather than a reference, not sure yet.
        /// </summary>
        public Vector3D[] VertexPositions
        {
            get
            {
                return vertexPositions;
            }
        }

        /// <summary>
        /// External accessor/getter for vertex colors.
        /// </summary>
        public Color[] VertexColors
        {
            get
            {
                return vertexColors;
            }
        }

        /// <summary>
        /// External accessor/getter for vertex uv's
        /// </summary>
        public Point[] VertexUvs
        {
            get
            {
                return vertexUVs;
            }
        }

        /// <summary>
        /// External accessor/getter for the sprite's texture. Main way 
        /// off associating a texture with the sprite. By default, 
        /// sprite height/width will be set to the sprite's.
        /// </summary>
        public Texture Texture
        {
            get
            {
                if (texture == null)
                {
                    return null;
                }
                else
                {
                    return new Texture(texture);
                }
            }
            set
            {
                texture = value;

                // Set width and height to texture's by default.
                CalcAndStoreVertexPositions(GetCenter(), texture.width, texture.height);
            }
        }

        /// <summary>
        /// Setter for giving a sprite a universal color.
        /// </summary>
        public Color Color
        {
            set
            {
                for(int i = 0; i < VERTEX_AMOUNT; i++)
                {
                    vertexColors[i] = value;
                }
            }
        }

        /// <summary>
        /// Setter for giving a sprite a set width other than 
        /// the default or the native texture's width.
        /// </summary>
        public double Width
        {
            get
            {
                //topright-topleft
                return vertexPositions[1].X - vertexPositions[0].X;
            }
            set
            {
                CalcAndStoreVertexPositions(GetCenter(), value, Height);
            }
        }

        /// <summary>
        /// Setter for giving a sprite a set height other than 
        /// the default or the native texture's height.
        /// </summary>
        public double Height
        {
            get
            {
                //topleft-bottomleft
                return vertexPositions[0].Y - vertexPositions[2].Y;
            }
            set
            {
                CalcAndStoreVertexPositions(GetCenter(), Width, value);
            }
        }

        /// <summary>
        /// Standard constructor. Creates a new white opaque sprite at the view's origin 
        /// with a height and width of 1. UV's go from 0 to 1, the standard for all textures.
        /// </summary>
        public Sprite()
        {
            CalcAndStoreVertexPositions(new Vector3D(0, 0, 0), 1, 1);
            Color = Color.FromArgb(255, 255, 255, 255);
            SetUVs(new Point(0, 0), new Point(1, 1));
        }

        /// <summary>
        /// Returns if the sprite has a texture associated with it yet or not.
        /// </summary>
        /// <returns></returns>
        public bool HasTexture()
        {
            return texture != null;
        }

        /// <summary>
        /// Sets the position of the sprite in 3D space for the viewport. 
        /// Units are all referenced to the viewport's own coordinate system rather than pixel or native OS screen based.
        /// </summary>
        /// <param name="x">The x coordinate in the 3D coordinate system</param>
        /// <param name="y">The y coordinate in the 3D coordinate system</param>
        /// <param name="z">The z coordinate in the 3D coordinate system</param>
        public void SetPosition(double x, double y, double z = 0)
        {
            SetPosition(new Vector3D(x, y, z));
        }

        /// <summary>
        /// Sets the position of the sprite in 3D space for the viewport. 
        /// Vector units are all referenced to the viewport's own coordinate system rather than pixel or native OS screen based.
        /// </summary>
        /// <param name="position">The vector describing the sprite's new position in space</param>
        public void SetPosition(Vector3D position)
        {
            CalcAndStoreVertexPositions(position, Width, Height);
        }

        /// <summary>
        /// Returns the central position of the sprite described in 3D space.
        /// </summary>
        /// <returns></returns>
        public Vector3D GetPosition()
        {
            return GetCenter();
        }

        /// <summary>
        /// Sets the texture coordinates in UV that the sprite will display, mapped from its top left 
        /// corner to its bottom right
        /// </summary>
        /// <param name="topLeft">The UV point of the texture the sprite will display at its top left</param>
        /// <param name="bottomRight">The UV point of the texture the sprite will display at its bottom right</param>
        public void SetUVs(Point topLeft, Point bottomRight)
        {
            vertexUVs[0] = topLeft;
            vertexUVs[1] = new Point(bottomRight.X, topLeft.Y);
            vertexUVs[2] = new Point(topLeft.X, bottomRight.Y);

            vertexUVs[3] = new Point(bottomRight.X, topLeft.Y);
            vertexUVs[4] = bottomRight;
            vertexUVs[5] = new Point(topLeft.X, bottomRight.Y);
        }

        /// <summary>
        /// Sets the individual sprite vertex positions based on the desired sprite central position + 
        /// the sprites desired with and height.
        /// </summary>
        /// <param name="position">Position of the center of the sprite in 3D space</param>
        /// <param name="width">Width of the sprite, units are in viewport's coordinate system's units</param>
        /// <param name="height">Height of the sprite, units are in viewport's coordinate system's units</param>
        private void CalcAndStoreVertexPositions(Vector3D position, double width, double height)
        {
            double halfWidth = width / 2;
            double halfHeight = height / 2;

            //clockwise creation of two triangles to make a quad
            // TopLeft, TopRight, BottomLeft
            vertexPositions[0] = new Vector3D(position.X - halfWidth, position.Y + halfHeight, position.Z);
            vertexPositions[1] = new Vector3D(position.X + halfWidth, position.Y + halfHeight, position.Z);
            vertexPositions[2] = new Vector3D(position.X - halfWidth, position.Y - halfHeight, position.Z);
            
            // TopRight, BottomRight, BottomLeft
            vertexPositions[3] = new Vector3D(position.X + halfWidth, position.Y + halfHeight, position.Z);
            vertexPositions[4] = new Vector3D(position.X + halfWidth, position.Y - halfHeight, position.Z);
            vertexPositions[5] = new Vector3D(position.X - halfWidth, position.Y - halfHeight, position.Z);
        }

        /// <summary>
        /// Returns the centeral position of the sprite in 3D space
        /// </summary>
        /// <returns></returns>
        private Vector3D GetCenter()
        {
            double halfWidth = Width / 2;
            double halfHeight = Height / 2;

            return new Vector3D(vertexPositions[0].X + halfWidth, vertexPositions[0].Y - halfHeight, vertexPositions[0].Z);
        }
    }
}
