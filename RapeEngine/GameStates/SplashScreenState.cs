﻿using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapeEngine.GameStates
{
    public class SplashScreenState: IGameState
    {
        public const string STATE_ID = "SplashScreenState";
        private const int TIME_SPLASH_ON_SCREEN_SECONDS = 3;

        private readonly StateSystemManager stateManager;
        private readonly Renderer renderer;
        private float timeLeftOnScreen = TIME_SPLASH_ON_SCREEN_SECONDS;

        public string StateId { get { return STATE_ID; } }

        public SplashScreenState(StateSystemManager stateSystemManager, Renderer renderer)
        {
            stateManager = stateSystemManager;
            this.renderer = renderer;
        }
        public void Update(double elapsedTimeMs)
        {
            timeLeftOnScreen -= (float)(elapsedTimeMs / 1000);

            if(timeLeftOnScreen <= 0)
            {
                timeLeftOnScreen = TIME_SPLASH_ON_SCREEN_SECONDS;
                stateManager.SetState(TitleMenuState.STATE_ID);
            }
        }

        public void Render()
        {
            renderer.SetClearScreenColor(255, 255, 255, 255);
            renderer.ClearScreen();
            renderer.Finish();
        }
    }
}
