using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace RapeEngine
{
    public class Sprite
    {
        internal const int VERTEX_AMOUNT = 6;

        private Vector3D[] vertexPositions = new Vector3D[VERTEX_AMOUNT];
        private Color[] vertexColors = new Color[VERTEX_AMOUNT];
        private Point[] vertexUVs = new Point[VERTEX_AMOUNT];

        private Texture texture;

        public Vector3D[] VertexPositions
        {
            get
            {
                return vertexPositions;
            }
        }

        public Color[] VertexColors
        {
            get
            {
                return vertexColors;
            }
        }

        public Point[] VertexUvs
        {
            get
            {
                return vertexUVs;
            }
        }

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
                InitVertexPositions(GetCenter(), texture.width, texture.height);
                
            }
        }

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

        public double Width
        {
            get
            {
                //topright-topleft
                return vertexPositions[1].X - vertexPositions[0].X;
            }
            set
            {
                InitVertexPositions(GetCenter(), value, Height);
            }
        }

        public double Height
        {
            get
            {
                //topleft-bottomleft
                return vertexPositions[0].Y - vertexPositions[2].Y;
            }
            set
            {
                InitVertexPositions(GetCenter(), Width, value);
            }
        }

        public Sprite()
        {
            InitVertexPositions(new Vector3D(0, 0, 0), 1, 1);
            Color = Color.FromArgb(1, 1, 1, 1);
            SetUVs(new Point(0, 0), new Point(1, 1));
        }

        public bool HasTexture()
        {
            return texture != null;
        }

        public void SetPosition(double x, double y, double z = 0)
        {
            SetPosition(new Vector3D(x, y, z));
        }

        public void SetPosition(Vector3D position)
        {
            InitVertexPositions(position, Width, Height);
        }

        public void SetUVs(Point topLeft, Point bottomRight)
        {
            vertexUVs[0] = topLeft;
            vertexUVs[1] = new Point(bottomRight.X, topLeft.Y);
            vertexUVs[2] = new Point(topLeft.X, bottomRight.Y);

            vertexUVs[3] = new Point(bottomRight.X, topLeft.Y);
            vertexUVs[4] = bottomRight;
            vertexUVs[5] = new Point(topLeft.X, bottomRight.Y);
        }

        private void InitVertexPositions(Vector3D position, double width, double height)
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

        private Vector3D GetCenter()
        {
            double halfWidth = Width / 2;
            double halfHeight = Height / 2;

            return new Vector3D(vertexPositions[0].X + halfWidth, vertexPositions[0].Y - halfHeight, vertexPositions[0].Z);
        }
    }
}
