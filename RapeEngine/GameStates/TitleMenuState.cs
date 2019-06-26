﻿using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapeEngine.GameStates
{
    public class TitleMenuState : IGameState
    {
        public static string STATE_ID = "TitleMenuState";
        private readonly OpenGL gl;
        private readonly StateSystemManager stateManager;

        private double currentRotation = 0;

        public string StateId { get { return STATE_ID; } }

        public TitleMenuState(StateSystemManager stateSystemManager, OpenGL openGl)
        {
            gl = openGl;
            stateManager = stateSystemManager;
        }
        public void Update(double elapsedTimeMs)
        {
            currentRotation = elapsedTimeMs / 1000f;
        }

        public void Render()
        {
            gl.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            gl.PointSize(5.0f);
            gl.Rotate(currentRotation, 0, 1, 0);
            gl.Begin(OpenGL.GL_TRIANGLES);
            {
                gl.Color(1.0, 0.0, 0.0, 0.5);
                gl.Vertex(-50f, 0, 0f);
                gl.Color(0.0, 1.0, 0.0);
                gl.Vertex(50f, 0, 0f);
                gl.Color(0.0, 0.0, 1.0);
                gl.Vertex(0, 50f, 0f);
            }
            gl.End();
            gl.Finish();
        }
    }
}
