using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapeEngine.GameStates.TestStates
{
    internal class DrawSpriteState : IGameState
    {
        public const string STATE_ID = "DrawSpriteState";
        private readonly OpenGL gl;
        private TextureManager textureManager;
        private Renderer renderer;
        private Sprite testSprite1;
        private Sprite testSprite2;

        public string StateId { get { return STATE_ID; } }

        internal DrawSpriteState(OpenGL openGl)
        {
            gl = openGl;
            textureManager = new TextureManager(gl);
            renderer = new Renderer(gl);
            testSprite1 = new Sprite();
            testSprite2 = new Sprite();
            
            textureManager.AddTexturePathAndLoad("text1", "testImage2.png");

            testSprite1.Texture = textureManager.GetTexture("text1");
            testSprite1.Height = (1000f);
            testSprite1.Width = (1000f);

            testSprite2.Texture = textureManager.GetTexture("text1");
            testSprite2.SetPosition(-256, -256);
            testSprite2.Color = Color.FromArgb(1, 0, 0, 1);
        }

        public void Update(double elapsedTimeMs)
        {
            
        }

        public void Render()
        {
            renderer.ClearScreen(Color.FromArgb(1, 0, 0, 0));
            renderer.Draw(testSprite1);
            renderer.Draw(testSprite2);
            renderer.Finish();
        }
    }
}
