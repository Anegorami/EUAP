﻿using SharpGL;
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
        private OpenGL gl;

        public string StateId { get { return STATE_ID; } }

        internal DrawSpriteState(Renderer renderer)
        {
            textureManager = new TextureManager(renderer.getGlObject());
            testSprite1 = new Sprite();
            testSprite2 = new Sprite();
            this.renderer = renderer;
            gl = renderer.getGlObject();

            textureManager.AddTexturePathAndLoad("text1", "exampleBackground.bmp");
            textureManager.AddTexturePathAndLoad("text2", "testImage2.png");
            testSprite1.Texture = textureManager.GetTexture("text1");
            testSprite1.SetPosition(100, 100);
            testSprite1.Height = 100;
            testSprite1.Width = 100;

            testSprite2.Texture = textureManager.GetTexture("text2");
            testSprite2.SetPosition(0, 0, 1f);
            renderer.SetClearScreenColor(Color.FromArgb(255, 0, 0, 0));

        }

        public void Update(double elapsedTimeMs)
        {
            
        }

        public void Render()
        {


            
            renderer.ClearScreen();

            renderer.DrawBackground(testSprite1);
            //renderer.Draw(testSprite1);
            renderer.Draw(testSprite2);
            renderer.Finish();
        }
    }
}
