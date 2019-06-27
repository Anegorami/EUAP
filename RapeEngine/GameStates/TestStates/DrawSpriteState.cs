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
        private TextureManager textureManager;
        private Renderer renderer;
        private Sprite testSprite1;
        private Sprite testSprite2;

        public string StateId { get { return STATE_ID; } }

        internal DrawSpriteState(Renderer renderer)
        {
            textureManager = new TextureManager(renderer.getGlObject());
            testSprite1 = new Sprite();
            testSprite2 = new Sprite();
            this.renderer = renderer;

            textureManager.AddTexturePathAndLoad("text1", "testImage2.png");

            testSprite1.Texture = textureManager.GetTexture("text1");
            testSprite1.Height = (1000f);
            testSprite1.Width = (1000f);

            testSprite2.Texture = textureManager.GetTexture("text1");
            testSprite2.SetPosition(-256, -256, 1f);
            testSprite2.Color = Color.FromArgb(255, 0, 0, 255);
        }

        public void Update(double elapsedTimeMs)
        {
            
        }

        public void Render()
        {
            renderer.SetClearScreenColor(Color.FromArgb(255, 0, 0, 0));
            renderer.ClearScreen();
            renderer.Draw(testSprite1);
            renderer.Draw(testSprite2);
            renderer.Finish();
        }
    }
}
